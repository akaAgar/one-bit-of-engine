/*
==========================================================================
This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
WadPacker is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
WadPacker is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with Asterion Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Asterion.IO
{
/// <summary>
/// Provides a class to read and store various resources to be used in your game in an encrypted archive file. Basically an encrypted WAD (as in "Doom WAD") file.
/// </summary>
    public sealed class ResourceArchive : IDisposable
    {
        /// <summary>
        /// Maximul length of an entry name.
        /// </summary>
        public const int MAX_ENTRY_NAME_LENGTH = 32;

        /// <summary>
        /// Length of each entry in the index.
        /// </summary>
        private const int INDEX_ENTRY_LENGTH = MAX_ENTRY_NAME_LENGTH + 4; // name + int32 length
        
        /// <summary>
        /// Index of all entries stored in the file.
        /// </summary>
        private readonly Dictionary<string, ResourceArchiveIndex> Index = new Dictionary<string, ResourceArchiveIndex>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Path to the archive file.
        /// </summary>
        public string FilePath { get; private set; } = null;

        /// <summary>
        /// Password to the file, if any.
        /// </summary>
        public string Password { get; private set; } = null;

        /// <summary>
        /// Bytes to use as salt for the password.
        /// </summary>
        private byte[] PasswordSalt;

        /// <summary>
        /// Is the file password protected?
        /// </summary>
        public bool PasswordProtected { get { return !string.IsNullOrEmpty(Password); } }

        /// <summary>
        /// Total number of entries in the archive file.
        /// </summary>
        public int EntriesCount { get { return Index.Count; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath">Path to the archive file</param>
        /// <param name="password">Password, if any</param>
        public ResourceArchive(string filePath, string password = null)
        {
            FilePath = filePath;

            Password = password;
            if (PasswordProtected) PasswordSalt = GenerateSalt(password);

            if (!File.Exists(FilePath)) return;

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    int entryCount = br.ReadInt32();
                    int entryOffset = INDEX_ENTRY_LENGTH * entryCount + 4; // First entry is at the end of the index

                    for (int i = 0; i < entryCount; i++)
                    {
                        string entryName = Encoding.ASCII.GetString(br.ReadBytes(MAX_ENTRY_NAME_LENGTH)).Trim(' ', '\0').ToLower();
                        int entryLength = br.ReadInt32();

                        if (!Index.ContainsKey(entryName))
                            Index.Add(entryName, new ResourceArchiveIndex(entryOffset, entryLength));

                        entryOffset += entryLength;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the bytes of an entry.
        /// </summary>
        /// <param name="entry">The entry</param>
        /// <returns>Bytes of the entry, or null if the entry doesn't exist.</returns>
        public byte[] this[string entry] { get { return GetEntry(entry); } }

        /// <summary>
        /// Returns the bytes of an entry.
        /// </summary>
        /// <param name="entry">Name of the entry</param>
        /// <returns>Bytes of the entry, or null if the entry doesn't exist.</returns>
        public byte[] GetEntry(string entry)
        {
            entry = entry?.ToLowerInvariant();
            if (!EntryExists(entry)) return null;

            ResourceArchiveIndex entryIndex = Index[entry];

            byte[] bytes = new byte[entryIndex.Length];
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                fs.Seek(entryIndex.Offset, SeekOrigin.Begin);
                fs.Read(bytes, 0, entryIndex.Length);
                if (PasswordProtected)
                    bytes = DecryptBytes(bytes, Password, PasswordSalt);
            }

            return bytes;
        }

        /// <summary>
        /// Returns an entry as a memory stream.
        /// </summary>
        /// <param name="entry">Name of the entry</param>
        /// <returns>A stream, or null if the entry doesn't exist.</returns>
        public Stream GetEntryAsStream(string entry)
        {
            if (!EntryExists(entry)) return null;

            return new MemoryStream(GetEntry(entry));
        }

        /// <summary>
        /// Returns an entry as an image.
        /// </summary>
        /// <param name="entry">Name of the entry</param>
        /// <returns>An image, or null if the entry doesn't exist.</returns>
        public Image GetEntryAsImage(string entry)
        {
            byte[] bytes = GetEntry(entry);
            if (bytes == null) return null;

            Image image = null;

            using (MemoryStream ms = new MemoryStream(bytes))
                image = Image.FromStream(ms);

            return image;
        }

        /// <summary>
        /// Returns an entry as a text string.
        /// </summary>
        /// <param name="entry">Name of the entry</param>
        /// <param name="textEncoding">Text encoding to use, or UTF-8 if no encoding is provided</param>
        /// <returns>An string, or null if the entry doesn't exist.</returns>
        public string GetEntryAsText(string entry, Encoding textEncoding = null)
        {
            if (textEncoding == null) textEncoding = Encoding.UTF8;
            byte[] bytes = GetEntry(entry);
            if (bytes == null) return null;

            return textEncoding.GetString(bytes);
        }

        /// <summary>
        /// Array of all entries in the file.
        /// </summary>
        public string[] Entries { get { return Index.Keys.ToArray(); } }

        /// <summary>
        /// Does the entry exist?
        /// </summary>
        /// <param name="entry">Name of the entry</param>
        /// <returns>True if the entry exists, false otherwise</returns>
        public bool EntryExists(string entry)
        {
            entry = entry?.ToLowerInvariant();
            return Index.ContainsKey(entry);
        }

        /// <summary>
        /// Creates an archive from an array of source files.
        /// </summary>
        /// <param name="archiveFilePath">A path to the archive to create</param>
        /// <param name="password">Password to use, if any</param>
        /// <param name="filesToInclude">An array of paths to the file which must be included</param>
        /// <returns>True if everything went well, false otherwise</returns>
        public static bool CreateArchive(string archiveFilePath, string password = null, params string[] filesToInclude)
        {
            try
            {
                byte[] passwordSalt = new byte[0];
                if (!string.IsNullOrEmpty(password)) passwordSalt = GenerateSalt(password);

                List<byte> indexBytes = new List<byte>();
                List<byte> dataBytes = new List<byte>();

                int entryCount = filesToInclude.Length;

                foreach (string f in filesToInclude)
                {
                    string entryName = Path.GetFileNameWithoutExtension(f).ToLowerInvariant();
                    if (entryName.Length > MAX_ENTRY_NAME_LENGTH) entryName = entryName.Substring(0, MAX_ENTRY_NAME_LENGTH);
                    entryName = entryName.PadRight(MAX_ENTRY_NAME_LENGTH, '\0');
                    byte[] entryNameBytes = Encoding.ASCII.GetBytes(entryName);
                    byte[] fileBytes = File.ReadAllBytes(f);
                    if (!string.IsNullOrEmpty(password))
                        fileBytes = EncryptBytes(fileBytes, password, passwordSalt);

                    indexBytes.AddRange(entryNameBytes);
                    indexBytes.AddRange(BitConverter.GetBytes(fileBytes.Length));
                    dataBytes.AddRange(fileBytes);
                }

                List<byte> archiveBytes = new List<byte>();
                archiveBytes.AddRange(BitConverter.GetBytes(entryCount));
                archiveBytes.AddRange(indexBytes);
                archiveBytes.AddRange(dataBytes);

                File.WriteAllBytes(archiveFilePath, archiveBytes.ToArray());

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns some bytes to salt a password.
        /// </summary>
        /// <param name="password">The password to salt</param>
        /// <returns>An array of bytes to use as salt</returns>
        private static byte[] GenerateSalt(string password)
        {
            if (string.IsNullOrEmpty(password)) return new byte[0];
            int seed = Encoding.UTF8.GetBytes(password).Select(x => (int)x).Sum();
            Random saltRnd = new Random(seed);

            byte[] salt = new byte[32];
            for (int i = 0; i < salt.Length; i++)
                salt[i] = (byte)saltRnd.Next(256);

            return salt;
        }

        /// <summary>
        /// Ecrypts an array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to encrypt</param>
        /// <param name="password">Password to use for encryption</param>
        /// <param name="passwordSalt">Bytes to use to salt the password</param>
        /// <returns>An encrypted array of bytes</returns>
        private static byte[] EncryptBytes(byte[] bytes, string password, byte[] passwordSalt)
        {
            if (bytes == null) return null;
            if (string.IsNullOrEmpty(password)) return bytes;
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, passwordSalt);

            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms,
              aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// Decrypts an array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to decrypt</param>
        /// <param name="password">Password to use for decryption</param>
        /// <param name="passwordSalt">Bytes to use to salt the password</param>
        /// <returns>A decrypted array of bytes</returns>
        private static byte[] DecryptBytes(byte[] bytes, string password, byte[] passwordSalt)
        {
            if (bytes == null) return null;
            if (string.IsNullOrEmpty(password)) return bytes;
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, passwordSalt);

            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms,
              aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { }
    }
}
/*
==========================================================================
This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
Asterion Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
Asterion Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with Asterion Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Asterion.IO
{
    /// <summary>
    /// File source using an Asterion archive file as a file source
    /// </summary>
    public class FileSourceArchive : FileSource
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
        private readonly Dictionary<string, FileSourceArchiveIndex> Index = new Dictionary<string, FileSourceArchiveIndex>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Password to the file, if any.
        /// </summary>
        public string Password { get; private set; } = null;

        /// <summary>
        /// Bytes to use as salt for the password.
        /// </summary>
        private readonly byte[] PasswordSalt;

        /// <summary>
        /// Is the file password protected?
        /// </summary>
        public bool PasswordProtected { get { return !string.IsNullOrEmpty(Password); } }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">Path to the Asterion archive to use as a source file</param>
        /// <param name="password">Archive password, if any</param>
        public FileSourceArchive(string path, string password = ""): base(path)
        {
            Password = password;
            if (PasswordProtected) PasswordSalt = GenerateSalt(password);

            if (!File.Exists(SourcePath)) return;

            using (FileStream fs = new FileStream(SourcePath, FileMode.Open))
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
                            Index.Add(entryName, new FileSourceArchiveIndex(entryOffset, entryLength));

                        entryOffset += entryLength;
                    }
                }
            }
        }

        /// <summary>
        /// Does the file exist in this file source?
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>True if the file exists, false otherwise</returns>
        public override bool FileExists(string file)
        {
            return Index.ContainsKey(file);
        }

        /// <summary>
        /// Returns the bytes of a file stored in this file source.
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>An array of byte if the file exists, null otherwise</returns>
        public override byte[] GetFile(string file)
        {
            if (!FileExists(file)) return null;

            FileSourceArchiveIndex entryIndex = Index[file];

            byte[] bytes = new byte[entryIndex.Length];
            using (FileStream fs = new FileStream(SourcePath, FileMode.Open))
            {
                fs.Seek(entryIndex.Offset, SeekOrigin.Begin);
                fs.Read(bytes, 0, entryIndex.Length);
                if (PasswordProtected)
                    bytes = DecryptBytes(bytes, Password, PasswordSalt);
            }

            return bytes;
        }

        /// <summary>
        /// Creates an Asterion archive from an array of source files.
        /// </summary>
        /// <param name="archiveFilePath">A path to the archive to create</param>
        /// <param name="password">Password to use, if any</param>
        /// <param name="filesToInclude">An array of paths to each individual file which must be included</param>
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
        /// (Protected) OnDispose override. Clears the index.
        /// </summary>
        protected override void OnDispose()
        {
            Index.Clear();
        }
    }
}

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
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Asterion.IO
{
    /// <summary>
    /// Abstract class from which all "file source" classes, which store files to be used by the game, are derived.
    /// </summary>
    public abstract class FileSource : IDisposable
    {
        /// <summary>
        /// Path to the file source. Can be a folder or a file, depending on the file source type.
        /// </summary>
        public string SourcePath { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">Path to the file or folder containing the files.</param>
        public FileSource(string path)
        {
            SourcePath = path;
        }

        /// <summary>
        /// Does the file exist in this file source?
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>True if the file exists, false otherwise</returns>
        public abstract bool FileExists(string file);

        /// <summary>
        /// Returns the bytes of a file stored in this file source.
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>An array of byte if the file exists, null otherwise</returns>
        public abstract byte[] GetFile(string file);

        /// <summary>
        /// (Protected) Called when the source is disposed. Should be used to free memory and close open files.
        /// </summary>
        protected abstract void OnDispose();

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose() { OnDispose(); }

        /// <summary>
        /// (Protected) Returns some bytes to salt a password.
        /// </summary>
        /// <param name="password">The password to salt</param>
        /// <returns>An array of bytes to use as salt</returns>
        protected static byte[] GenerateSalt(string password)
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
        /// (Protected) Encrypts an array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to encrypt</param>
        /// <param name="password">Password to use for encryption</param>
        /// <param name="passwordSalt">Bytes to use to salt the password</param>
        /// <returns>An encrypted array of bytes</returns>
        protected static byte[] EncryptBytes(byte[] bytes, string password, byte[] passwordSalt)
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
        /// (Protected) Decrypts an array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to decrypt</param>
        /// <param name="password">Password to use for decryption</param>
        /// <param name="passwordSalt">Bytes to use to salt the password</param>
        /// <returns>A decrypted array of bytes</returns>
        protected static byte[] DecryptBytes(byte[] bytes, string password, byte[] passwordSalt)
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
    }
}

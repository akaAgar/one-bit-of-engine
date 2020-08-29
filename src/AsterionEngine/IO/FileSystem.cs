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

using System.IO;
using System.Reflection;
using System.Text;

namespace Asterion.IO
{
    /// <summary>
    /// Provides access to media files used by Asterion Engine. Can load files from a variety of sources such as a folder or an encrypted archive.
    /// </summary>
    public sealed class FileSystem
    {
        /// <summary>
        /// (Private) The file source to use. Re
        /// </summary>
        private FileSource Source = null;

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        internal FileSystem()
        {
            SetFolderAsFileSource(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        /// <summary>
        /// Uses a folder as a the file source from now on.
        /// </summary>
        /// <param name="folderPath">Path to the folder where the files are stored</param>
        /// <returns>True if the folder exists, false otherwise</returns>
        public bool SetFolderAsFileSource(string folderPath)
        {
            if (!Directory.Exists(folderPath)) return false;
            CloseSource();
            Source = new FileSourceFolder(folderPath);
            return true;
        }

        /// <summary>
        /// Uses an Asterion archive as the file source from now on.
        /// </summary>
        /// <param name="filePath">Path to the archive file where the files are store.</param>
        /// <param name="password">The archive's password, if any</param>
        /// <returns>True if the file exists, false otherwise</returns>
        public bool SetArchiveAsFileSource(string filePath, string password = "")
        {
            if (!File.Exists(filePath)) return false;
            CloseSource();
            Source = new FileSourceArchive(filePath, password);
            return true;
        }

        /// <summary>
        /// (Private) Closes and destorys the current file source.
        /// </summary>
        private void CloseSource()
        {
            if (Source == null) return;

            Source.Dispose();
            Source = null;
        }

        /// <summary>
        /// Returns all bytes from a file in the current file source.
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>An array of bytes if the file exists, null otherwise</returns>
        public byte[] GetFile(string file)
        {
            if (!Source.FileExists(file)) return null;
            return Source.GetFile(file);
        }

        /// <summary>
        /// Returns a stream containing the bytes of a file in the current file source.
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>A stream if the file exists, null otherwise</returns>
        public Stream GetFileAsStream(string file)
        {
            byte[] buffer = Source.GetFile(file);
            if (buffer == null) return null;
            return new MemoryStream(buffer);
        }

        /// <summary>
        /// Does the file exists in the current file source?
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>True if the file exists, false otherwise</returns>
        public bool FileExists(string file)
        {
            return Source.FileExists(file);
        }

        /// <summary>
        /// Returns a file from the current file source, as a text string.
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <param name="textEncoding">Text encoding to use, or UTF-8 if none is provided</param>
        /// <returns>An string, or null if the file doesn't exist.</returns>
        internal string GetEntryAsText(string file, Encoding textEncoding = null)
        {
            if (textEncoding == null) textEncoding = Encoding.UTF8;
            byte[] bytes = GetFile(file);
            if (bytes == null) return null;

            return textEncoding.GetString(bytes);
        }

        /// <summary>
        /// (Internal) Destroys the file system.
        /// </summary>
        internal void Destroy()
        {
            CloseSource();
        }
    }
}

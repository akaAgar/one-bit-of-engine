/*
==========================================================================
This file is part of One Bit of Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
One Bit of Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
One Bit of Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with One Bit of Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using System.IO;

namespace OneBitOfEngine.IO
{
    /// <summary>
    /// File source using a folder as the file source.
    /// </summary>
    public class FileSourceFolder : FileSource
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">Path to the folder which stores the files</param>
        public FileSourceFolder(string path): base(path)
        {

        }

        /// <summary>
        /// Creates an file path by combining the path to the folder used as the file source and the required file name.
        /// </summary>
        /// <param name="file">Relative path to the file from the root of the file source folder</param>
        /// <returns>Path</returns>
        private string MakeFilePath(string file)
        {
            return Path.Combine(SourcePath, file);
        }

        /// <summary>
        /// Does the file exist in this file source?
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>True if the file exists, false otherwise</returns>
        public override bool FileExists(string file)
        {
            return File.Exists(MakeFilePath(file));
        }

        /// <summary>
        /// Returns the bytes of a file stored in this file source.
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>An array of byte if the file exists, null otherwise</returns>
        public override byte[] GetFile(string file)
        {
            string fileWithPath = MakeFilePath(file);
            if (!File.Exists(fileWithPath)) return null;
            return File.ReadAllBytes(fileWithPath);
        }

        /// <summary>
        /// (Protected) OnDispose override. Does nothing as this kind of file source has nothing to close or dispose of.
        /// </summary>
        protected override void OnDispose() { }
    }
}

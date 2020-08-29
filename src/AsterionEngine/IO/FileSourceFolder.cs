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

namespace Asterion.IO
{
    public sealed class FileSourceFolder : FileSource
    {
        internal override FileSystemSourceType SourceType { get { return FileSystemSourceType.Folder; } }

        internal FileSourceFolder(string path): base(path)
        {

        }

        private string MakeFilePath(string file)
        {
            return Path.Combine(SourcePath, file);
        }

        internal override bool FileExists(string file)
        {
            return File.Exists(MakeFilePath(file));
        }

        internal override byte[] GetFile(string file)
        {
            string fileWithPath = MakeFilePath(file);
            if (!FileExists(fileWithPath)) return null;

            return File.ReadAllBytes(fileWithPath);
        }
    }
}

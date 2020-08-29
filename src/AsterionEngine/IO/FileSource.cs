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

namespace Asterion.IO
{
    public abstract class FileSource
    {
        internal abstract FileSystemSourceType SourceType { get; }

        internal string SourcePath { get; private set; }

        internal FileSource(string path)
        {
            SourcePath = path;
        }

        internal abstract bool FileExists(string file);

        internal abstract byte[] GetFile(string file);

        internal virtual void Dispose()
        {

        }
    }
}
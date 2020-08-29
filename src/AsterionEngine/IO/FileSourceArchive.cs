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
    public sealed class FileSourceArchive : FileSource
    {
        /// <summary>
        /// Maximul length of an entry name.
        /// </summary>
        internal const int MAX_ENTRY_NAME_LENGTH = 32;

        /// <summary>
        /// Length of each entry in the index.
        /// </summary>
        private const int INDEX_ENTRY_LENGTH = MAX_ENTRY_NAME_LENGTH + 4; // name + int32 length

        /// <summary>
        /// Index of all entries stored in the file.
        /// </summary>
        private readonly Dictionary<string, ResourceArchiveIndex> Index = new Dictionary<string, ResourceArchiveIndex>(StringComparer.InvariantCultureIgnoreCase);


        internal override FileSystemSourceType SourceType { get { return FileSystemSourceType.Archive; } }

        internal FileSourceArchive(string path, string password = ""): base(path)
        {

        }

        internal override bool FileExists(string file)
        {
            return false;
        }

        internal override byte[] GetFile(string file)
        {
            return null;
        }
    }
}

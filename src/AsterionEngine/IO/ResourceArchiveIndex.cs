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

namespace Asterion.IO
{
    /// <summary>
    /// Data for an index entry in the ResourceAchive class
    /// </summary>
    internal struct ResourceArchiveIndex
    {
        /// <summary>
        /// Offset (in bytes) from the beginning of the file at which the entry is stored.
        /// </summary>
        internal readonly int Offset;
        
        /// <summary>
        /// Length of the entry, in bytes.
        /// </summary>
        internal readonly int Length;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="offset">Offset (in bytes) from the beginning of the file at which the entry is stored</param>
        /// <param name="length">Length of the entry, in bytes</param>
        internal ResourceArchiveIndex(int offset, int length)
        {
            Offset = Math.Max(0, offset);
            Length = Math.Max(0, length);
        }
    }
}

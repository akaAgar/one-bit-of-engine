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

namespace Asterion.Core
{
    /// <summary>
    /// Enumerates available window borders.
    /// </summary>
    public enum GameWindowBorder
    {
        /// <summary>
        /// The window has a resizable border. A window with a resizable border can be resized by the user or programmatically.
        /// </summary>
        Resizable = 0,
        /// <summary>
        /// The window has a fixed border. A window with a fixed border can only be resized programmatically.
        /// </summary>
        Fixed = 1,
        /// <summary>
        /// The window does not have a border. A window with a hidden border can only be resized programmatically.
        /// </summary>
        Hidden = 2
    }
}

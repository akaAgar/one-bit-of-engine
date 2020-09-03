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
    
    public enum TileVFX
    {
        // Must match values in the TilesShader.frag fragment shader

        /// <summary>
        /// No special effect.
        /// </summary>
        None = 0,

        /// <summary>
        /// Slow glow (1 hz).
        /// </summary>
        GlowSlow,

        /// <summary>
        /// Average glow (2 hz).
        /// </summary>
        GlowAverage,

        /// <summary>
        /// Fast glow (4 hz).
        /// </summary>
        GlowFast,

        /// <summary>
        /// Slow blink (2 hz).
        /// </summary>
        BlinkSlow,

        /// <summary>
        /// Average blink (4 hz).
        /// </summary>
        BlinkAverage,

        /// <summary>
        /// Fast blink (8 hz).
        /// </summary>
        BlinkFast,

        /// <summary>
        /// Negative.
        /// </summary>
        Negative,

        Test
    }
}

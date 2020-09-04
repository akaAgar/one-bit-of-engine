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

namespace OneBitOfEngine.Core
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
        /// Medium glow (2 hz).
        /// </summary>
        GlowMedium,

        /// <summary>
        /// Fast glow (4 hz).
        /// </summary>
        GlowFast,

        /// <summary>
        /// Slow blink (2 hz).
        /// </summary>
        BlinkSlow,

        /// <summary>
        /// Medium blink (4 hz).
        /// </summary>
        BlinkMedium,

        /// <summary>
        /// Fast blink (8 hz).
        /// </summary>
        BlinkFast,

        /// <summary>
        /// Negative.
        /// </summary>
        Negative,

        /// <summary>
        /// Negative, blink slow (1 hz).
        /// </summary>
        NegativeBlinkSlow,

        /// <summary>
        /// Negative, blink medium (2 hz).
        /// </summary>
        NegativeBlinkMedium,

        /// <summary>
        /// Negative, blink fast (4 hz).
        /// </summary>
        NegativeBlinkFast,

        NegativeGlowSlow,
        NegativeGlowMedium,
        NegativeGlowFast,

        /// <summary>
        /// Slanted to the right.
        /// </summary>
        SlantedRight,

        /// <summary>
        /// Slanted to the left.
        /// </summary>
        SlantedLeft,

        /// <summary>
        /// Slow top oscillation (1 hz).
        /// </summary>
        OscillateTopSlow,

        /// <summary>
        /// Medium top oscillation (2 hz).
        /// </summary>
        OscillateTopMedium,

        /// <summary>
        /// Fast top oscillation (4 hz).
        /// </summary>
        OscillateTopFast,

        /// <summary>
        /// Slow top oscillation (1 hz).
        /// </summary>
        OscillateBottomSlow,

        /// <summary>
        /// Medium bottom oscillation (2 hz).
        /// </summary>
        OscillateBottomMedium,

        /// <summary>
        /// Fast bottom oscillation (4 hz).
        /// </summary>
        OscillateBottomFast,

        WaveHorizontalSlow,
        WaveHorizontalMedium,
        WaveHorizontalFast,

        WaveVerticalSlow,
        WaveVerticalMedium,
        WaveVerticalFast,

        TEST
    }
}

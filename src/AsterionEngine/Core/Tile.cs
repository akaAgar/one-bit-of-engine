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
    /// Describes a tile to be displayed on the screen.
    /// </summary>
    public struct Tile
    {
        /// <summary>
        /// An empty tile: tile index 0, tilemap 0, black (0,0,0) color.
        /// </summary>
        public static Tile Empty { get; } = new Tile(0, RGBColor.Black);

        /// <summary>
        /// Index of the tile in the tilemap.
        /// </summary>
        public int TileIndex { get; }
        
        /// <summary>
        /// Color of the tile.
        /// </summary>
        public RGBColor Color { get; }
        
        /// <summary>
        /// Tilemap from which to load the tile.
        /// </summary>
        public int Tilemap { get; }

        /// <summary>
        /// Special shader effect to use when drawing this tile.
        /// </summary>
        public TileVFX Effect { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tileIndex">Index of the tile in the tilemap</param>
        /// <param name="color">Color of the tile</param>
        /// <param name="tilemap">Index of the tilemap</param>
        /// <param name="effect">Special shader effect to use when drawing tile</param>
        public Tile(int tileIndex, RGBColor color, int tilemap = 0, TileVFX effect = TileVFX.None)
        {
            TileIndex = tileIndex;
            Color = color;
            Tilemap = tilemap;
            Effect = effect;
        }
    }
}

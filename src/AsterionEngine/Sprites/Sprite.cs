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

using Asterion.Core;

namespace Asterion.Sprites
{
    internal struct Sprite
    {
        internal string Name { get; }
        internal SpriteType SpriteType { get; }
        internal int Tile { get; }
        internal int TileCount { get; }
        internal RGBColor Color { get; }
        internal int Tilemap { get; }
        internal float FrameDuration { get; }
        internal Position[] Positions { get; }

        internal Sprite(string name, SpriteType animType, int tile, int tileCount, RGBColor color, int tilemap, float time, params Position[] positions)
        {
            Name = name;
            SpriteType = animType;
            Tile = tile;
            TileCount = tileCount;
            Color = color;
            Tilemap = tilemap;
            FrameDuration = time;
            Positions = positions;
        }
    }
}

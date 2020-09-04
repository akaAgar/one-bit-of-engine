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
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Asterion.Sprites
{
    internal struct Sprite
    {
        internal string Name { get; }
        internal SpriteType AnimType { get; }
        internal int[] Tiles { get; }
        internal RGBColor Color { get; }
        internal int Tilemap { get; }
        internal float FrameTime { get; }
        internal Position[] Positions { get; }

        internal Sprite(string name, SpriteType animType, int[] tiles, RGBColor color, int tilemap, float time, params Position[] positions)
        {
            Name = name;
            AnimType = animType;
            Tiles = tiles;
            Color = color;
            Tilemap = tilemap;
            FrameTime = time;
            Positions = positions;

            switch (AnimType)
            {
                case SpriteType.Moving:
                    Positions = GetPointsBetween(positions[0], positions[1]);
                    FrameTime = time / Positions.Length;
                    break;
            }
        }

        private Position[] GetPointsBetween(Position start, Position end)
        {
            List<Position> points = new List<Position>();

            float length = (float)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            float dX = (end.X - start.X) / length;
            float dY = (end.Y - start.Y) / length;

            for (float f = 0; f <= length; f += 1f)
                points.Add(new Position((int)(start.X + dX * f), (int)(start.Y + dY * f)));

            return points.ToArray();
        }
    }
}

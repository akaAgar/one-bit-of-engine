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

using System;

namespace Asterion
{
    public struct Area : ICloneable, IEquatable<Area>
    {
        public static Area Zero { get; } = new Area(0, 0, 0, 0);

        public Position Position { get; }
        public Dimension Dimension { get; }

        public int X { get { return Position.X; } }
        public int Y { get { return Position.Y; } }

        public int Width { get { return Dimension.Width; } }
        public int Height { get { return Dimension.Height; } }

        public int Left { get { return Position.X; } }
        public int Top { get { return Position.Y; } }

        public int Right { get { return Position.X + Dimension.Width; } }
        public int Bottom { get { return Position.Y + Dimension.Height; } }

        public Area(Position position, Dimension dimension)
        {
            Position = position;
            Dimension = dimension;
        }

        public Area(int x, int y, int width, int height)
        {
            Position = new Position(x, y);
            Dimension = new Dimension(width, height);
        }

        public bool Contains(Position position)
        {
            return Contains(position.X, position.Y);
        }

        public bool Contains(int x, int y)
        {
            return !((x < Left) || (y < Top) || (x >= Right) || (y >= Bottom));
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() + 7 * Dimension.GetHashCode();
        }

        public override string ToString()
        {
            return $"{X},{Y} {Width}×{Height}";
        }

        /// <summary>
        /// == operator override. Returns true if both rectangles have the same X, Y, Width and Height values.
        /// </summary>
        /// <param name="r1">A rectangle</param>
        /// <param name="r2">Another rectangle</param>
        /// <returns>True if both rectangles have the same X, Y, Width and Height values</returns>
        public static bool operator ==(Area r1, Area r2) { return r1.Equals(r2); }

        /// <summary>
        /// != operator override. Returns true if both rectangles DO NOT have the same X, Y, Width and Height values.
        /// </summary>
        /// <param name="r1">A rectangle</param>
        /// <param name="r2">Another rectangle</param>
        /// <returns>True if both rectangles DO NOT have the same X, Y, Width and Height values</returns>
        public static bool operator !=(Area r1, Area r2) { return r1.Equals(r2); }

        /// <summary>
        /// Returns true if both rectangles have the same X, Y, Width and Height values.
        /// </summary>
        /// <param name="other">Another rectangle</param>
        /// <returns>True if both rectangles have the same X, Y, Width and Height values</returns>
        public bool Equals(Area other)
        {
            return (Position == other.Position) && (Dimension == other.Dimension);
        }

        /// <summary>
        /// Returns true the other object is a reactangle and if both rectangles have the same X, Y, Width and Height values.
        /// </summary>
        /// <param name="obj">Another object</param>
        /// <returns>True the other object is a reactangle and if both rectangles have the same X, Y, Width and Height values</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Area)) return false;
            return Equals((Area)obj);
        }

        /// <summary>
        /// Creates a copy of this Rectangle object.
        /// </summary>
        /// <returns>A copy of this object</returns>
        public object Clone() { return new Area(Position, Dimension); }
    }
}

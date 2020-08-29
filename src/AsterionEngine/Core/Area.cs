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

namespace Asterion.Core
{
    /// <summary>
    /// Describes a rectangular area from a set of X,Y coordinates, a width and and height.
    /// </summary>
    public struct Area : ICloneable, IEquatable<Area>
    {
        /// <summary>
        /// An area with X, Y, Width and Height equals to zero.
        /// </summary>
        public static Area Zero { get; } = new Area(0, 0, 0, 0);

        /// <summary>
        /// X coordinate of the left bound of the area.
        /// </summary>
        public int X { get { return Position.X; } }

        /// <summary>
        /// Y coordinate of the top bound of the area.
        /// </summary>
        public int Y { get { return Position.Y; } }

        /// <summary>
        /// Width of the area.
        /// </summary>
        public int Width { get { return Dimension.Width; } }

        /// <summary>
        /// Height of the area.
        /// </summary>
        public int Height { get { return Dimension.Height; } }

        /// <summary>
        /// X coordinate of the left bound of the area.
        /// </summary>
        public int Left { get { return Position.X; } }
        
        /// <summary>
        /// Y coordinate of the top bound of the area.
        /// </summary>
        public int Top { get { return Position.Y; } }

        /// <summary>
        /// X coordinate of the right bound of the area.
        /// </summary>
        public int Right { get { return Position.X + Dimension.Width; } }

        /// <summary>
        /// Y coordinate of the bottom bound of the area.
        /// </summary>
        public int Bottom { get { return Position.Y + Dimension.Height; } }

        /// <summary>
        /// (Private) A position holding the X and Y coordinates of the upper-left corner of the area.
        /// </summary>
        private readonly Position Position;

        /// <summary>
        /// (Private) A dimension holding the width and height of the area.
        /// </summary>
        private readonly Dimension Dimension;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position">Position of the upper-left corner of the area</param>
        /// <param name="dimension">Dimension of the area</param>
        public Area(Position position, Dimension dimension)
        {
            Position = position;
            Dimension = dimension;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">X coordinate of the left of the area</param>
        /// <param name="y">Y coordinate of the top of the area</param>
        /// <param name="width">Width of the area</param>
        /// <param name="height">Height of the area</param>
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

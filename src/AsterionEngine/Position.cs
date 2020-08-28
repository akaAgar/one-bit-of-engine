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
using System.Drawing;
using System.Globalization;

namespace Asterion
{
    /// <summary>
    /// Stores a pair of integer coordinates, one for the X-axis and one for the Y-axis.
    /// </summary>
    public struct Position : ICloneable, IEquatable<Position>
    {
        /// <summary>
        /// An instance of Position with both coordinates set to zero.
        /// </summary>
        public static Position Zero { get; } = new Position(0, 0);

        /// <summary>
        /// The X coordinate.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// The Y coordinate.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="both">A value to use for both the X and the Y coordinate</param>
        public Position(int both) { X = both; Y = both; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        public Position(int x, int y) { X = x; Y = y; }
        
        /// <summary>
        /// (Internal) Constructor
        /// </summary>
        /// <param name="point">A System.Drawing.Point to copy from.</param>
        internal Position(Point point) { X = point.X; Y = point.Y; }

        /// <summary>
        /// == operator. Returns true if both positions are equal, false otherwise.
        /// </summary>
        /// <param name="p1">A position</param>
        /// <param name="p2">Another position</param>
        /// <returns>True if both positions are equal, false otherwise</returns>
        public static bool operator ==(Position p1, Position p2) { return p1.Equals(p2); }

        /// <summary>
        /// != operator. Returns true if both positions are not equal, false if they are.
        /// </summary>
        /// <param name="p1">A position</param>
        /// <param name="p2">Another position</param>
        /// <returns>True if both positions are not equal, false if they are</returns>
        public static bool operator !=(Position p1, Position p2) { return !p1.Equals(p2); }

        /// <summary>
        /// + operator. A new position where X and Y coordinates are the sum of X and Y axis of both positions.
        /// </summary>
        /// <param name="p1">A position</param>
        /// <param name="p2">Another position</param>
        /// <returns>A new position where X and Y coordinates are the sum of X and Y axis of both positions</returns>
        public static Position operator +(Position p1, Position p2) { return new Position(p1.X + p2.X, p1.Y + p2.Y); }

        /// <summary>
        /// - operator. A new position where X and Y coordinates are position1's X and Y coordinates minus position2's X and Y of both positions.
        /// </summary>
        /// <param name="p1">A position</param>
        /// <param name="p2">Another position</param>
        /// <returns>A new position where X and Y coordinates are position1's X and Y coordinates minus position2's X and Y of both positions</returns>
        public static Position operator -(Position p1, Position p2) { return new Position(p1.X - p2.X, p1.Y - p2.Y); }

        /// <summary>
        /// * operator. Returns a new position where the X and Y axis have been multiplied by the multiplier.
        /// </summary>
        /// <param name="pos">A position</param>
        /// <param name="m">A floating-point multiplier</param>
        /// <returns>A new position where the X and Y axis have been multiplied by the multiplier.</returns>
        public static Position operator *(Position pos, float m) { return new Position((int)(pos.X * m), (int)(pos.Y * m)); }

        /// <summary>
        /// Checks if this position is the same as another Position (both X and Y coordinates have the same values)
        /// </summary>
        /// <param name="other">Another position</param>
        /// <returns>True if both positions are equal, false otherwhise</returns>
        public bool Equals(Position other)
        {
            return (X == other.X) && (Y == other.Y);
        }

        /// <summary>
        /// Checks if this position is the same as another object.
        /// </summary>
        /// <param name="obj">Another object</param>
        /// <returns>True if obj is a position and both positions are equal, false otherwhise</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Position)) return false;
            return Equals((Position)obj);
        }

        /// <summary>
        /// Returns the hashcode for this object.
        /// </summary>
        /// <returns>An integer hashcode</returns>
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + X.GetHashCode();
            hash = (hash * 7) + Y.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Converts the position to a string in the "X,Y" format. (e.g. "4,8")
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return X.ToString(NumberFormatInfo.InvariantInfo) + "," + Y.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts the position to a string in the "X,Y" format. (e.g. "4,8")
        /// </summary>
        /// <param name="format">The custom format string</param>
        /// <returns>A string</returns>
        public string ToString(string format)
        {
            return X.ToString(format, NumberFormatInfo.InvariantInfo) + "," + Y.ToString(format, NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// (Internal) Converts this position to a System.Drawing.Point.
        /// </summary>
        /// <returns>A System.Drawing.Point object</returns>
        internal Point ToPoint()
        {
            return new Point(X, Y);
        }

        /// <summary>
        /// Creates a copy of this Position object.
        /// </summary>
        /// <returns>A copy of this object</returns>
        public object Clone() { return new Position(X, Y); }

        /// <summary>
        /// Returns the pythagorean distance between two positions.
        /// </summary>
        /// <param name="p1">A position</param>
        /// <param name="p2">Another position</param>
        /// <returns>The distance, as a float</returns>
        public static float Distance(Position p1, Position p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        /// <summary>
        /// Returns the pythagorean distance between this position and another.
        /// </summary>
        /// <param name="other">Another position</param>
        /// <returns>The distance, as a float</returns>
        public float Distance(Position other)
        {
            return Distance(this, other);
        }

        /// <summary>
        /// Returns a quick approximation of the distance between two positions using the Angband algorithm (distance = long axis + half of short axis).
        /// </summary>
        /// <param name="p1">A position</param>
        /// <param name="p2">Another position</param>
        /// <returns>An integer approximate value of the distance</returns>
        public static int AngbandDistance(Position p1, Position p2)
        {
            int xDist = Math.Abs(p1.X - p2.X);
            int yDist = Math.Abs(p1.Y - p2.Y);

            int longAxis = Math.Max(xDist, yDist);
            int shortAxis = Math.Max(xDist, yDist);

            return longAxis + shortAxis / 2;
        }

        /// <summary>
        /// Returns a quick approximation of the distance between this position and another one using the Angband algorithm (distance = long axis + half of short axis).
        /// </summary>
        /// <param name="other">Another position</param>
        /// <returns>An integer approximate value of the distance</returns>
        public int AngbandDistance(Position other)
        {
            return AngbandDistance(this, other);
        }
    }
}

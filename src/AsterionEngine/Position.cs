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
    public struct Position : ICloneable, IEquatable<Position>
    {
        public static readonly Position Zero = new Position(0, 0);

        public readonly int X;
        public readonly int Y;

        public Position(int both) { X = both; Y = both; }
        public Position(int x, int y) { X = x; Y = y; }
        internal Position(Point pt) { X = pt.X; Y = pt.Y; }

        public static bool operator ==(Position p1, Position p2) { return p1.Equals(p2); }
        public static bool operator !=(Position p1, Position p2) { return !p1.Equals(p2); }
        public static Position operator +(Position p1, Position p2) { return new Position(p1.X + p2.X, p1.Y + p2.Y); }
        public static Position operator -(Position p1, Position p2) { return new Position(p1.X - p2.X, p1.Y - p2.Y); }
        public static Position operator *(Position pos, int m) { return new Position(pos.X * m, pos.Y * m); }
        public static Position operator *(Position p1, Position p2) { return new Position(p1.X * p2.X, p1.Y * p2.Y); }
        public static Position operator /(Position p1, Position p2) { return new Position(p1.X / p2.X, p1.Y / p2.Y); }
        public static Position operator /(Position pos, int m) { return new Position(pos.X / m, pos.Y / m); }

        public bool Equals(Position other)
        {
            return (X == other.X) && (Y == other.Y);
        }

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

        public override string ToString()
        { return X.ToString(NumberFormatInfo.InvariantInfo) + "," + Y.ToString(NumberFormatInfo.InvariantInfo); }
        
        public string ToString(string format)
        { return X.ToString(format, NumberFormatInfo.InvariantInfo) + "," + Y.ToString(format, NumberFormatInfo.InvariantInfo); }

        internal Point ToPoint() { return new Point(X, Y); }

        public object Clone() { return new Position(X, Y); }
    }
}

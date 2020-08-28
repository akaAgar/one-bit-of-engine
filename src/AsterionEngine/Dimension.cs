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
    public struct Dimension : ICloneable, IEquatable<Dimension>
    {
        public static readonly Dimension Zero = new Dimension(0, 0);

        public readonly int Width;
        public readonly int Height;

        public Dimension(int both) { Width = both; Height = both; }
        public Dimension(int width, int height) { Width = width; Height = height; }
        internal Dimension(Size size) { Width = size.Width; Height = size.Height; }

        public static bool operator ==(Dimension p1, Dimension p2) { return p1.Equals(p2); }
        public static bool operator !=(Dimension p1, Dimension p2) { return !p1.Equals(p2); }
        public static Dimension operator +(Dimension p1, Dimension p2) { return new Dimension(p1.Width + p2.Width, p1.Height + p2.Height); }
        public static Dimension operator -(Dimension p1, Dimension p2) { return new Dimension(p1.Width - p2.Width, p1.Height - p2.Height); }
        public static Dimension operator *(Dimension pos, int m) { return new Dimension(pos.Width * m, pos.Height * m); }
        public static Dimension operator *(Dimension p1, Dimension p2) { return new Dimension(p1.Width * p2.Width, p1.Height * p2.Height); }
        public static Dimension operator /(Dimension p1, Dimension p2) { return new Dimension(p1.Width / p2.Width, p1.Height / p2.Height); }
        public static Dimension operator /(Dimension pos, int m) { return new Dimension(pos.Width / m, pos.Height / m); }

        public bool Equals(Dimension other)
        {
            return (Width == other.Width) && (Height == other.Height);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Dimension)) return false;
            return Equals((Dimension)obj);
        }

        /// <summary>
        /// Returns the hashcode for this object.
        /// </summary>
        /// <returns>An integer hashcode</returns>
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Width.GetHashCode();
            hash = (hash * 7) + Height.GetHashCode();
            return hash;
        }

        public override string ToString()
        { return Width.ToString(NumberFormatInfo.InvariantInfo) + "×" + Width.ToString(NumberFormatInfo.InvariantInfo); }
        
        public string ToString(string format)
        { return Height.ToString(format, NumberFormatInfo.InvariantInfo) + "×" + Height.ToString(format, NumberFormatInfo.InvariantInfo); }

        internal Size ToSize() { return new Size(Width, Height); }

        public object Clone() { return new Dimension(Width, Height); }
    }
}

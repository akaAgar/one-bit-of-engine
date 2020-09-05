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

using System;
using System.Drawing;
using System.Globalization;

namespace OneBitOfEngine.Core
{
    public struct Dimension : ICloneable, IEquatable<Dimension>
    {
        public static Dimension Zero { get; } = new Dimension(0, 0);
        public static Dimension One { get; } = new Dimension(1, 1);

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Dimension(int both)
        {
            both = Math.Max(0, both);

            Width = both; Height = both;
        }
        
        public Dimension(int width, int height)
        {
            Width = Math.Max(0, width);
            Height = Math.Max(0, height);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="widthHeightString">A string in the "WIDTHxHEIGHT" or "WIDTH×HEIGHT" format containing the width and height in the <see cref="NumberFormatInfo.InvariantInfo"/> format</param>
        public Dimension(string widthHeightString)
        {
            Width = 0; Height = 0;

            if (string.IsNullOrEmpty(widthHeightString)) return;
            string[] widthHeightStringValues = widthHeightString.Trim().Split('x', '×', 'X');
            if (widthHeightStringValues.Length < 2) return;

            try
            {
                Width = Math.Max(0, Convert.ToInt32(widthHeightStringValues[0].Trim(), NumberFormatInfo.InvariantInfo));
                Height = Math.Max(0, Convert.ToInt32(widthHeightStringValues[1].Trim(), NumberFormatInfo.InvariantInfo));
            }
            catch (Exception)
            {
                Width = 0;
                Height = 0;
            }
        }

        internal Dimension(Size size)
        {
            Width = Math.Max(0, size.Width);
            Height = Math.Max(0, size.Height);
        }

        public bool Contains(int x, int y)
        {
            return Contains(new Position(x, y));
        }

        public bool Contains(Position position)
        {
            return new Area(Position.Zero, this).Contains(position);
        }

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

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

using OpenTK.Graphics;
using System;
using System.Drawing;
using System.Globalization;

namespace Asterion
{
    /// <summary>
    /// Stores a R,G,B color 24-bit color.
    /// </summary>
    public struct RGBColor : ICloneable, IEquatable<RGBColor>
    {
        /// <summary>
        /// The red component of the color, as an integer in the 0-255 range.
        /// </summary>
        public int R { get; private set; }

        /// <summary>
        /// The green component of the color, as an integer in the 0-255 range.
        /// </summary>
        public int G { get; private set; }

        /// <summary>
        /// The blue component of the color, as an integer in the 0-255 range.
        /// </summary>
        public int B { get; private set; }

        /// <summary>
        /// The red component of the color, as a float in the 0.0-1.0 range.
        /// </summary>
        public float Rf { get { return R / 255f; } }

        /// <summary>
        /// The green component of the color, as a float in the 0.0-1.0 range.
        /// </summary>
        public float Gf { get { return G / 255f; } }

        /// <summary>
        /// The blue component of the color, as a float in the 0.0-1.0 range.
        /// </summary>
        public float Bf { get { return B / 255f; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="grayscale">An integer value in the 0-255 range to use for all three (R,G,B) channels.</param>
        public RGBColor(int grayscale)
        {
            grayscale = BoundChannelValue(grayscale);

            R = grayscale;
            G = grayscale;
            B = grayscale;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="grayscale">A floating-point value in the 0.0-1.0 range to use for all three (R,G,B) channels.</param>
        public RGBColor(float grayscale)
        {
            int grayscaleI = BoundChannelValue((int)(grayscale * 255));

            R = grayscaleI;
            G = grayscaleI;
            B = grayscaleI;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="red">An integer value in the 0-255 range to use for the red channel.</param>
        /// <param name="green">An integer value in the 0-255 range to use for the green channel.</param>
        /// <param name="blue">An integer value in the 0-255 range to use for the blue channel.</param>
        public RGBColor(int red, int green, int blue)
        {
            R = BoundChannelValue(red);
            G = BoundChannelValue(green);
            B = BoundChannelValue(blue);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="red">A floating-point value in the 0.0-1.0 range to use for the red channel.</param>
        /// <param name="green">A floating-point in the 0.0-1.0 range to use for the green channel.</param>
        /// <param name="blue">A floating-point in the 0.0-1.0 range to use for the blue channel.</param>
        public RGBColor(float red, float green, float blue)
        {
            R = BoundChannelValue((int)(red * 255));
            G = BoundChannelValue((int)(green * 255));
            B = BoundChannelValue((int)(blue * 255));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="knownColor">A known color, found in the KnownRGBColor enum.</param>
        public RGBColor(KnownRGBColor knownColor)
        {
            try
            {
                Color color = Color.FromKnownColor((KnownColor)knownColor);

                R = color.R;
                G = color.G;
                B = color.B;
            }
            catch (Exception)
            {
                R = 0; G = 0; B = 0;
            }
        }

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        /// <param name="color">A System.Drawing.Color to copy R,G,B values from.</param>
        internal RGBColor(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
        }

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        /// <param name="color">An OpenTK.Graphics.Color to copy R,G,B values from.</param>
        internal RGBColor(Color4 color)
        {
            R = BoundChannelValue((int)(color.R * 255));
            G = BoundChannelValue((int)(color.G * 255));
            B = BoundChannelValue((int)(color.B * 255));
        }

        /// <summary>
        /// (Private) Makes sure an integer value is in the 0-255 range.
        /// </summary>
        /// <param name="value">An integer value</param>
        /// <returns>The value, bounded in the 0-255 range</returns>
        private static int BoundChannelValue(int value) { return Math.Max(0, Math.Min(255, value)); }

        /// <summary>
        /// == operator override.
        /// </summary>
        /// <param name="c1">An RGBColor object</param>
        /// <param name="c2">Another RGBColor object</param>
        /// <returns>True if both objects are equal, false otherwise</returns>
        public static bool operator ==(RGBColor c1, RGBColor c2) { return c1.Equals(c2); }

        /// <summary>
        /// != operator override.
        /// </summary>
        /// <param name="c1">An RGBColor object</param>
        /// <param name="c2">Another RGBColor object</param>
        /// <returns>True if both objects are not equal, false if they are</returns>
        public static bool operator !=(RGBColor c1, RGBColor c2) { return !c1.Equals(c2); }

        /// <summary>
        /// Checks if this RGBColor is the same as another RGBColor (all three R,G,B channels have the same values)
        /// </summary>
        /// <param name="other">Another RGBColor</param>
        /// <returns>True if both colors are equal, false otherwhise</returns>
        public bool Equals(RGBColor other)
        {
            return (R == other.R) && (G == other.G) && (B == other.B);
        }

        /// <summary>
        /// Checks if this RGBColor is the same as another object.
        /// </summary>
        /// <param name="obj">Another object</param>
        /// <returns>True if obj is an RGBColor and both colors are equal, false otherwhise</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is RGBColor)) return false;
            return Equals((RGBColor)obj);
        }

        /// <summary>
        /// Returns the hashcode for this object.
        /// </summary>
        /// <returns>An integer hashcode</returns>
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + R.GetHashCode();
            hash = (hash * 7) + G.GetHashCode();
            hash = (hash * 7) + B.GetHashCode();
            return hash;
        }

        /// <summary>
        /// (Internal) Converts the color to an OpenTK color.
        /// </summary>
        /// <returns>An OpenTK color</returns>
        internal Color4 ToColor4() { return new Color4(Rf, Gf, Bf, 1.0f); }

        /// <summary>
        /// (Internal) Converts the color to a DotNet color.
        /// </summary>
        /// <returns>A DotNet color</returns>
        internal Color ToColor() { return Color.FromArgb(255, R, G, B); }

        /// <summary>
        /// Converts the color to a string in the "R,G,B" format. (e.g. "128,64,32")
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return R.ToString(NumberFormatInfo.InvariantInfo) + "," + G.ToString(NumberFormatInfo.InvariantInfo) + "," + B.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts the color to a string in the "R,G,B" format. (e.g. "128,64,32")
        /// </summary>
        /// <param name="format">The custom format string</param>
        /// <returns>A string</returns>
        public string ToString(string format)
        {
            return R.ToString(format, NumberFormatInfo.InvariantInfo) + "," + G.ToString(format, NumberFormatInfo.InvariantInfo) + "," + B.ToString(format, NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts the color to a string in the "#RRGGBB" format, with RGB channels as hex values. (e.g. "#ff8844")
        /// </summary>
        /// <returns>A string</returns>
        public string ToHexString()
        {
            return $"#{R:X2}{G:X2}{B:X2}";
        }

        /// <summary>
        /// Creates a copy of the RGBColor object.
        /// </summary>
        /// <returns>A copy of this object</returns>
        public object Clone() { return new RGBColor(R, G, B); }
    }
}

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

using OpenTK.Graphics;
using System;
using System.Drawing;
using System.Globalization;

namespace Asterion.Core
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
        /// <param name="preset">A preset color from the PresetColor enumeration</param>
        public RGBColor(PresetColor preset)
        {
            Color color = Color.FromKnownColor((KnownColor)preset);

            R = color.R;
            G = color.G;
            B = color.B;
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
        private static int BoundChannelValue(int value) { return AsterionTools.Clamp(value, 0, 255); }

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
        /// * operator override.
        /// </summary>
        /// <param name="color1">An RGBColor object</param>
        /// <param name="color2">Another RGBColor object</param>
        /// <returns>The product of colors c1 and c2</returns>
        public static RGBColor operator *(RGBColor color1, RGBColor color2)
        { return new RGBColor(color1.Rf * color2.Rf, color1.Gf * color2.Gf, color1.Bf * color2.Bf); }

        /// <summary>
        /// * operator override.
        /// </summary>
        /// <param name="color">An RGBColor object</param>
        /// <param name="f">A floating-point value by which to multiply each color channel of Color color</param>
        /// <returns>The product of colors c1 and c2</returns>
        public static RGBColor operator *(RGBColor color, float f)
        { return new RGBColor(color.Rf * f, color.Gf * f, color.Bf * f); }

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
        /// Creates a copy of this RGBColor object.
        /// </summary>
        /// <returns>A copy of this object</returns>
        public object Clone() { return new RGBColor(R, G, B); }

        /// <summary>
        /// The preset color with (R, G, B) = (240,248,255).
        /// <\summary>
        public static RGBColor AliceBlue { get; } = new RGBColor(240, 248, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (250,235,215).
        /// <\summary>
        public static RGBColor AntiqueWhite { get; } = new RGBColor(250, 235, 215);
        /// <summary>
        /// The preset color with (R, G, B) = (0,255,255).
        /// <\summary>
        public static RGBColor Aqua { get; } = new RGBColor(0, 255, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (127,255,212).
        /// <\summary>
        public static RGBColor Aquamarine { get; } = new RGBColor(127, 255, 212);
        /// <summary>
        /// The preset color with (R, G, B) = (240,255,255).
        /// <\summary>
        public static RGBColor Azure { get; } = new RGBColor(240, 255, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (245,245,220).
        /// <\summary>
        public static RGBColor Beige { get; } = new RGBColor(245, 245, 220);
        /// <summary>
        /// The preset color with (R, G, B) = (255,228,196).
        /// <\summary>
        public static RGBColor Bisque { get; } = new RGBColor(255, 228, 196);
        /// <summary>
        /// The preset color with (R, G, B) = (0,0,0).
        /// <\summary>
        public static RGBColor Black { get; } = new RGBColor(0, 0, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (255,235,205).
        /// <\summary>
        public static RGBColor BlanchedAlmond { get; } = new RGBColor(255, 235, 205);
        /// <summary>
        /// The preset color with (R, G, B) = (0,0,255).
        /// <\summary>
        public static RGBColor Blue { get; } = new RGBColor(0, 0, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (138,43,226).
        /// <\summary>
        public static RGBColor BlueViolet { get; } = new RGBColor(138, 43, 226);
        /// <summary>
        /// The preset color with (R, G, B) = (165,42,42).
        /// <\summary>
        public static RGBColor Brown { get; } = new RGBColor(165, 42, 42);
        /// <summary>
        /// The preset color with (R, G, B) = (222,184,135).
        /// <\summary>
        public static RGBColor BurlyWood { get; } = new RGBColor(222, 184, 135);
        /// <summary>
        /// The preset color with (R, G, B) = (95,158,160).
        /// <\summary>
        public static RGBColor CadetBlue { get; } = new RGBColor(95, 158, 160);
        /// <summary>
        /// The preset color with (R, G, B) = (127,255,0).
        /// <\summary>
        public static RGBColor Chartreuse { get; } = new RGBColor(127, 255, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (210,105,30).
        /// <\summary>
        public static RGBColor Chocolate { get; } = new RGBColor(210, 105, 30);
        /// <summary>
        /// The preset color with (R, G, B) = (255,127,80).
        /// <\summary>
        public static RGBColor Coral { get; } = new RGBColor(255, 127, 80);
        /// <summary>
        /// The preset color with (R, G, B) = (100,149,237).
        /// <\summary>
        public static RGBColor CornflowerBlue { get; } = new RGBColor(100, 149, 237);
        /// <summary>
        /// The preset color with (R, G, B) = (255,248,220).
        /// <\summary>
        public static RGBColor Cornsilk { get; } = new RGBColor(255, 248, 220);
        /// <summary>
        /// The preset color with (R, G, B) = (220,20,60).
        /// <\summary>
        public static RGBColor Crimson { get; } = new RGBColor(220, 20, 60);
        /// <summary>
        /// The preset color with (R, G, B) = (0,255,255).
        /// <\summary>
        public static RGBColor Cyan { get; } = new RGBColor(0, 255, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (0,0,139).
        /// <\summary>
        public static RGBColor DarkBlue { get; } = new RGBColor(0, 0, 139);
        /// <summary>
        /// The preset color with (R, G, B) = (0,139,139).
        /// <\summary>
        public static RGBColor DarkCyan { get; } = new RGBColor(0, 139, 139);
        /// <summary>
        /// The preset color with (R, G, B) = (184,134,11).
        /// <\summary>
        public static RGBColor DarkGoldenrod { get; } = new RGBColor(184, 134, 11);
        /// <summary>
        /// The preset color with (R, G, B) = (169,169,169).
        /// <\summary>
        public static RGBColor DarkGray { get; } = new RGBColor(169, 169, 169);
        /// <summary>
        /// The preset color with (R, G, B) = (0,100,0).
        /// <\summary>
        public static RGBColor DarkGreen { get; } = new RGBColor(0, 100, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (189,183,107).
        /// <\summary>
        public static RGBColor DarkKhaki { get; } = new RGBColor(189, 183, 107);
        /// <summary>
        /// The preset color with (R, G, B) = (139,0,139).
        /// <\summary>
        public static RGBColor DarkMagenta { get; } = new RGBColor(139, 0, 139);
        /// <summary>
        /// The preset color with (R, G, B) = (85,107,47).
        /// <\summary>
        public static RGBColor DarkOliveGreen { get; } = new RGBColor(85, 107, 47);
        /// <summary>
        /// The preset color with (R, G, B) = (255,140,0).
        /// <\summary>
        public static RGBColor DarkOrange { get; } = new RGBColor(255, 140, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (153,50,204).
        /// <\summary>
        public static RGBColor DarkOrchid { get; } = new RGBColor(153, 50, 204);
        /// <summary>
        /// The preset color with (R, G, B) = (139,0,0).
        /// <\summary>
        public static RGBColor DarkRed { get; } = new RGBColor(139, 0, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (233,150,122).
        /// <\summary>
        public static RGBColor DarkSalmon { get; } = new RGBColor(233, 150, 122);
        /// <summary>
        /// The preset color with (R, G, B) = (143,188,139).
        /// <\summary>
        public static RGBColor DarkSeaGreen { get; } = new RGBColor(143, 188, 139);
        /// <summary>
        /// The preset color with (R, G, B) = (72,61,139).
        /// <\summary>
        public static RGBColor DarkSlateBlue { get; } = new RGBColor(72, 61, 139);
        /// <summary>
        /// The preset color with (R, G, B) = (47,79,79).
        /// <\summary>
        public static RGBColor DarkSlateGray { get; } = new RGBColor(47, 79, 79);
        /// <summary>
        /// The preset color with (R, G, B) = (0,206,209).
        /// <\summary>
        public static RGBColor DarkTurquoise { get; } = new RGBColor(0, 206, 209);
        /// <summary>
        /// The preset color with (R, G, B) = (148,0,211).
        /// <\summary>
        public static RGBColor DarkViolet { get; } = new RGBColor(148, 0, 211);
        /// <summary>
        /// The preset color with (R, G, B) = (255,20,147).
        /// <\summary>
        public static RGBColor DeepPink { get; } = new RGBColor(255, 20, 147);
        /// <summary>
        /// The preset color with (R, G, B) = (0,191,255).
        /// <\summary>
        public static RGBColor DeepSkyBlue { get; } = new RGBColor(0, 191, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (105,105,105).
        /// <\summary>
        public static RGBColor DimGray { get; } = new RGBColor(105, 105, 105);
        /// <summary>
        /// The preset color with (R, G, B) = (30,144,255).
        /// <\summary>
        public static RGBColor DodgerBlue { get; } = new RGBColor(30, 144, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (178,34,34).
        /// <\summary>
        public static RGBColor Firebrick { get; } = new RGBColor(178, 34, 34);
        /// <summary>
        /// The preset color with (R, G, B) = (255,250,240).
        /// <\summary>
        public static RGBColor FloralWhite { get; } = new RGBColor(255, 250, 240);
        /// <summary>
        /// The preset color with (R, G, B) = (34,139,34).
        /// <\summary>
        public static RGBColor ForestGreen { get; } = new RGBColor(34, 139, 34);
        /// <summary>
        /// The preset color with (R, G, B) = (255,0,255).
        /// <\summary>
        public static RGBColor Fuchsia { get; } = new RGBColor(255, 0, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (220,220,220).
        /// <\summary>
        public static RGBColor Gainsboro { get; } = new RGBColor(220, 220, 220);
        /// <summary>
        /// The preset color with (R, G, B) = (248,248,255).
        /// <\summary>
        public static RGBColor GhostWhite { get; } = new RGBColor(248, 248, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (255,215,0).
        /// <\summary>
        public static RGBColor Gold { get; } = new RGBColor(255, 215, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (218,165,32).
        /// <\summary>
        public static RGBColor Goldenrod { get; } = new RGBColor(218, 165, 32);
        /// <summary>
        /// The preset color with (R, G, B) = (128,128,128).
        /// <\summary>
        public static RGBColor Gray { get; } = new RGBColor(128, 128, 128);
        /// <summary>
        /// The preset color with (R, G, B) = (0,128,0).
        /// <\summary>
        public static RGBColor Green { get; } = new RGBColor(0, 128, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (173,255,47).
        /// <\summary>
        public static RGBColor GreenYellow { get; } = new RGBColor(173, 255, 47);
        /// <summary>
        /// The preset color with (R, G, B) = (240,255,240).
        /// <\summary>
        public static RGBColor Honeydew { get; } = new RGBColor(240, 255, 240);
        /// <summary>
        /// The preset color with (R, G, B) = (255,105,180).
        /// <\summary>
        public static RGBColor HotPink { get; } = new RGBColor(255, 105, 180);
        /// <summary>
        /// The preset color with (R, G, B) = (205,92,92).
        /// <\summary>
        public static RGBColor IndianRed { get; } = new RGBColor(205, 92, 92);
        /// <summary>
        /// The preset color with (R, G, B) = (75,0,130).
        /// <\summary>
        public static RGBColor Indigo { get; } = new RGBColor(75, 0, 130);
        /// <summary>
        /// The preset color with (R, G, B) = (255,255,240).
        /// <\summary>
        public static RGBColor Ivory { get; } = new RGBColor(255, 255, 240);
        /// <summary>
        /// The preset color with (R, G, B) = (240,230,140).
        /// <\summary>
        public static RGBColor Khaki { get; } = new RGBColor(240, 230, 140);
        /// <summary>
        /// The preset color with (R, G, B) = (230,230,250).
        /// <\summary>
        public static RGBColor Lavender { get; } = new RGBColor(230, 230, 250);
        /// <summary>
        /// The preset color with (R, G, B) = (255,240,245).
        /// <\summary>
        public static RGBColor LavenderBlush { get; } = new RGBColor(255, 240, 245);
        /// <summary>
        /// The preset color with (R, G, B) = (124,252,0).
        /// <\summary>
        public static RGBColor LawnGreen { get; } = new RGBColor(124, 252, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (255,250,205).
        /// <\summary>
        public static RGBColor LemonChiffon { get; } = new RGBColor(255, 250, 205);
        /// <summary>
        /// The preset color with (R, G, B) = (173,216,230).
        /// <\summary>
        public static RGBColor LightBlue { get; } = new RGBColor(173, 216, 230);
        /// <summary>
        /// The preset color with (R, G, B) = (240,128,128).
        /// <\summary>
        public static RGBColor LightCoral { get; } = new RGBColor(240, 128, 128);
        /// <summary>
        /// The preset color with (R, G, B) = (224,255,255).
        /// <\summary>
        public static RGBColor LightCyan { get; } = new RGBColor(224, 255, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (250,250,210).
        /// <\summary>
        public static RGBColor LightGoldenrodYellow { get; } = new RGBColor(250, 250, 210);
        /// <summary>
        /// The preset color with (R, G, B) = (211,211,211).
        /// <\summary>
        public static RGBColor LightGray { get; } = new RGBColor(211, 211, 211);
        /// <summary>
        /// The preset color with (R, G, B) = (144,238,144).
        /// <\summary>
        public static RGBColor LightGreen { get; } = new RGBColor(144, 238, 144);
        /// <summary>
        /// The preset color with (R, G, B) = (255,182,193).
        /// <\summary>
        public static RGBColor LightPink { get; } = new RGBColor(255, 182, 193);
        /// <summary>
        /// The preset color with (R, G, B) = (255,160,122).
        /// <\summary>
        public static RGBColor LightSalmon { get; } = new RGBColor(255, 160, 122);
        /// <summary>
        /// The preset color with (R, G, B) = (32,178,170).
        /// <\summary>
        public static RGBColor LightSeaGreen { get; } = new RGBColor(32, 178, 170);
        /// <summary>
        /// The preset color with (R, G, B) = (135,206,250).
        /// <\summary>
        public static RGBColor LightSkyBlue { get; } = new RGBColor(135, 206, 250);
        /// <summary>
        /// The preset color with (R, G, B) = (119,136,153).
        /// <\summary>
        public static RGBColor LightSlateGray { get; } = new RGBColor(119, 136, 153);
        /// <summary>
        /// The preset color with (R, G, B) = (176,196,222).
        /// <\summary>
        public static RGBColor LightSteelBlue { get; } = new RGBColor(176, 196, 222);
        /// <summary>
        /// The preset color with (R, G, B) = (255,255,224).
        /// <\summary>
        public static RGBColor LightYellow { get; } = new RGBColor(255, 255, 224);
        /// <summary>
        /// The preset color with (R, G, B) = (0,255,0).
        /// <\summary>
        public static RGBColor Lime { get; } = new RGBColor(0, 255, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (50,205,50).
        /// <\summary>
        public static RGBColor LimeGreen { get; } = new RGBColor(50, 205, 50);
        /// <summary>
        /// The preset color with (R, G, B) = (250,240,230).
        /// <\summary>
        public static RGBColor Linen { get; } = new RGBColor(250, 240, 230);
        /// <summary>
        /// The preset color with (R, G, B) = (255,0,255).
        /// <\summary>
        public static RGBColor Magenta { get; } = new RGBColor(255, 0, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (128,0,0).
        /// <\summary>
        public static RGBColor Maroon { get; } = new RGBColor(128, 0, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (102,205,170).
        /// <\summary>
        public static RGBColor MediumAquamarine { get; } = new RGBColor(102, 205, 170);
        /// <summary>
        /// The preset color with (R, G, B) = (0,0,205).
        /// <\summary>
        public static RGBColor MediumBlue { get; } = new RGBColor(0, 0, 205);
        /// <summary>
        /// The preset color with (R, G, B) = (186,85,211).
        /// <\summary>
        public static RGBColor MediumOrchid { get; } = new RGBColor(186, 85, 211);
        /// <summary>
        /// The preset color with (R, G, B) = (147,112,219).
        /// <\summary>
        public static RGBColor MediumPurple { get; } = new RGBColor(147, 112, 219);
        /// <summary>
        /// The preset color with (R, G, B) = (60,179,113).
        /// <\summary>
        public static RGBColor MediumSeaGreen { get; } = new RGBColor(60, 179, 113);
        /// <summary>
        /// The preset color with (R, G, B) = (123,104,238).
        /// <\summary>
        public static RGBColor MediumSlateBlue { get; } = new RGBColor(123, 104, 238);
        /// <summary>
        /// The preset color with (R, G, B) = (0,250,154).
        /// <\summary>
        public static RGBColor MediumSpringGreen { get; } = new RGBColor(0, 250, 154);
        /// <summary>
        /// The preset color with (R, G, B) = (72,209,204).
        /// <\summary>
        public static RGBColor MediumTurquoise { get; } = new RGBColor(72, 209, 204);
        /// <summary>
        /// The preset color with (R, G, B) = (199,21,133).
        /// <\summary>
        public static RGBColor MediumVioletRed { get; } = new RGBColor(199, 21, 133);
        /// <summary>
        /// The preset color with (R, G, B) = (25,25,112).
        /// <\summary>
        public static RGBColor MidnightBlue { get; } = new RGBColor(25, 25, 112);
        /// <summary>
        /// The preset color with (R, G, B) = (245,255,250).
        /// <\summary>
        public static RGBColor MintCream { get; } = new RGBColor(245, 255, 250);
        /// <summary>
        /// The preset color with (R, G, B) = (255,228,225).
        /// <\summary>
        public static RGBColor MistyRose { get; } = new RGBColor(255, 228, 225);
        /// <summary>
        /// The preset color with (R, G, B) = (255,228,181).
        /// <\summary>
        public static RGBColor Moccasin { get; } = new RGBColor(255, 228, 181);
        /// <summary>
        /// The preset color with (R, G, B) = (255,222,173).
        /// <\summary>
        public static RGBColor NavajoWhite { get; } = new RGBColor(255, 222, 173);
        /// <summary>
        /// The preset color with (R, G, B) = (0,0,128).
        /// <\summary>
        public static RGBColor Navy { get; } = new RGBColor(0, 0, 128);
        /// <summary>
        /// The preset color with (R, G, B) = (253,245,230).
        /// <\summary>
        public static RGBColor OldLace { get; } = new RGBColor(253, 245, 230);
        /// <summary>
        /// The preset color with (R, G, B) = (128,128,0).
        /// <\summary>
        public static RGBColor Olive { get; } = new RGBColor(128, 128, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (107,142,35).
        /// <\summary>
        public static RGBColor OliveDrab { get; } = new RGBColor(107, 142, 35);
        /// <summary>
        /// The preset color with (R, G, B) = (255,165,0).
        /// <\summary>
        public static RGBColor Orange { get; } = new RGBColor(255, 165, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (255,69,0).
        /// <\summary>
        public static RGBColor OrangeRed { get; } = new RGBColor(255, 69, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (218,112,214).
        /// <\summary>
        public static RGBColor Orchid { get; } = new RGBColor(218, 112, 214);
        /// <summary>
        /// The preset color with (R, G, B) = (238,232,170).
        /// <\summary>
        public static RGBColor PaleGoldenrod { get; } = new RGBColor(238, 232, 170);
        /// <summary>
        /// The preset color with (R, G, B) = (152,251,152).
        /// <\summary>
        public static RGBColor PaleGreen { get; } = new RGBColor(152, 251, 152);
        /// <summary>
        /// The preset color with (R, G, B) = (175,238,238).
        /// <\summary>
        public static RGBColor PaleTurquoise { get; } = new RGBColor(175, 238, 238);
        /// <summary>
        /// The preset color with (R, G, B) = (219,112,147).
        /// <\summary>
        public static RGBColor PaleVioletRed { get; } = new RGBColor(219, 112, 147);
        /// <summary>
        /// The preset color with (R, G, B) = (255,239,213).
        /// <\summary>
        public static RGBColor PapayaWhip { get; } = new RGBColor(255, 239, 213);
        /// <summary>
        /// The preset color with (R, G, B) = (255,218,185).
        /// <\summary>
        public static RGBColor PeachPuff { get; } = new RGBColor(255, 218, 185);
        /// <summary>
        /// The preset color with (R, G, B) = (205,133,63).
        /// <\summary>
        public static RGBColor Peru { get; } = new RGBColor(205, 133, 63);
        /// <summary>
        /// The preset color with (R, G, B) = (255,192,203).
        /// <\summary>
        public static RGBColor Pink { get; } = new RGBColor(255, 192, 203);
        /// <summary>
        /// The preset color with (R, G, B) = (221,160,221).
        /// <\summary>
        public static RGBColor Plum { get; } = new RGBColor(221, 160, 221);
        /// <summary>
        /// The preset color with (R, G, B) = (176,224,230).
        /// <\summary>
        public static RGBColor PowderBlue { get; } = new RGBColor(176, 224, 230);
        /// <summary>
        /// The preset color with (R, G, B) = (128,0,128).
        /// <\summary>
        public static RGBColor Purple { get; } = new RGBColor(128, 0, 128);
        /// <summary>
        /// The preset color with (R, G, B) = (255,0,0).
        /// <\summary>
        public static RGBColor Red { get; } = new RGBColor(255, 0, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (188,143,143).
        /// <\summary>
        public static RGBColor RosyBrown { get; } = new RGBColor(188, 143, 143);
        /// <summary>
        /// The preset color with (R, G, B) = (65,105,225).
        /// <\summary>
        public static RGBColor RoyalBlue { get; } = new RGBColor(65, 105, 225);
        /// <summary>
        /// The preset color with (R, G, B) = (139,69,19).
        /// <\summary>
        public static RGBColor SaddleBrown { get; } = new RGBColor(139, 69, 19);
        /// <summary>
        /// The preset color with (R, G, B) = (250,128,114).
        /// <\summary>
        public static RGBColor Salmon { get; } = new RGBColor(250, 128, 114);
        /// <summary>
        /// The preset color with (R, G, B) = (244,164,96).
        /// <\summary>
        public static RGBColor SandyBrown { get; } = new RGBColor(244, 164, 96);
        /// <summary>
        /// The preset color with (R, G, B) = (46,139,87).
        /// <\summary>
        public static RGBColor SeaGreen { get; } = new RGBColor(46, 139, 87);
        /// <summary>
        /// The preset color with (R, G, B) = (255,245,238).
        /// <\summary>
        public static RGBColor SeaShell { get; } = new RGBColor(255, 245, 238);
        /// <summary>
        /// The preset color with (R, G, B) = (160,82,45).
        /// <\summary>
        public static RGBColor Sienna { get; } = new RGBColor(160, 82, 45);
        /// <summary>
        /// The preset color with (R, G, B) = (192,192,192).
        /// <\summary>
        public static RGBColor Silver { get; } = new RGBColor(192, 192, 192);
        /// <summary>
        /// The preset color with (R, G, B) = (135,206,235).
        /// <\summary>
        public static RGBColor SkyBlue { get; } = new RGBColor(135, 206, 235);
        /// <summary>
        /// The preset color with (R, G, B) = (106,90,205).
        /// <\summary>
        public static RGBColor SlateBlue { get; } = new RGBColor(106, 90, 205);
        /// <summary>
        /// The preset color with (R, G, B) = (112,128,144).
        /// <\summary>
        public static RGBColor SlateGray { get; } = new RGBColor(112, 128, 144);
        /// <summary>
        /// The preset color with (R, G, B) = (255,250,250).
        /// <\summary>
        public static RGBColor Snow { get; } = new RGBColor(255, 250, 250);
        /// <summary>
        /// The preset color with (R, G, B) = (0,255,127).
        /// <\summary>
        public static RGBColor SpringGreen { get; } = new RGBColor(0, 255, 127);
        /// <summary>
        /// The preset color with (R, G, B) = (70,130,180).
        /// <\summary>
        public static RGBColor SteelBlue { get; } = new RGBColor(70, 130, 180);
        /// <summary>
        /// The preset color with (R, G, B) = (210,180,140).
        /// <\summary>
        public static RGBColor Tan { get; } = new RGBColor(210, 180, 140);
        /// <summary>
        /// The preset color with (R, G, B) = (0,128,128).
        /// <\summary>
        public static RGBColor Teal { get; } = new RGBColor(0, 128, 128);
        /// <summary>
        /// The preset color with (R, G, B) = (216,191,216).
        /// <\summary>
        public static RGBColor Thistle { get; } = new RGBColor(216, 191, 216);
        /// <summary>
        /// The preset color with (R, G, B) = (255,99,71).
        /// <\summary>
        public static RGBColor Tomato { get; } = new RGBColor(255, 99, 71);
        /// <summary>
        /// The preset color with (R, G, B) = (64,224,208).
        /// <\summary>
        public static RGBColor Turquoise { get; } = new RGBColor(64, 224, 208);
        /// <summary>
        /// The preset color with (R, G, B) = (238,130,238).
        /// <\summary>
        public static RGBColor Violet { get; } = new RGBColor(238, 130, 238);
        /// <summary>
        /// The preset color with (R, G, B) = (245,222,179).
        /// <\summary>
        public static RGBColor Wheat { get; } = new RGBColor(245, 222, 179);
        /// <summary>
        /// The preset color with (R, G, B) = (255,255,255).
        /// <\summary>
        public static RGBColor White { get; } = new RGBColor(255, 255, 255);
        /// <summary>
        /// The preset color with (R, G, B) = (245,245,245).
        /// <\summary>
        public static RGBColor WhiteSmoke { get; } = new RGBColor(245, 245, 245);
        /// <summary>
        /// The preset color with (R, G, B) = (255,255,0).
        /// <\summary>
        public static RGBColor Yellow { get; } = new RGBColor(255, 255, 0);
        /// <summary>
        /// The preset color with (R, G, B) = (154,205,50).
        /// <\summary>
        public static RGBColor YellowGreen { get; } = new RGBColor(154, 205, 50);
    }
}

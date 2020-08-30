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
using Asterion.OpenGL;
using System.Text;

namespace Asterion.UI
{
    /// <summary>
    /// An UIControl, Asterion.UI equivalent to a System.Windows.Forms control.
    /// This class is abstract but all UIControl classes are derived from it.
    /// </summary>
    public abstract class UIControl
    {
        /// <summary>
        /// The page this controls belongs to.
        /// </summary>
        public UIPage Page { get; private set; }

        /// <summary>
        /// The position of the upper-left corner of the control.
        /// </summary>
        public Position Position { get { return Position_; } set { Position_ = value; Page.UI.Invalidate(); } }
        private Position Position_ = Position.Zero;

        /// <summary>
        /// X-coordinate of the left of the control.
        /// </summary>
        public int X { get { return Position_.X; } set { Position_ = new Position(value, Position_.Y); Page.UI.Invalidate(); } }

        /// <summary>
        /// Y-coordinate of the top of the control.
        /// </summary>
        public int Y { get { return Position_.Y; } set { Position_ = new Position(Position_.X, value); Page.UI.Invalidate(); } }

        /// <summary>
        /// The color of the control.
        /// </summary>
        public RGBColor Color { get { return Color_; } set { Color_ = value; Page.UI.Invalidate(); } }
        private RGBColor Color_ = RGBColor.White;

        /// <summary>
        /// The tilemap on which to find this control's tile.
        /// </summary>
        public int Tilemap { get { return Tilemap_; } set { Tilemap_ = value; Page.UI.Invalidate(); } }
        private int Tilemap_ = 0;

        /// <summary>
        /// The Z-Order of this control. Higher values are drawn last.
        /// </summary>
        public int ZOrder { get { return ZOrder_; } set { ZOrder_ = value; Page.UI.Invalidate(); } }
        private int ZOrder_ = 0;

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        internal UIControl() { }

        /// <summary>
        /// (Internal) Called just after the control is created.
        /// </summary>
        /// <param name="page">The UIPage this control belongs to</param>
        internal virtual void Initialize(UIPage page) { Page = page; }

        /// <summary>
        /// (Internal) Destroys the control.
        /// </summary>
        internal virtual void Destroy() { }

        /// <summary>
        /// (Internal) Draws the control on the provided VBO.
        /// </summary>
        /// <param name="vbo">UI VBO on which to draw the control.</param>
        internal abstract void UpdateVBOTiles(VBO vbo);

        internal void DrawTextOnVBO(VBO vbo, string text, int x, int y, int tile)
        {
            if (string.IsNullOrEmpty(text)) return;

            byte[] textBytes = Encoding.ASCII.GetBytes(text);

            for (int i = 0; i < textBytes.Length; i++)
            {
                if ((textBytes[i] < 32) || (textBytes[i] > 126)) textBytes[i] = 32;

                Tile charTile = new Tile(tile + textBytes[i] - 32, Color, Tilemap);

                vbo.UpdateTileData(x + i, y, charTile);
            }
        }
    }
}

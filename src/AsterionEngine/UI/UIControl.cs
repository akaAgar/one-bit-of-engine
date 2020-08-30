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

namespace Asterion.UI
{
    /// <summary>
    /// An UIControl, Asterion.UI equivalent to a System.Windows.Forms control.
    /// This class is abstract but all UIControl classes are derived from it.
    /// </summary>
    public abstract class UIControl
    {
        /// <summary>
        /// (Internal) The page this controls belongs to.
        /// </summary>
        internal UIPage Page { get; private set; }

        /// <summary>
        /// The position of the upper-left corner of the control.
        /// </summary>
        public Position Position { get { return PositionPrivate; } set { PositionPrivate = value; Page.UI.Invalidate(); } }
        private Position PositionPrivate = Position.Zero;

        /// <summary>
        /// The color of the control.
        /// </summary>
        public RGBColor Color { get { return ColorPrivate; } set { ColorPrivate = value; Page.UI.Invalidate(); } }
        private RGBColor ColorPrivate = RGBColor.White;

        /// <summary>
        /// The tilemap on which to find this control's tile.
        /// </summary>
        public int Tilemap { get { return TilemapPrivate; } set { TilemapPrivate = value; Page.UI.Invalidate(); } }
        private int TilemapPrivate = 0;

        /// <summary>
        /// The Z-Order of this control. Higher values are drawn last.
        /// </summary>
        public int ZOrder { get { return ZOrderPrivate; } set { ZOrderPrivate = value; Page.UI.Invalidate(); } }
        private int ZOrderPrivate = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        internal UIControl() { }

        internal virtual void Initialize(UIPage page) { Page = page; }

        internal virtual void Render() { }

        internal void Destroy() { }

        internal abstract void UpdateVBOTiles(VBO vbo);
    }
}

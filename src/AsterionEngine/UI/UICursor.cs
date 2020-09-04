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
    /// A tile cursor (not the mouse cursor) which can be drawn on any tile.
    /// </summary>
    public sealed class UICursor
    {
        /// <summary>
        /// Cursor tile.
        /// </summary>
        public int Tile { get { return Tile_; } set { Tile_ = value; UpdateCursor(); } }
        private int Tile_ = 0;

        /// <summary>
        /// Cursor color.
        /// </summary>
        public RGBColor Color { get { return Color_; } set { Color_ = value; UpdateCursor(); } }
        private RGBColor Color_ = RGBColor.White;

        /// <summary>
        /// Tile map to read the cursor tile from.
        /// </summary>
        public int Tilemap { get { return Tilemap_; } set { Tilemap_ = value; UpdateCursor(); } }
        private int Tilemap_ = 0;

        /// <summary>
        /// Special shader effect to use for the cursor tile.
        /// </summary>
        public TileVFX VFX { get { return VFX_; } set { VFX_ = value; UpdateCursor(); } }
        private TileVFX VFX_ = TileVFX.None;

        /// <summary>
        /// Cursor position.
        /// </summary>
        public Position Position { get { return Position_; } set { Position_ = value; UpdateCursor(); } }
        private Position Position_ = Position.Zero;

        /// <summary>
        /// (Private) Cursor VBO (1×1 tile).
        /// </summary>
        private VBO CursorVBO = null;

        /// <summary>
        /// Should the cursor be drawn?
        /// </summary>
        public bool Visible { get; set; } = false;

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        internal UICursor() { }

        /// <summary>
        /// (Internal) Called before the first game loop. Creates the VBO.
        /// </summary>
        /// <param name="renderer">The tile renderer to use to draw the cursor VBO</param>
        internal void OnLoad(TileRenderer renderer)
        {
            CursorVBO = new VBO(renderer, 1, 1);
            UpdateCursor();
        }

        /// <summary>
        /// (Internal) Called each frame. Draws the cursor VBO, if required.
        /// </summary>
        internal void OnRenderFrame()
        {
            if (!Visible) return;
            CursorVBO.Render();
        }

        /// <summary>
        /// (Private) Updates the cursor VBO with new data.
        /// </summary>
        private void UpdateCursor()
        {
            CursorVBO.UpdateTileData(0, 0, Position_.X, Position_.Y, Tile_, Color_, Tilemap_, VFX_);
        }

        /// <summary>
        /// (Internal) Destroys the cursor and the VBO.
        /// </summary>
        internal void Destroy()
        {
            CursorVBO?.Dispose();
        }
    }
}

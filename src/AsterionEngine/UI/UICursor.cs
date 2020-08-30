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
        /// (Private) Cursor tile/appearance.
        /// </summary>
        private Tile Tile = new Tile(0, RGBColor.Black);
        
        /// <summary>
        /// (Private) Cursor position.
        /// </summary>
        private Position Position = Position.NegativeOne;

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
        /// Moves the cursor by a certain number of tiles.
        /// </summary>
        /// <param name="positionOffset">Cursor displacement offset, in tiles</param>
        public void MoveBy(Position positionOffset)
        {
            MoveBy(positionOffset.X, positionOffset.Y);
        }

        /// <summary>
        /// Moves the cursor by a certain number of tiles.
        /// </summary>
        /// <param name="offsetX">Cursor displacement X offset, in tiles</param>
        /// <param name="offsetY">Cursor displacement Y offset, in tiles</param>
        public void MoveBy(int offsetX, int offsetY)
        {
            Position newCursorPosition = new Position(Position.X + offsetX, Position.Y + offsetY);
            MoveTo(newCursorPosition);
        }

        /// <summary>
        /// Moves the cursor to a given tile.
        /// </summary>
        /// <param name="position">Coordinates of the tile the cursor should be moved to.</param>
        public void MoveTo(Position position)
        {
            MoveTo(position.X, position.Y);
        }

        /// <summary>
        /// Moves the cursor to a given tile.
        /// </summary>
        /// <param name="x">X coordinate of the tile the cursor should be moved to.</param>
        /// <param name="y">Y coordinate of the tile the cursor should be moved to.</param>
        public void MoveTo(int x, int y)
        {
            Position = new Position(x, y);

            UpdateCursor();
        }

        /// <summary>
        /// Sets the cursor tile/appearance.
        /// </summary>
        /// <param name="tile">A tile to use as a cursor</param>
        public void SetTile(Tile tile)
        {
            Tile = tile;
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
            CursorVBO.UpdateTileData(0, 0, Position.X, Position.Y, Tile);
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

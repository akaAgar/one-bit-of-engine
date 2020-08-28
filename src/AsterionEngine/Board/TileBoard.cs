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

using Asterion.Video;
using System.Drawing;

namespace Asterion.Board
{
    /// <summary>
    /// Provides a "blackboard" of tiles which can be get/set manually.
    /// </summary>
    public sealed class TileBoard
    {
        public Rectangle Viewport { get; private set; } = Rectangle.Zero;

        public bool Visible { get; set; } = false;

        private VBO TilesBoardVBO = null;

        internal TileBoard(Size tileCount)
        {
            ResizeViewport(0, 0, tileCount.Width, tileCount.Height);
        }

        public void ResizeViewport(int x, int y, int width, int height)
        {
            Viewport = new Rectangle(x, y, width, height);

            if (TilesBoardVBO != null)
                RecreateVBO();
        }

        internal void OnRenderFrame()
        {
            if (!Visible) return;

            TilesBoardVBO.Render();
        }

        internal void Dispose()
        {
            TilesBoardVBO.Dispose();
        }

        internal void OnLoad(AsterionGame game)
        {
            TilesBoardVBO = new VBO(game.Tiles, 0, 0);
            RecreateVBO();
            Clear(0, RGBColor.Black);
        }

        private void RecreateVBO()
        {
            TilesBoardVBO.CreateNewBuffer(Viewport.Dimension);
        }

        public void Clear(int tile, RGBColor color, int tilemap = 0)
        {
            int x, y;

            for (x = 0; x < Viewport.Width; x++)
                for (y = 0; y < Viewport.Height; y++)
                    TilesBoardVBO.UpdateTileData(
                        x, y,
                        x + Viewport.X, y + Viewport.Y,
                        new Tile(tile, color, tilemap));
        }

        public bool IsTileInViewport(Position tile)
        {
            return IsTileInViewport(tile.X, tile.Y);
        }

        public bool IsTileInViewport(int x, int y)
        {
            return Viewport.Contains(x, y);
        }

        public void SetTile(int x, int y, Tile tile)
        {
            TilesBoardVBO.UpdateTileData(
                x, y,
                x + Viewport.X, y + Viewport.Y,
                tile);
        }
    }
}

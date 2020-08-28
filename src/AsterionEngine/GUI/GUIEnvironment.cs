using Asterion.Video;
using System;
using System.Drawing;
using System.Text;

namespace Asterion.GUI
{
    public sealed class GUIEnvironment
    {
        private readonly AsterionGame Game;

        private Tile CursorTile = new Tile(0, RGBColor.Black);
        private Point CursorPosition = Point.Empty;

        private VBO TilesVBO, CursorVBO;

        public bool CursorVisible { get; set; } = true;

        internal GUIEnvironment(AsterionGame game)
        {
            Game = game;
        }

        internal void OnLoad()
        {
            TilesVBO = new VBO(Game.Tiles, Game.Tiles.TileCountX, Game.Tiles.TileCountY);
            CursorVBO = new VBO(Game.Tiles, 1, 1);
            UpdateCursor();
        }

        internal void Dispose()
        {
            TilesVBO?.Dispose();
            CursorVBO?.Dispose();
        }

        internal void RenderInterface()
        {
            TilesVBO.Render();
        }

        internal void RenderCursor()
        {
            if (!CursorVisible) return;
            CursorVBO.Render();
        }

        public void MoveCursorBy(int deltaX, int deltaY)
        {
            Point newCursorPosition = new Point(CursorPosition.X + deltaX, CursorPosition.Y + deltaY);
            MoveCursorTo(newCursorPosition.X, newCursorPosition.Y);
        }

        public void MoveCursorTo(Position pt) { MoveCursorTo(pt.X, pt.Y); }
        public void MoveCursorTo(int x, int y)
        {
            x = Math.Max(0, Math.Min(Game.Tiles.TileCountX - 1, x));
            y = Math.Max(0, Math.Min(Game.Tiles.TileCountY - 1, y));
            CursorPosition = new Point(x, y);

            UpdateCursor();
        }

        public void SetCursor(Tile cursorTile)
        {
            CursorTile = cursorTile;
            UpdateCursor();
        }

        private void UpdateCursor()
        {
            CursorVBO.UpdateTileData(CursorPosition.X, CursorPosition.Y, CursorTile);
        }

        public void ClearTiles(Tile tile)
        {
            ClearTiles(new Area(0, 0, Game.Tiles.TileCountX, Game.Tiles.TileCountY), tile);
        }

        public void ClearTiles(Area region, Tile tile)
        {
            int x, y;

            for (x = region.Left; x < region.Right; x++)
                for (y = region.Top; y < region.Bottom; y++)
                    DrawTile(x, y, tile);
        }

        public void DrawTile(Point pt, Tile tile) { DrawTile(pt.X, pt.Y, tile); }
        public void DrawTile(int x, int y, Tile tile)
        {
            TilesVBO.UpdateTileData(x, y, tile);
        }

        public void DrawFrame(Area rect, Tile tile)
        {
            int x, y;
            int frameTileIndex;
            Tile frameTile;

            for (x = rect.Left; x < rect.Right; x++)
                for (y = rect.Top; y < rect.Bottom; y++)
                {
                    frameTileIndex = tile.TileIndex;

                    if (x == rect.Left)
                    {
                        if (y == rect.Top) frameTileIndex += 0;
                        else if (y == rect.Bottom - 1) frameTileIndex += 2;
                        else frameTileIndex += 5;
                    }
                    else if (x == rect.Right - 1)
                    {
                        if (y == rect.Top) frameTileIndex += 1;
                        else if (y == rect.Bottom - 1) frameTileIndex += 3;
                        else frameTileIndex += 7;
                    }
                    else if (y == rect.Top) frameTileIndex += 4;
                    else if (y == rect.Bottom - 1) frameTileIndex += 6;
                    else
                        continue;

                    frameTile = new Tile(frameTileIndex, tile.Color, tile.Tilemap, tile.Animated);

                    DrawTile(x, y, frameTile);
                }
        }

        // ASCII: 32-126 Valid characters are: !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~
        public void DrawText(int x, int y, string text, Tile fontTile, int maxlength = 0)
        {
            if (string.IsNullOrEmpty(text)) return;
            if (maxlength > 0) text = text.Substring(0, Math.Min(text.Length, maxlength));

            byte[] textBytes = Encoding.ASCII.GetBytes(text);

            for (int i = 0; i < textBytes.Length; i++)
            {
                if ((textBytes[i] < 32) || (textBytes[i] > 126)) textBytes[i] = 32;

                Tile charTile = new Tile(fontTile.TileIndex + textBytes[i] - 32, fontTile.Color, fontTile.Tilemap, fontTile.Animated);

                DrawTile(x + i, y, charTile);
            }
        }
    }
}

using Asterion.Video;
using System.Drawing;

namespace Asterion.Board
{
    public sealed class TileBoard
    {
        public Rectangle Viewport
        {
            get
            {
                return _Viewport;
            }
            set
            {
                _Viewport = value;
                if (TilesBoardVBO != null) RecreateVBO();
            }
        }
        private Rectangle _Viewport = Rectangle.Empty;

        public bool Visible { get; set; } = false;

        private VBO TilesBoardVBO = null;

        internal TileBoard(Size tileCount)
        {
            Viewport = new Rectangle(0, 0, tileCount.Width, tileCount.Height);
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
        }

        private void RecreateVBO()
        {
            TilesBoardVBO.CreateNewBuffer(_Viewport.Width, _Viewport.Height);
        }

        public bool IsTileInViewport(Point cell) { return IsTileInViewport(cell.X, cell.Y); }
        public bool IsTileInViewport(int x, int y) { return _Viewport.Contains(x, y); }
    }
}

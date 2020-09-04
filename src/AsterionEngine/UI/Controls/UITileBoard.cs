using Asterion.Core;
using Asterion.OpenGL;

namespace Asterion.UI.Controls
{
    public class UITileBoard : UIControl
    {
        public Dimension Dimension
        {
            get { return _Dimension; }
            set
            {
                _Dimension = value;
                Tiles = new UITileBoardTile[Dimension.Width, Dimension.Height];
                Clear(UITileBoardTile.Empty);
            }
        }

        private Dimension _Dimension = Dimension.One;

        private UITileBoardTile[,] Tiles = new UITileBoardTile[1, 1];

        internal override void Initialize(UIPage page)
        {
            base.Initialize(page);
            Clear(UITileBoardTile.Empty);
        }

        public UITileBoardTile this[Position position]
        {
            get { return GetTile(position); }
            set { SetTile(position, value); }
        }

        public UITileBoardTile this[int x, int y]
        {
            get { return GetTile(x, y); }
            set { SetTile(x, y, value); }
        }

        public UITileBoardTile GetTile(Position position)
        {
            return GetTile(position.X, position.Y);
        }

        public UITileBoardTile GetTile(int x, int y)
        {
            if (!Dimension.Contains(x, y)) return UITileBoardTile.Empty;
            return Tiles[x, y];
        }

        public void SetTile(Position position, UITileBoardTile tile)
        {
            SetTile(position.X, position.Y, tile);
        }

        public void SetTile(int x, int y, UITileBoardTile tile)
        {
            if (!Dimension.Contains(x, y)) return;
            Tiles[x, y] = tile;
            Page.UI.Invalidate();
        }

        public void Clear(UITileBoardTile tile)
        {
            int x, y;

            for (x = 0; x < Dimension.Width; x++)
                for (y = 0; y < Dimension.Height; y++)
                    Tiles[x, y] = tile;

            Page.UI.Invalidate();
        }

        internal override void UpdateVBOTiles(VBO vbo)
        {
            int x, y;

            for (x = 0; x < Dimension.Width; x++)
                for (y = 0; y < Dimension.Height; y++)
                    vbo.UpdateTileData(x + Position.X, y + Position.Y, Tiles[x, y].TileIndex, Tiles[x, y].Color, Tiles[x, y].Tilemap, Tiles[x, y].VFX);
        }
    }
}

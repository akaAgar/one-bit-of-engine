using Asterion.Core;
using Asterion.OpenGL;

namespace Asterion.UI
{
    public class UITileBoard : UIControl
    {
        public Dimension Dimension
        {
            get { return _Dimension; }
            set
            {
                _Dimension = value;
                Tiles = new Tile[Dimension.Width, Dimension.Height];
                Clear(Tile.Empty);
            }
        }

        private Dimension _Dimension = Dimension.One; 

        private Tile[,] Tiles = new Tile[1, 1];

        internal override void Initialize(UIPage page)
        {
            base.Initialize(page);
            Clear(Tile.Empty);
        }

        public Tile this[Position position]
        {
            get { return GetTile(position); }
            set { SetTile(position, value); }
        }

        public Tile this[int x, int y]
        {
            get { return GetTile(x, y); }
            set { SetTile(x, y, value); }
        }

        public Tile GetTile(Position position)
        {
            return GetTile(position.X, position.Y);
        }

        public Tile GetTile(int x, int y)
        {
            if (!Dimension.Contains(x, y)) return Tile.Empty;
            return Tiles[x, y];
        }

        public void SetTile(Position position, Tile tile)
        {
            SetTile(position.X, position.Y, tile);
        }

        public void SetTile(int x, int y, Tile tile)
        {
            if (!Dimension.Contains(x, y)) return;
            Tiles[x, y] = tile;
            Page.UI.UpdateTiles();
        }

        public void Clear(Tile tile)
        {
            int x, y;

            for (x = 0; x < Dimension.Width; x++)
                for (y = 0; y < Dimension.Height; y++)
                    Tiles[x, y] = tile;

            Page.UI.UpdateTiles();
        }

        internal override void UpdateVBOTiles(VBO vbo)
        {
            int x, y;

            for (x = 0; x < Dimension.Width; x++)
                for (y = 0; y < Dimension.Height; y++)
                    vbo.UpdateTileData(x + Position.X, y + Position.Y, Tiles[x, y]);
        }
    }
}

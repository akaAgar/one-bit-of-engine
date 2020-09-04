using Asterion.Core;
using Asterion.OpenGL;

namespace Asterion.UI.Controls
{
    /// <summary>
    /// A rectangular control made of several tiles which can be edited. Think of it as a screen on which you can change each pixel.
    /// </summary>
    public class UITileBoard : UIControlRectangle
    {
        /// <summary>
        /// Size of the control. Resizing the control will clear the tiles.
        /// </summary>
        public override Dimension Size
        {
            get { return base.Size; }
            set
            {
                base.Size = value;
                Tiles = new UITileBoardTile[base.Size.Width, base.Size.Height];
                Clear(UITileBoardTile.Empty);
            }
        }

        /// <summary>
        /// Number of tiles in the board.
        /// </summary>
        public virtual Dimension BoardSize
        {
            get { return new Dimension(Tiles.GetLength(0), Tiles.GetLength(1)); }
        }

        /// <summary>
        /// An 2D array storing the control's tiles.
        /// </summary>
        private UITileBoardTile[,] Tiles = new UITileBoardTile[0, 0];
        
        /// <summary>
        /// (Internal) Initialize override. Creates the control's tiles array.
        /// </summary>
        /// <param name="page"></param>
        internal override void Initialize(UIPage page)
        {
            base.Initialize(page);
            Clear(UITileBoardTile.Empty);
        }

        /// <summary>
        /// Returns a tile from the tileboard.
        /// </summary>
        /// <param name="position">Coordinates of the tile</param>
        /// <returns>The tile, or an empty (tile 0, black color) tile if the position was out of bounds</returns>
        public UITileBoardTile this[Position position]
        {
            get { return GetTile(position); }
            set { SetTile(position, value); }
        }

        /// <summary>
        /// Returns a tile from the tileboard.
        /// </summary>
        /// <param name="x">X coordinate of the tile</param>
        /// <param name="y">Y coordinate of the tile</param>
        /// <returns>The tile, or an empty (tile 0, black color) tile if the position was out of bounds</returns>
        public UITileBoardTile this[int x, int y]
        {
            get { return GetTile(x, y); }
            set { SetTile(x, y, value); }
        }

        /// <summary>
        /// Returns a tile from the tileboard.
        /// </summary>
        /// <param name="position">Coordinates of the tile</param>
        /// <returns>The tile, or an empty (tile 0, black color) tile if the position was out of bounds</returns>
        public UITileBoardTile GetTile(Position position)
        {
            return GetTile(position.X, position.Y);
        }

        /// <summary>
        /// Returns a tile from the tileboard.
        /// </summary>
        /// <param name="x">X coordinate of the tile</param>
        /// <param name="y">Y coordinate of the tile</param>
        /// <returns>The tile, or an empty (tile 0, black color) tile if the position was out of bounds</returns>
        public UITileBoardTile GetTile(int x, int y)
        {
            if (!BoardSize.Contains(x, y)) return UITileBoardTile.Empty;
            return Tiles[x, y];
        }

        /// <summary>
        /// Sets a tile on the tileboard.
        /// </summary>
        /// <param name="position">Coordinates of the tile</param>
        /// <param name="tile">Tile info</param>
        public void SetTile(Position position, UITileBoardTile tile)
        {
            SetTile(position.X, position.Y, tile);
        }

        /// <summary>
        /// Sets a tile on the tileboard.
        /// </summary>
        /// <param name="x">X coordinate of the tile</param>
        /// <param name="y">Y coordinate of the tile</param>
        /// <param name="tile">Tile info</param>
        public void SetTile(int x, int y, UITileBoardTile tile)
        {
            if (!BoardSize.Contains(x, y)) return;
            Tiles[x, y] = tile;
            Page.UI.Invalidate();
        }

        /// <summary>
        /// Clears/fills the tileboard with the specified tile.
        /// </summary>
        /// <param name="tile">The tile to draw everywhere on the board</param>
        public void Clear(UITileBoardTile tile)
        {
            int x, y;

            for (x = 0; x < BoardSize.Width; x++)
                for (y = 0; y < BoardSize.Height; y++)
                    Tiles[x, y] = tile;

            Page.UI.Invalidate();
        }

        /// <summary>
        /// (Internal) Draws the control on the provided VBO.
        /// </summary>
        /// <param name="vbo">UI VBO on which to draw the control.</param>
        internal override void UpdateVBOTiles(VBO vbo)
        {
            int x, y;

            for (x = 0; x < BoardSize.Width; x++)
                for (y = 0; y < BoardSize.Height; y++)
                    vbo.UpdateTileData(x + Position.X, y + Position.Y, Tiles[x, y].TileIndex, Tiles[x, y].Color, Tiles[x, y].Tilemap, Tiles[x, y].VFX);
        }
    }
}

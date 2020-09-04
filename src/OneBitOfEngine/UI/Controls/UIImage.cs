using OneBitOfEngine.Core;
using OneBitOfEngine.OpenGL;

namespace OneBitOfEngine.UI.Controls
{
    /// <summary>
    /// A control displaying a tile
    /// </summary>
    public class UIImage : UIControlRectangle
    {
        /// <summary>
        /// The tile to draw.
        /// </summary>
        public int Tile { get { return Tile_; } set { Tile_ = value; Page.UI.Invalidate(); } }
        private int Tile_ = 0;

        /// <summary>
        /// Does this image display a composite tile?
        /// If true, will display a tile made of multiple adjacent tiles on the tilemap.
        /// If false, it will display the same tile (of index <see cref="Tile"/>) on each tile.
        /// </summary>
        public bool Composite { get { return Composite_; } set { Composite_ = value; Page.UI.Invalidate(); } }
        private bool Composite_ = false;

        /// <summary>
        /// (Internal) Draws the control on the provided VBO.
        /// </summary>
        /// <param name="vbo">UI VBO on which to draw the control.</param>
        internal override void UpdateVBOTiles(VBO vbo)
        {
            int x, y, tileIndex;

            for (x = 0; x < Width; x++)
                for (y = 0; y < Height; y++)
                {
                    tileIndex = Tile_;

                    if (Composite_)
                        tileIndex += x + (y * Page.UI.Game.Renderer.TilemapCount.Width);

                    vbo.UpdateTileData(Position.X + x, Position.Y + y, tileIndex, Color, Tilemap, TileEffect);
                }
        }
    }
}

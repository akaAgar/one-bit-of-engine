using Asterion.Core;
using Asterion.Video;

namespace Asterion.UI
{
    public class UIFrame : UIControl
    {
        public Dimension Size { get; set; } = Dimension.One;

        public bool Filled { get; set; } = false;

        public int FillTile { get; set; } = 0;

        internal override void UpdateVBOTiles(VBO vbo)
        {
            int x, y;
            int frameTileIndex;
            Tile frameTile;

            Area rect = new Area(Position, Size);

            for (x = rect.Left; x < rect.Right; x++)
                for (y = rect.Top; y < rect.Bottom; y++)
                {
                    frameTileIndex = TileID;

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
                    else if (Filled)
                        frameTileIndex = FillTile;
                    else
                        continue;

                    frameTile = new Tile(frameTileIndex, Color, Tilemap);

                    vbo.UpdateTileData(x, y, frameTile);
                }
        }
    }
}

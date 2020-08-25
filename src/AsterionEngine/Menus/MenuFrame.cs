using Asterion.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asterion.Menus
{
    public class MenuFrame : MenuControl
    {
        public Size Size { get; set; } = new Size(1, 1);

        internal override void Render()
        {
            int x, y;
            int frameTileIndex;
            Tile frameTile;

            Rectangle rect = new Rectangle(Position, Size);

            for (x = rect.Left; x < rect.Right; x++)
                for (y = rect.Top; y < rect.Bottom; y++)
                {
                    frameTileIndex = Tile;

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

                    frameTile = new Tile(frameTileIndex, Color, Tilemap);

                    //DrawTile(x, y, frameTile);
                }
        }
    }
}

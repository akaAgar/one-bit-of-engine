using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsterionEngine.Tools.Forms
{
    public partial class TilemapAnimationMakerForm : Form
    {
        public TilemapAnimationMakerForm()
        {
            InitializeComponent();
        }

        private void TilemapAnimationMakerForm_Load(object sender, EventArgs e)
        {

        }

        public Bitmap MakeTileSetAnimations(Bitmap inputImage, int tileWidth, int tileHeight, int animationSteps, int outTilesPerRow)
        {
            animationSteps = Math.Max(1, Math.Min(3, animationSteps));
            List<Bitmap> outTiles = new List<Bitmap>();

            int inputColumns = inputImage.Width / tileWidth;
            int inputRows = inputImage.Height / tileHeight;
            int inputTileCount = inputRows * inputColumns;

            int i, j, x, y;
            for (i = 0; i < inputTileCount; i += animationSteps)
            {
                Bitmap outTile = new Bitmap(tileWidth, tileHeight);
                using (Graphics g = Graphics.FromImage(outTile)) { g.Clear(Color.Black); }

                for (j = 0; j < animationSteps; j++)
                {
                    int tileY = (i + j) / inputColumns;
                    int tileX = (i + j) - (tileY * inputColumns);

                    if (tileX + 1 > inputColumns) continue;
                    if (tileY + 1 > inputRows) continue;

                    for (x = 0; x < tileWidth; x++)
                    {
                        for (y = 0; y < tileHeight; y++)
                        {
                            int tileBrightness = (int)(inputImage.GetPixel(tileX * tileWidth + x, tileY * tileHeight + y).GetBrightness() * 255);

                            Color outPixel = outTile.GetPixel(x, y);
                            switch (j)
                            {
                                case 0: outTile.SetPixel(x, y, Color.FromArgb(255, tileBrightness, outPixel.G, outPixel.B)); break;
                                case 1: outTile.SetPixel(x, y, Color.FromArgb(255, outPixel.R, tileBrightness, outPixel.B)); break;
                                case 2: outTile.SetPixel(x, y, Color.FromArgb(255, outPixel.R, outPixel.G, tileBrightness)); break;
                            }
                        }
                    }
                }

                outTiles.Add(outTile);
            }

            int outputColumns = outTilesPerRow;
            int outputRows = (int)Math.Ceiling((float)outTiles.Count / outTilesPerRow);

            Bitmap outTilemap = new Bitmap(outputColumns * tileWidth, outputRows * tileHeight);

            using (Graphics g = Graphics.FromImage(outTilemap))
            {
                g.Clear(Color.Black);
                for (i = 0; i < outTiles.Count; i++)
                {
                    int tileY = i / outputColumns;
                    int tileX = i - (tileY * outputColumns);

                    g.DrawImage(outTiles[i], new Point(tileX * tileWidth, tileY * tileHeight));
                }
            }

            for (i = 0; i < outTiles.Count; i++) outTiles[i].Dispose();
            outTiles.Clear();

            return outTilemap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MakeTileSetAnimations(
                (Bitmap)Image.FromFile(@"..\..\media\tilemap.png"),
                16, 16, 3, 8).Save("output.png");
        }
    }
}

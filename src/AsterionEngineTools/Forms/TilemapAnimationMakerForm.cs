using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace AsterionEngine.Tools.Forms
{
    public partial class TilemapAnimationMakerForm : Form
    {
        private Bitmap OutputTilemap = null;

        public TilemapAnimationMakerForm()
        {
            InitializeComponent();
        }

        private void TilemapAnimationMakerForm_Load(object sender, EventArgs e)
        {
            SourceTilemapPictureBox.Image = Image.FromFile(@"..\..\media\animationFramesExample.png");
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

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender == LoadImageButton)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    try
                    {
                    ofd.Filter = "PNG images (*.png)|*.png";
                    if (ofd.ShowDialog() != DialogResult.OK) return;
                    SourceTilemapPictureBox.Image = Image.FromFile(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to load image: {ex.Message}");
                    }
                }
            }
            else if (sender == SaveImageButton)
            {
                if (OutputTilemap == null) return;

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    try
                    {
                        sfd.Filter = "PNG images (*.png)|*.png";
                        if (sfd.ShowDialog() != DialogResult.OK) return;
                        OutputTilemap.Save(sfd.FileName, ImageFormat.Png);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save image: {ex.Message}");
                    }
                }
            }
            else if (sender == CreateAnimatedTilemapButton)
            {
                if (SourceTilemapPictureBox.Image == null) return;
                OutputImagePictureBox.Image = null;
                if (OutputTilemap != null) OutputTilemap.Dispose();
                OutputTilemap = null;

                try
                {
                    OutputTilemap = MakeTileSetAnimations(
                        (Bitmap)SourceTilemapPictureBox.Image,
                        (int)TileWidthNumericUpDown.Value, (int)TileHeightNumericUpDown.Value,
                        (int)AnimationFramesLabelNumericUpDown.Value, SourceTilemapPictureBox.Width / (int)TileWidthNumericUpDown.Value);

                    OutputImagePictureBox.Image = OutputTilemap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to create image: {ex.Message}");
                }
            }
        }
    }
}

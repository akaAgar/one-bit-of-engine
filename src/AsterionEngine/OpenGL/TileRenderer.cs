using Asterion.Core;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.IO;

namespace Asterion.OpenGL
{
    /// <summary>
    /// (Internal) A class handling low-level OpenGL calls to draw the tiles.
    /// </summary>
    internal sealed class TileRenderer : IDisposable
    {
        /// <summary>
        /// Maximum number of tilemaps.
        /// </summary>
        internal const int TILEMAP_COUNT = 4;

        /// <summary>
        /// The GLSL shader used to draw the tiles.
        /// </summary>
        private TileShader Shader = null;

        /// <summary>
        /// An array of textures storing the tilemaps.
        /// </summary>
        private readonly TilemapTexture[] Tilemaps = new TilemapTexture[TILEMAP_COUNT];

        /// <summary>
        /// Background color for the frame.
        /// </summary>
        internal Color4 BackgroundColor { get; set; } = Color4.Black;

        /// <summary>
        /// Scale of the tiles at the current window size.
        /// Automatically updated when the game window is resized.
        /// </summary>
        private float TileScale = 1.0f;

        /// <summary>
        /// Offset between the upper-left corner of the game window and the upper-leftmost tile.
        /// Automatically updated when the game window is resized.
        /// </summary>
        private Position TileOffset = Position.Zero;

        /// <summary>
        /// Size of a each tile, in pixels.
        /// </summary>
        private readonly Dimension TileSize;

        /// <summary>
        /// Number of tiles on the tile board.
        /// </summary>
        private readonly Dimension TileCount;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tileSize">Size of a each tile, in pixels.</param>
        /// <param name="tileCount">Number of tiles on the tile board.</param>
        internal TileRenderer(Dimension tileSize, Dimension tileCount)
        {
            TileSize = tileSize;
            TileCount = tileCount;
        }

        /// <summary>
        /// Called before the first game loop.
        /// </summary>
        internal void OnLoad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest); // TODO: Remove?
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            Shader = new TileShader();
        }

        /// <summary>
        /// (Internal) Sets the tilemap
        /// </summary>
        /// <param name="index">Index of the tilemap to load, from 0 to <see cref="TILEMAP_COUNT"/></param>
        /// <param name="textureStream">A stream containing data for a System.Drawing.Image</param>
        /// <returns>True if everything went right, false otherwise</returns>
        public bool SetTilemap(int index, Stream textureStream)
        {
            DestroyTileMap(index);
            if (textureStream == null) return false;
            Tilemaps[index] = new TilemapTexture(textureStream);
            return true;
        }

        /// <summary>
        /// (Private) Removes a tilemap from memory.
        /// </summary>
        /// <param name="index">Index of the tilemap to destroy, from 0 to <see cref="TILEMAP_COUNT"/></param>
        private void DestroyTileMap(int index)
        {
            if ((index < 0) || (index >= TILEMAP_COUNT)) return;
            if (Tilemaps[index] == null) return;

            Tilemaps[index].Dispose();
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < TILEMAP_COUNT; i++)
                DestroyTileMap(i);
        }

        /// <summary>
        /// Called each time the game window is resized, recomputes the scale and offset of the tiles.
        /// </summary>
        /// <param name="width">The width of the game window, in pixels</param>
        /// <param name="height">The height of the game window, in pixels</param>
        internal void OnResize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);

            TileScale =
                Math.Min((float)width /
                (TileCount.Width * TileSize.Width), (float)height / (TileCount.Height * TileSize.Height));

            float resScale = (float)width / height;
            float ratio = (float)(TileCount.Width * TileSize.Width) / (TileCount.Height * TileSize.Height);
            RectangleF quad = new RectangleF(0, 0, TileCount.Width, TileCount.Height);

            int tileOffsetX = 0, tileOffsetY = 0;
            if (resScale > ratio)
            {
                quad.Width *= (resScale / ratio);
                quad.X = (TileCount.Width - quad.Width) / 2;
                tileOffsetX = (int)((width - (TileCount.Width * TileSize.Width * TileScale)) * .5f);
            }
            else
            {
                quad.Height /= (resScale / ratio);
                quad.Y = (TileCount.Height - quad.Height) / 2;
                tileOffsetY = (int)((height - (TileCount.Height * TileSize.Height * TileScale)) * .5f);
            }
            TileOffset = new Position(tileOffsetX, tileOffsetY);

            Shader.SetProjection(Matrix4.CreateOrthographicOffCenter(quad.Left, quad.Right, quad.Bottom, quad.Top, 0, 1));
        }

        /// <summary>
        /// Setups the rendering pipeline before rendering a new frame.
        /// </summary>
        internal void SetupFrame()
        {
            GL.ClearColor(BackgroundColor);
            Shader.Use();

            for (int i = 0; i < TILEMAP_COUNT; i++)
                Tilemaps[i]?.Use(i);
        }

        /// <summary>
        /// Returns the coordinates of the tile currently hovered by the mouse cursor.
        /// </summary>
        /// <param name="mouse">The position of the cursor, in pixels from the top-left corner of the game window</param>
        /// <returns>The coordinates of the tile, or null if the mouse cursor is not above a tile</returns>
        internal Position? GetTileFromMousePosition(Position mouse)
        {
            float tileX = (mouse.X - TileOffset.X) / (TileSize.Width * TileScale);
            float tileY = (mouse.Y - TileOffset.Y) / (TileSize.Height * TileScale);

            if (
                (tileX < 0) || (tileY < 0) ||
                ((int)tileX >= TileCount.Width) || ((int)tileY >= TileCount.Height)
                )
                return null;

            return new Position((int)tileX, (int)tileY);
        }
    }
}

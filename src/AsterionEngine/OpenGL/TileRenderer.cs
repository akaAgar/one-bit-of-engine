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
        /// Maximum number of tilemaps
        /// </summary>
        public const int TILEMAP_COUNT = 4;

        private TileShader Shader = null;

        private readonly TilemapTexture[] Tilemaps = new TilemapTexture[TILEMAP_COUNT];

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

        internal TileRenderer()
        {

        }

        internal void OnLoad()
        {
            GL.ClearColor(BackgroundColor);
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

        internal void OnResize(AsterionGame game, int width, int height)
        {
            GL.Viewport(0, 0, width, height);

            TileScale =
                Math.Min((float)width /
                (game.TileCount.Width * game.TileSize.Width), (float)height / (game.TileCount.Height * game.TileSize.Height));

            float resScale = (float)width / height;
            float ratio = (float)(game.TileCount.Width * game.TileSize.Width) / (game.TileCount.Height * game.TileSize.Height);
            RectangleF quad = new RectangleF(0, 0, game.TileCount.Width, game.TileCount.Height);

            int tileOffsetX = 0, tileOffsetY = 0;
            if (resScale > ratio)
            {
                quad.Width *= (resScale / ratio);
                quad.X = (game.TileCount.Width - quad.Width) / 2;
                tileOffsetX = (int)((width - (game.TileCount.Width * game.TileSize.Width * TileScale)) * .5f);
            }
            else
            {
                quad.Height /= (resScale / ratio);
                quad.Y = (game.TileCount.Height - quad.Height) / 2;
                tileOffsetY = (int)((height - (game.TileCount.Height * game.TileSize.Height * TileScale)) * .5f);
            }
            TileOffset = new Position(tileOffsetX, tileOffsetY);

            Shader.SetProjection(Matrix4.CreateOrthographicOffCenter(quad.Left, quad.Right, quad.Bottom, quad.Top, 0, 1));
        }

        internal void SetupFrame()
        {
            Shader.Use();

            for (int i = 0; i < TILEMAP_COUNT; i++)
                Tilemaps[i]?.Use(i);
        }

        internal Position? GetTileFromMousePosition(AsterionGame game, Position mouse)
        {
            float tileX = (mouse.X - TileOffset.X) / (game.TileSize.Width * TileScale);
            float tileY = (mouse.Y - TileOffset.Y) / (game.TileSize.Height * TileScale);

            if (
                (tileX < 0) || (tileY < 0) ||
                ((int)tileX >= game.TileCount.Width) || ((int)tileY >= game.TileCount.Height)
                )
                return null;

            return new Position((int)tileX, (int)tileY);
        }
    }
}

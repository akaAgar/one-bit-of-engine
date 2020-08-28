using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;

namespace Asterion.Video
{
    public sealed class TileManager
    {
        /// <summary>
        /// Maximum number of tilemaps
        /// </summary>
        public const int TILEMAP_COUNT = 4;

        public int TileCountX { get; private set; } = 52;
        public int TileCountY { get; private set; } = 19;

        public int TileWidth { get; private set; } = 16;
        public int TileHeight { get; private set; } = 24;

        public int TilemapWidth { get; private set; } = 512;
        public int TilemapHeight { get; private set; } = 512;

        public int TilemapCountX { get { return TilemapWidth / TileWidth; } }
        public int TilemapCountY { get { return TilemapHeight / TileHeight; } }

        /// <summary>
        /// Background color.
        /// </summary>
        public Color BackgroundColor { get { return _backgroundColor; } set { _backgroundColor = value; GL.ClearColor(value); } }
        private Color _backgroundColor = Color.Black;

        private float TileScale = 1.0f;
        private Point TileOffset = Point.Empty;

        private readonly AsterionGame Game = null;

        private TileShader Shader = null;

        private readonly TilemapTexture[] Tilemaps = new TilemapTexture[TILEMAP_COUNT];

        internal TileManager(AsterionGame game, Size tileSize, Size tileCount, Size tilemapSize)
        {
            Game = game;

            TileWidth = Math.Max(1, tileSize.Width);
            TileHeight = Math.Max(1, tileSize.Height);
            TileCountX = Math.Max(1, tileCount.Width);
            TileCountY = Math.Max(1, tileCount.Height);
            TilemapWidth = Math.Max(1, tilemapSize.Width);
            TilemapHeight = Math.Max(1, tilemapSize.Height);
        }

        internal void Dispose()
        {
            if (Shader != null)
                Shader.Dispose();
        }

        internal void OnLoad()
        {
            GL.ClearColor(_backgroundColor);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest); // TODO: Remove?
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // TODO: Use texture arrays - https://community.khronos.org/t/how-do-you-implement-texture-arrays/75315
            Shader = new TileShader();
        }

        public void SetAnimatedTiles(int tilemap, params int[] tiles)
        {
            // TODO
        }

        internal void OnRenderFrame()
        {
            Shader.Use();

            for (int i = 0; i < TILEMAP_COUNT; i++)
                Tilemaps[i]?.Use(i);

            GL.Disable(EnableCap.Blend);
            Game.GUI.RenderInterface();
            Game.Menu.Render();
            Game.Scene.OnRenderFrame();
            Game.Board.OnRenderFrame();
            GL.Enable(EnableCap.Blend);
            Game.GUI.RenderCursor();
        }

        internal void OnResize()
        {
            GL.Viewport(0, 0, Game.Width, Game.Height);

            TileScale = Math.Min((float)Game.Width / (TileCountX * TileWidth), (float)Game.Height / (TileCountY * TileHeight));

            float resScale = (float)Game.Width / Game.Height;
            float ratio = (float)(TileCountX * TileWidth) / (TileCountY * TileHeight);
            RectangleF quad = new RectangleF(0, 0, TileCountX, TileCountY);

            TileOffset = Point.Empty;
            if (resScale > ratio)
            {
                quad.Width *= (resScale / ratio);
                quad.X = (TileCountX - quad.Width) / 2;
                TileOffset.X = (int)((Game.Width - (TileCountX * TileWidth * TileScale)) * .5f);
            }
            else
            {
                quad.Height /= (resScale / ratio);
                quad.Y = (TileCountY - quad.Height) / 2;
                TileOffset.Y = (int)((Game.Height - (TileCountY * TileHeight * TileScale)) * .5f);
            }

            Shader.SetProjection(Matrix4.CreateOrthographicOffCenter(quad.Left, quad.Right, quad.Bottom, quad.Top, 0, 1));
        }

        public Position GetTileFromCursorPosition(int cursorX, int cursorY)
        {
            float tileX = (cursorX - TileOffset.X) / (TileWidth * TileScale);
            float tileY = (cursorY - TileOffset.Y) / (TileHeight * TileScale);

            if (
                (tileX < 0) || (tileY < 0) ||
                ((int)tileX >= TileCountX) || ((int)tileY >= TileCountY)
                )
                return Position.NegativeOne;

            return new Position((int)tileX, (int)tileY);
        }

        public bool SetTilemap(int index, Image tilemap)
        {
            if ((index < 0) || (index >= TILEMAP_COUNT)) return false;

            DestroyTileMap(index);
            Tilemaps[index] = new TilemapTexture(tilemap);
            return true;
        }

        private void DestroyTileMap(int index)
        {
            if ((index < 0) || (index >= TILEMAP_COUNT)) return;
            if (Tilemaps[index] == null) return;

            Tilemaps[index].Dispose();
        }

        internal void OnUpdate(float elapsedSeconds)
        {
            // TODO
        }
    }
}

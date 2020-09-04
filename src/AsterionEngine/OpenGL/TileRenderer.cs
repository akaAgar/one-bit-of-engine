using Asterion.Core;
using Asterion.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.IO;

namespace Asterion.OpenGL
{
    /// <summary>
    /// A class handling low-level OpenGL calls to draw the tiles.
    /// </summary>
    public sealed class TileRenderer
    {
        /// <summary>
        /// Maximum number of tilemaps.
        /// </summary>
        public const int TILEMAP_COUNT = 4;

        /// <summary>
        /// Background color for the frame.
        /// </summary>
        public RGBColor BackgroundColor { get; set; } = RGBColor.Black;

        /// <summary>
        /// The GLSL shader used to draw the tiles.
        /// </summary>
        private TileShader Shader = null;

        /// <summary>
        /// An array of textures storing the tilemaps.
        /// </summary>
        private readonly TilemapTexture[] Tilemaps = new TilemapTexture[TILEMAP_COUNT];

        /// <summary>
        /// Scale of the tiles at the current window size.
        /// Automatically updated when the game window is resized.
        /// </summary>
        public float TileScale { get; private set; }

        /// <summary>
        /// Offset between the upper-left corner of the game window and the upper-leftmost tile.
        /// Automatically updated when the game window is resized.
        /// </summary>
        public Position TileOffset { get; private set; }

        /// <summary>
        /// Size of a each tile, in pixels.
        /// </summary>
        public Dimension TileSize { get; }

        /// <summary>
        /// Number of tiles on the tile board.
        /// </summary>
        public Dimension TileCount { get; }

        /// <summary>
        /// Size of the tilemaps images to be loaded with <see cref="LoadTilemap(int, string)"/>, in pixels.
        /// </summary>
        public Dimension TilemapSize { get; }

        /// <summary>
        /// Number of tiles on each tilemap.
        /// </summary>
        public Dimension TilemapCount { get; }

        /// <summary>
        /// (Internal) UV size of each tile on a tilemap.
        /// </summary>
        internal SizeF TileUV { get; }

        /// <summary>
        /// (Private) The FileSystem from which tilemaps will be loaded.
        /// </summary>
        private readonly FileSystem Files = null;

        /// <summary>
        /// The number of frames in each frame animation. Minimum is 1, maximum is 3, default is 2.
        /// </summary>
        public int TileAnimationFrames { get { return TileAnimationFrames_; } set { TileAnimationFrames_ = AsterionTools.Clamp(value, 1, 3); ResetAnimation(); } }
        private int TileAnimationFrames_ = 2;

        /// <summary>
        /// The duration (in seconds) of each frame of animated tiles. Minimum is 0.1, maximum is 10.0, default is 1.0.
        /// </summary>
        public float TileAnimationFrameDuration { get { return TileAnimationFrameDuration_; } set { TileAnimationFrameDuration_ = AsterionTools.Clamp(value, .1f, 10.0f); ResetAnimation(); } }
        private float TileAnimationFrameDuration_ = 1.0f;

        /// <summary>
        /// Current animation frame.
        /// </summary>
        private int CurrentAnimationFrame = 0;

        /// <summary>
        /// Time elapsed (in seconds) on the current animation frame.
        /// </summary>
        private float CurrentAnimationFrameTime = 0.0f;

        /// <summary>
        /// Total elapsed time since the renderer was created.
        /// </summary>
        private float TotalElapsedSeconds = 0f;

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        /// <param name="files">The FileSystem from which tilemaps will be loaded</param>
        /// <param name="tileSize">Size of a each tile, in pixels</param>
        /// <param name="tileCount">Number of tiles on the tile board</param>
        /// <param name="tilemapSize">The size of the tilemaps, in pixels</param>
        internal TileRenderer(FileSystem files, Dimension tileSize, Dimension tileCount, Dimension tilemapSize)
        {
            Files = files;

            TileSize = new Dimension(Math.Max(1, tileSize.Width), Math.Max(1, tileSize.Height));
            TileCount = new Dimension(Math.Max(1, tileCount.Width), Math.Max(1, tileCount.Height));
            TilemapSize = new Dimension(Math.Max(1, tilemapSize.Width), Math.Max(1, tilemapSize.Height));
            TilemapCount = new Dimension(TilemapSize.Width / TileSize.Width, TilemapSize.Height / TileSize.Height);
            TileUV = new SizeF(
                (float)tileSize.Width / tilemapSize.Width,
                (float)tileSize.Height / tilemapSize.Height);

            TotalElapsedSeconds = 0f;
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

            Shader = new TileShader(TileUV);
            ResetAnimation();
        }

        /// <summary>
        /// Loads a tilemap from a file stored in the engine's filesytem.
        /// </summary>
        /// <param name="index">Index of the tilemap to load, from 0 to <see cref="TILEMAP_COUNT"/></param>
        /// <param name="file">The name of the image file, as it appears in this game's filesystem</param>
        /// <returns>True if everything went right, false otherwise</returns>
        public bool LoadTilemap(int index, string file)
        {
            if ((index < 0) || (index >= TileRenderer.TILEMAP_COUNT)) return false;
            if (!Files.FileExists(file)) return false;

            using (Stream textureStream = Files.GetFileAsStream(file))
            {
                try
                {
                    UnloadTileMap(index);
                    if (textureStream == null) return false;
                    Tilemaps[index] = new TilemapTexture(textureStream);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// (Internal) Called on each main loop update. Updates frame animations and VFX in the shader.
        /// </summary>
        /// <param name="elapsedSeconds">Seconds elapsed since the last update</param>
        internal void OnUpdate(float elapsedSeconds)
        {
            TotalElapsedSeconds += elapsedSeconds;
            Shader.SetTime(TotalElapsedSeconds);

            if (TileAnimationFrames_ > 1) // No need to process animation logic if animations only have one frame (aka, "no animation")
            {
                CurrentAnimationFrameTime += elapsedSeconds;

                if (CurrentAnimationFrameTime >= TileAnimationFrameDuration_)
                {
                    CurrentAnimationFrameTime = 0;
                    CurrentAnimationFrame++;

                    // 3-frames animation actually have 4 steps, to allow for a "ping-pong" 0-1-2-1 animation
                    int totalFrameSteps = (TileAnimationFrames_ == 3) ? 4 : TileAnimationFrames_;
                    CurrentAnimationFrame %= totalFrameSteps;
                    Shader.SetAnimationFrame((CurrentAnimationFrame == 3) ? 1 : CurrentAnimationFrame);
                }
            }
        }

        /// <summary>
        /// (Private) Removes a tilemap from memory.
        /// </summary>
        /// <param name="index">Index of the tilemap to destroy, from 0 to <see cref="TILEMAP_COUNT"/></param>
        private void UnloadTileMap(int index)
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
                UnloadTileMap(i);
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
            GL.ClearColor(BackgroundColor.ToColor4());
            Shader.Use();

            for (int i = 0; i < TILEMAP_COUNT; i++)
                Tilemaps[i]?.Use(i);
        }

        /// <summary>
        /// Resets animation timer and sets the current frame to 0.
        /// </summary>
        private void ResetAnimation()
        {
            CurrentAnimationFrameTime = 0;
            CurrentAnimationFrame = 0;
            Shader.SetAnimationFrame(0);
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

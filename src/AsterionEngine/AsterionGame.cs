/*
==========================================================================
This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
WadPacker is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
WadPacker is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with Asterion Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using Asterion.Audio;
using Asterion.Input;
using Asterion.IO;
using Asterion.UI;
using Asterion.Scene;
using Asterion.Video;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.IO;

namespace Asterion
{
    /// <summary>
    /// The AsterionGame class contains everything required to create and run a game. Just override it.
    /// </summary>
    public class AsterionGame : IDisposable
    {
        /// <summary>
        /// Maximum number of tilemaps
        /// </summary>
        public const int TILEMAP_COUNT = 4;

        /// <summary>
        /// Audio player.
        /// </summary>
        public AudioPlayer Audio { get; private set; }

        /// <summary>
        /// Background color.
        /// </summary>
        public Color BackgroundColor { get { return _backgroundColor; } set { _backgroundColor = value; GL.ClearColor(value); } }
        private Color _backgroundColor = Color.Black;

        private float TileScale = 1.0f;
        private Position TileOffset = Position.Zero;

        private TileShader Shader = null;
        private readonly TilemapTexture[] Tilemaps = new TilemapTexture[TILEMAP_COUNT];

        /// <summary>
        /// Gets or sets the width of the game window, in pixels.
        /// </summary>
        public int Width { get { return OpenTKWindow.Width; } set { OpenTKWindow.Width = Math.Max(1, value); } }

        /// <summary>
        /// Gets or sets the height of the game window, in pixels.
        /// </summary>
        public int Height { get { return OpenTKWindow.Height; } set { OpenTKWindow.Height = Math.Max(1, value); } }

        /// <summary>
        /// Gets or sets the game window title.
        /// </summary>
        public string Title { get { return OpenTKWindow.Title; } set { OpenTKWindow.Title = value; } }

        /// <summary>
        /// VSync status.
        /// </summary>
        public VSync VSync { get { return (VSync)OpenTKWindow.VSync; } set { OpenTKWindow.VSync = (VSyncMode)value; } }

        /// <summary>
        /// Window border type.
        /// </summary>
        public GameWindowBorder WindowBorder { get { return (GameWindowBorder)OpenTKWindow.WindowBorder; } set { OpenTKWindow.WindowBorder = (WindowBorder)value; } }

        /// <summary>
        /// Window state.
        /// </summary>
        public GameWindowState WindowState { get { return (GameWindowState)OpenTKWindow.WindowState; } set { OpenTKWindow.WindowState = (WindowState)value; } }

        /// <summary>
        /// Mouse cursor visibility.
        /// </summary>
        public bool MouseCursorVisible { get { return OpenTKWindow.CursorVisible; } set { OpenTKWindow.CursorVisible = value; } }

        internal void OnResizeInternal()
        {
            GL.Viewport(0, 0, Width, Height);

            TileScale = Math.Min((float)Width / (TileCount.Width * TileSize.Width), (float)Height / (TileCount.Height * TileSize.Height));

            float resScale = (float)Width / Height;
            float ratio = (float)(TileCount.Width * TileSize.Width) / (TileCount.Height * TileSize.Height);
            RectangleF quad = new RectangleF(0, 0, TileCount.Width, TileCount.Height);

            int tileOffsetX = 0, tileOffsetY = 0;
            if (resScale > ratio)
            {
                quad.Width *= (resScale / ratio);
                quad.X = (TileCount.Width - quad.Width) / 2;
                tileOffsetX = (int)((Width - (TileCount.Width * TileSize.Width * TileScale)) * .5f);
            }
            else
            {
                quad.Height /= (resScale / ratio);
                quad.Y = (TileCount.Height - quad.Height) / 2;
                tileOffsetY = (int)((Height - (TileCount.Height * TileSize.Height * TileScale)) * .5f);
            }
            TileOffset = new Position(tileOffsetX, tileOffsetY);

            Shader.SetProjection(Matrix4.CreateOrthographicOffCenter(quad.Left, quad.Right, quad.Bottom, quad.Top, 0, 1));
        }

        /// <summary>
        /// Internal OpenTK window.
        /// </summary>
        private readonly OpenTKWindow OpenTKWindow = null;

        public SceneManager Scene { get; private set; } = null;
        public UIEnvironment UI { get; private set; } = null;

        //public TileManager Tiles { get; private set; } = null;
        public InputManager Input { get; private set; } = null;

        public FileSystem Files { get; private set; } = null;

        /// <summary>
        /// Closes the game.
        /// </summary>
        public void Close() { OpenTKWindow.Close(); }

        public Dimension TileSize { get; } = Dimension.One;
        public Dimension TileCount { get; } = Dimension.One;
        public Dimension TilemapSize { get; } = Dimension.One;

        public Dimension TilemapCount { get { return new Dimension(TilemapSize.Width / TileSize.Width, TilemapSize.Height / TileSize.Height); } }

        /// <summary>
        /// Creates a new Asterion Engine game.
        /// </summary>
        /// <param name="tileSize">Size of a each tile, in pixels.</param>
        /// <param name="tileCount">Number of tiles on the tile board.</param>
        /// <param name="tilemapSize">Size of the tilemaps, in pixels.</param>
        public AsterionGame(Dimension tileSize, Dimension tileCount, Dimension tilemapSize)
        {
            OpenTKWindow = new OpenTKWindow(this) { Title = "Asterion Engine" };

            TileSize = tileSize;
            TileCount = tileCount;
            TilemapSize = tilemapSize;

            Files = new FileSystem();

            Audio = new AudioPlayer(Files);
            //Tiles = new TileManager(this, tileSize, tileCount, tilemapSize);
            Input = new InputManager();

            UI = new UIEnvironment(this);
            Scene = new SceneManager(this);
        }

        /// <summary>
        /// Internal OnLoad method, called just before the main loop starts.
        /// </summary>
        internal void OnLoadInternal()
        {
            GL.ClearColor(_backgroundColor);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest); // TODO: Remove?
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // TODO: Use texture arrays - https://community.khronos.org/t/how-do-you-implement-texture-arrays/75315
            Shader = new TileShader();

            Scene.OnLoad();
            UI.OnLoad();

            OnLoad();
        }

        /// <summary>
        /// Internal OnUpdate method, called on each album cycle.
        /// </summary>
        /// <param name="elapsedSeconds">Number of seconds elapsed since the last update.</param>
        internal void OnUpdateInternal(float elapsedSeconds)
        {
            Scene.OnUpdate(elapsedSeconds);
            Input.OnUpdate();
            OnUpdate(elapsedSeconds);
        }

        /// <summary>
        /// Begins the main loop at 30 frames/updates per second.
        /// </summary>
        public void Run() { Run(30.0f); }

        /// <summary>
        /// Begins the main loop.
        /// </summary>
        /// <param name="updateRate">Number of frames/updates per second.</param>
        public void Run(float updateRate) { OpenTKWindow.Run(updateRate); }

        /// <summary>
        /// Adjust the game window size so it matches the size and number of tiles.
        /// </summary>
        /// <param name="scale">Scale, 1.0 means 1:1 scale, 2.0 means 200%, etc.</param>
        public void AdjustToTileScreenSize(float scale = 1.0f)
        {
            OpenTKWindow.WindowState = OpenTK.WindowState.Normal;
            OpenTKWindow.Size = new Size(
                (int)(TileCount.Width * TileSize.Width * scale),
                (int)(TileCount.Height * TileSize.Height * scale));
        }

        /// <summary>
        /// Called after the OpenGL context has been established, but before entering the main loop.
        /// </summary>
        protected virtual void OnLoad() { }

        /// <summary>
        /// Called when this window is resized.
        /// </summary>
        /// <param name="width">The new width of the game window.</param>
        /// <param name="height">The new height of the game window.</param>
        protected virtual void OnResize(int width, int height) { }

        /// <summary>
        /// Called when the frame is updated.
        /// </summary>
        /// <param name="elapsedSeconds">How many seconds since the last frame?</param>
        protected virtual void OnUpdate(float elapsedSeconds) { }

        /// <summary>
        /// Called when the game is disposed.
        /// </summary>
        protected virtual void OnDispose() { }

        public Position GetTileFromMousePosition(int mouseX, int mouseY)
        {
            float tileX = (mouseX - TileOffset.X) / (TileSize.Width * TileScale);
            float tileY = (mouseY - TileOffset.Y) / (TileSize.Height * TileScale);

            if (
                (tileX < 0) || (tileY < 0) ||
                ((int)tileX >= TileCount.Width) || ((int)tileY >= TileCount.Height)
                )
                return Position.NegativeOne;

            return new Position((int)tileX, (int)tileY);
        }

        internal void OnRenderFrame()
        {
            Shader.Use();

            for (int i = 0; i < TILEMAP_COUNT; i++)
                Tilemaps[i]?.Use(i);

            GL.Disable(EnableCap.Blend);
            UI.Render();
            Scene.OnRenderFrame();
            GL.Enable(EnableCap.Blend);
            UI.Cursor.Render();
        }

        public bool SetTilemap(int index, string file)
        {
            if ((index < 0) || (index >= TILEMAP_COUNT)) return false;
            if (!Files.FileExists(file)) return false;

            DestroyTileMap(index);

            using (Stream s = Files.GetFileAsStream(file))
            {
                if (s == null) return false;

                Tilemaps[index] = new TilemapTexture(s);
            }

            return true;
        }

        private void DestroyTileMap(int index)
        {
            if ((index < 0) || (index >= TILEMAP_COUNT)) return;
            if (Tilemaps[index] == null) return;

            Tilemaps[index].Dispose();
        }

        /// <summary>
        /// IDispose implementation. Closes and destroys the game.
        /// </summary>
        public void Dispose()
        {
            UI.Dispose();
            Scene.Dispose();

            Audio.Destroy();
            OpenTKWindow.Dispose();
            Input.Dispose();

            OnDispose();
        }
    }
}

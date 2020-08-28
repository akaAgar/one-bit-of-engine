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
using Asterion.UI;
using Asterion.Scene;
using Asterion.Video;
using OpenTK;
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
        /// Audio player.
        /// </summary>
        public AudioPlayer Audio { get; private set; }

        /// <summary>
        /// Tileboard, handles the drawing of tiles.
        /// </summary>
        //public TileBoard TileBoard { get; private set; }

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

        /// <summary>
        /// Internal OpenTK window.
        /// </summary>
        private readonly OpenTKWindow OpenTKWindow = null;

        public SceneManager Scene { get; private set; } = null;
        public UIEnvironment UI { get; private set; } = null;

        public TileManager Tiles { get; private set; } = null;
        public InputManager Input { get; private set; } = null;

        /// <summary>
        /// Closes the game.
        /// </summary>
        public void Close() { OpenTKWindow.Close(); }

        /// <summary>
        /// Creates a new Asterion Engine game.
        /// </summary>
        /// <param name="tileSize">Size of a each tile, in pixels.</param>
        /// <param name="tileCount">Number of tiles on the tile board.</param>
        /// <param name="tilemapSize">Size of the tilemaps, in pixels.</param>
        public AsterionGame(Size tileSize, Size tileCount, Size tilemapSize)
        {
            OpenTKWindow = new OpenTKWindow(this) { Title = "One Bit of Engine" };

            Audio = new AudioPlayer();
            Tiles = new TileManager(this, tileSize, tileCount, tilemapSize);
            Input = new InputManager();

            UI = new UIEnvironment(this);
            Scene = new SceneManager(this);
        }

        /// <summary>
        /// Internal OnLoad method, called just before the main loop starts.
        /// </summary>
        internal void OnLoadInternal()
        {
            Tiles.OnLoad();
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
            Tiles.OnUpdate(elapsedSeconds);
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
                (int)(Tiles.TileCountX * Tiles.TileWidth * scale),
                (int)(Tiles.TileCountY * Tiles.TileHeight * scale));
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

        /// <summary>
        /// IDispose implementation. Closes and destroys the game.
        /// </summary>
        public void Dispose()
        {
            UI.Dispose();
            Scene.Dispose();

            Audio.Dispose();
            OpenTKWindow.Dispose();
            Input.Dispose();

            OnDispose();
        }
    }
}

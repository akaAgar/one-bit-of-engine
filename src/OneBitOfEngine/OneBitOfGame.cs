/*
==========================================================================
This file is part of One Bit of Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
One Bit of Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
One Bit of Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with One Bit of Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using OneBitOfEngine.Audio;
using OneBitOfEngine.Core;
using OneBitOfEngine.Input;
using OneBitOfEngine.IO;
using OneBitOfEngine.Sprites;
using OneBitOfEngine.UI;
using OneBitOfEngine.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;

namespace OneBitOfEngine
{
    /// <summary>
    /// The OneBitOfGame class contains everything required to create and run a game.
    /// Override <see cref="OnLoad"/> and <see cref="OnUpdate(float)"/> and you're set.
    /// </summary>
    public class OneBitOfGame : IDisposable
    {
        /// <summary>
        /// Audio player.
        /// </summary>
        public AudioPlayer Audio { get; private set; }

        /// <summary>
        /// Background color.
        /// </summary>
        public Color BackgroundColor { get { return _backgroundColor; } set { _backgroundColor = value; GL.ClearColor(value); } }
        private Color _backgroundColor = Color.Black;

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
        /// State of the game window.
        /// </summary>
        public GameWindowState WindowState { get { return (GameWindowState)OpenTKWindow.WindowState; } set { OpenTKWindow.WindowState = (WindowState)value; } }

        /// <summary>
        /// Mouse cursor visibility.
        /// </summary>
        public bool MouseCursorVisible { get { return OpenTKWindow.CursorVisible; } set { OpenTKWindow.CursorVisible = value; } }

        internal void OnResizeInternal()
        {
            Renderer.OnResize(Width, Height);
            OnResize(Width, Height);
        }

        /// <summary>
        /// (Internal) OpenTK window.
        /// </summary>
        private readonly OpenTKWindow OpenTKWindow = null;

        /// <summary>
        /// (Internal) Processes a InputEvent event.
        /// </summary>
        /// <param name="key">The key or gamepad button that raised the event</param>
        /// <param name="modifiers">Which modifier keys are down?</param>
        /// <param name="gamepadIndex">Index of the gamepad that raised the event, if the key is a gamepad button, or -1 if it was a keyboard key</param>
        /// <param name="isRepeat">Is this a "repeated key press" event, automatically generated while the used holds the key down?</param>
        internal void OnInputEventInternal(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            UI.OnInputEvent(key, modifiers, gamepadIndex, isRepeat);
            OnInputEvent(key, modifiers, gamepadIndex, isRepeat);
        }

        /// <summary>
        /// (Protected) Overridable method to perform custom logic after an input event is raised.
        /// Base method doesn't do anything, so there is no need to call base.OnInputEvent.
        /// </summary>
        /// <param name="key">The key or gamepad button that raised the event</param>
        /// <param name="modifiers">Which modifier keys are down?</param>
        /// <param name="gamepadIndex">Index of the gamepad that raised the event, if the key is a gamepad button, or -1 if it was a keyboard key</param>
        /// <param name="isRepeat">Is this a "repeated key press" event, automatically generated while the used holds the key down?</param>
        protected virtual void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {

        }

        /// <summary>
        /// UI Environment, draws menus and UI controls.
        /// </summary>
        public UIEnvironment UI { get; } = null;

        /// <summary>
        /// Input manager. Handles keyboard, mouse and gamepad events.
        /// </summary>
        public InputManager Input { get; } = null;

        /// <summary>
        /// File system. Loads files from a folder or an archive.
        /// </summary>
        public FileSystem Files { get; } = null;

        /// <summary>
        /// Tile renderer.
        /// </summary>
        public TileRenderer Renderer { get; } = null;

        /// <summary>
        /// Tile renderer.
        /// </summary>
        public SpriteManager Sprites { get; } = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tileSize">Size of a each tile, in pixels.</param>
        /// <param name="tileCount">Number of tiles on the tile board.</param>
        /// <param name="tilemapSize">Size of the tilemaps images to be loaded with <see cref="SetTilemap(int, string)"/>, in pixels.</param>
        public OneBitOfGame(Dimension tileSize, Dimension tileCount, Dimension tilemapSize)
        {
            OpenTKWindow = new OpenTKWindow(this) { Title = "" };

            Files = new FileSystem();
            Renderer = new TileRenderer(Files, tileSize, tileCount, tilemapSize);
            Audio = new AudioPlayer(Files);
            Sprites = new SpriteManager();

            UI = new UIEnvironment(this);
            Input = new InputManager(this);
        }

        /// <summary>
        /// Internal OnLoad method, called just before the main loop starts.
        /// </summary>
        internal void OnLoadInternal()
        {
            Renderer.OnLoad();
            UI.OnLoad();
            Sprites.OnLoad(Renderer);
            OnLoad();
        }

        /// <summary>
        /// (Internal) Internal OnUpdate method, called on each update cycle.
        /// </summary>
        /// <param name="elapsedSeconds">Number of seconds elapsed since the last update.</param>
        internal void OnUpdateInternal(float elapsedSeconds)
        {
            Renderer.OnUpdate(elapsedSeconds);
            Input.OnUpdate(elapsedSeconds);
            Sprites.OnUpdate(elapsedSeconds);
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
                (int)(Renderer.TileCount.Width * Renderer.TileSize.Width * scale),
                (int)(Renderer.TileCount.Height * Renderer.TileSize.Height * scale));
        }

        /// <summary>
        /// (Protected) Called just before the first update of the main loop.
        /// Override with your initialization logic.
        /// </summary>
        protected virtual void OnLoad() { }

        /// <summary>
        /// (Protected) Called when the game window is resized.
        /// </summary>
        /// <param name="width">The new width of the game window.</param>
        /// <param name="height">The new height of the game window.</param>
        protected virtual void OnResize(int width, int height) { }

        /// <summary>
        /// (Protected) Called on each frame update.
        /// Override with your main loop logic.
        /// </summary>
        /// <param name="elapsedSeconds">How many seconds since the last frame?</param>
        protected virtual void OnUpdate(float elapsedSeconds) { }

        /// <summary>
        /// (Protected) Called when the game is disposed.
        /// Override with your closing/finalization logic.
        /// </summary>
        protected virtual void OnDispose() { }

        /// <summary>
        /// Returns the coordinates of the tile currently hovered by the mouse cursor.
        /// </summary>
        /// <param name="mouseX">The X position of the cursor, in pixels from the left of the game window</param>
        /// <param name="mouseY">The Y position of the cursor, in pixels from the top of the game window</param>
        /// <returns>The coordinates of the tile, or null if the mouse cursor is not above a tile</returns>
        public Position? GetTileFromMousePosition(int mouseX, int mouseY)
        {
            return Renderer.GetTileFromMousePosition(new Position(mouseX, mouseY));
        }

        /// <summary>
        /// Returns the coordinates of the tile currently hovered by the mouse cursor.
        /// </summary>
        /// <param name="mouse">The position of the cursor, in pixels from the top-left corner of the game window</param>
        /// <returns>The coordinates of the tile, or null if the mouse cursor is not above a tile</returns>
        public Position? GetTileFromMousePosition(Position mouse)
        {
            return Renderer.GetTileFromMousePosition(mouse);
        }

        /// <summary>
        /// (Internal) Called on each new frame, draws the frame.
        /// </summary>
        internal void OnRenderFrame()
        {
            Renderer.SetupFrame();

            GL.Disable(EnableCap.Blend);
            UI.OnRenderFrame();
            Sprites.OnRenderFrame();
            GL.Enable(EnableCap.Blend);
            UI.Cursor.OnRenderFrame();
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose()
        {
            Sprites.Destroy();
            UI.Destroy();
            Audio.Destroy();
            Renderer.Dispose();
            OpenTKWindow.Dispose();

            OnDispose();
        }

        /// <summary>
        /// Closes the game window.
        /// </summary>
        public void Close()
        {
            OpenTKWindow.Close();
        }
    }
}

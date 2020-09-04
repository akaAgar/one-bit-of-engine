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

using OneBitOfEngine.Core;
using OneBitOfEngine.Input;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;

namespace OneBitOfEngine.OpenGL
{
    /// <summary>
    /// Internal implementation of OpenTK's GameWindow.
    /// Only used internally, all required public/overrideable methods are in ObeEGame classes.
    /// </summary>
    internal class OpenTKWindow : GameWindow
    {
        /// <summary>
        /// Instance of OBoEOpenTKWindow which created this game window.
        /// </summary>
        private readonly OneBitOfGame Game;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">Instance of OBoEOpenTKWindow which created this game window.</param>
        internal OpenTKWindow(OneBitOfGame game)
        {
            Game = game;
        }

        /// <summary>
        /// Called after the OpenGL context has been established, but before entering the main loop.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnLoad(EventArgs e)
        {
            Game.OnLoadInternal();
            base.OnLoad(e);
        }

        /// <summary>
        /// Called when the frame is updated.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Game.OnUpdateInternal((float)e.Time);
            base.OnUpdateFrame(e);
        }

        /// <summary>
        /// Called when the frame is rendered.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Game.OnRenderFrame();
            SwapBuffers();
        }

        /// <summary>
        /// Called when this window is resized.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnResize(EventArgs e)
        {
            Game.OnResizeInternal();
            base.OnResize(e);
        }

        /// <summary>
        /// OnKeyDown override. Basically just calls Game.OnKeyDown.
        /// </summary>
        /// <param name="e">OpenTK keyboard event.</param>
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            Game.OnInputEventInternal((KeyCode)e.Key, (ModifierKeys)e.Modifiers, -1, e.IsRepeat);
        }
    }
}

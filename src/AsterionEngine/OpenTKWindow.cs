/*
==========================================================================
This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
Asterion Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
Asterion Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with Asterion Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using Asterion.Core;
using Asterion.Input;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;

namespace Asterion
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
        private readonly AsterionGame Game;

        /// <summary>
        /// Last tile hovered by the mouse, or null if none.
        /// </summary>
        private Position? LastHoveredTile = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">Instance of OBoEOpenTKWindow which created this game window.</param>
        internal OpenTKWindow(AsterionGame game)
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
        /// OnMouseDown override. If the mouse cursor is enabled, calls Game.OnMouseDown.
        /// </summary>
        /// <param name="e">OpenTK mouse event.</param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (!Game.MouseCursorVisible) return;
            Position? tile = Game.GetTileFromMousePosition(e.X, e.Y);
            Game.Input.OnMouseDownInternal((Input.MouseButton)e.Button, tile);
        }

        /// <summary>
        /// OnMouseUp override. If the mouse cursor is enabled, calls Game.OnMouseUp.
        /// </summary>
        /// <param name="e">OpenTK mouse event.</param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (!Game.MouseCursorVisible) return;
            Position? tile = Game.GetTileFromMousePosition(e.X, e.Y);
            Game.Input.OnMouseUpInternal((Input.MouseButton)e.Button, tile);
        }

        /// <summary>
        /// OnMouseWheel override. Basically just calls Game.OnMouseWheel.
        /// </summary>
        /// <param name="e">OpenTK mouse event.</param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            Game.Input.OnMouseWheelInternal(e.DeltaPrecise);
        }

        /// <summary>
        /// OnMouseMove override. If the mouse cursor is enabled, calls Game.OnMouseMove.
        /// </summary>
        /// <param name="e">OpenTK mouse events.</param>
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (!Game.MouseCursorVisible) return;

            Position? tile = Game.GetTileFromMousePosition(e.X, e.Y);
            if (LastHoveredTile == tile) return;
            LastHoveredTile = tile;
            Game.Input.OnMouseMoveInternal(tile);
        }

        /// <summary>
        /// OnKeyDown override. Basically just calls Game.OnKeyDown.
        /// </summary>
        /// <param name="e">OpenTK keyboard event.</param>
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            Game.Input.OnKeyDownInternal((KeyCode)e.Key, e.Shift, e.Control, e.Alt, e.IsRepeat);
        }

        /// <summary>
        /// OnKeyUp override. Basically just calls Game.OnKeyUp.
        /// </summary>
        /// <param name="e">OpenTK keyboard event.</param>
        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            Game.Input.OnKeyUpInternal((KeyCode)e.Key, e.Shift, e.Control, e.Alt, e.IsRepeat);
        }
    }
}

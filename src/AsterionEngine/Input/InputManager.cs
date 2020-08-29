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
using OpenTK.Input;
using System.Resources;
using System.Security.Cryptography;
using OTKMouseButton = OpenTK.Input.MouseButton;

namespace Asterion.Input
{
    /// <summary>
    /// Input manager. Provides methods to manage all keyboard, mouse and gamepad events.
    /// </summary>
    public sealed class InputManager
    {
        /// <summary>
        /// Maximum number of gamepads.
        /// </summary>
        public const int MAX_GAMEPADS = 8;

        /// <summary>
        /// Maximum number of gamepads.
        /// </summary>
        public static readonly int MAX_GAMEPAD_BUTTONS = AsterionTools.EnumCount<GamePadButton>();

        /// <summary>
        /// (Private) Last gamepad states. Compared with the current gamepad states on each update to look for changes and raise events.
        /// </summary>
        private readonly GamePadState[] LastGamepadState = new GamePadState[MAX_GAMEPADS];

        /// <summary>
        /// Should gamepads be enabled. If false, no gamepad events will be raised and all gamepad-related methods will return default values.
        /// </summary>
        public bool EnableGamePads { get; set; } = false;

        /// <summary>
        /// Mouse movement event.
        /// </summary>
        /// <param name="tile">The tile the mouse cursor is currently hovering, or null if none</param>
        public delegate void MouseEvent(Position? tile);

        /// <summary>
        /// Mouse button event.
        /// </summary>
        /// <param name="button">The button that raised the event</param>
        /// <param name="tile">The tile the mouse cursor is currently hovering, or null if none</param>
        public delegate void MouseButtonEvent(MouseButton button, Position? tile);

        /// <summary>
        /// Mouse wheel event.
        /// </summary>
        /// <param name="wheelDelta">Variation in the mouse wheel value since the last mouse wheel event</param>
        public delegate void MouseWheelEvent(float wheelDelta);

        /// <summary>
        /// Keyboard event.
        /// </summary>
        /// <param name="key">The key that raised the event</param>
        /// <param name="shift">Was the shift modifier key down?</param>
        /// <param name="control">Was the control modifier key down?</param>
        /// <param name="alt">Was the alt modifier key down?</param>
        /// <param name="isRepeat">Is this a "repeated key press" event, automatically generated while the used holds the key down?</param>
        public delegate void KeyboardEvent(KeyCode key, bool shift, bool control, bool alt, bool isRepeat);

        /// <summary>
        /// Gamepad button event.
        /// </summary>
        /// <param name="gamePadIndex">Index of the gamepad that raised the event</param>
        /// <param name="button">Button that raised the event</param>
        public delegate void GamepadButtonEvent(int gamePadIndex, GamePadButton button);

        /// <summary>
        /// Event raised when the mouse is moved from one tile to another.
        /// </summary>
        public event MouseEvent OnMouseMove = null;

        /// <summary>
        /// Event raised when a mouse button is pressed.
        /// </summary>
        public event MouseButtonEvent OnMouseDown = null;

        /// <summary>
        /// Event raised when a mouse button is released.
        /// </summary>
        public event MouseButtonEvent OnMouseUp = null;

        /// <summary>
        /// Event raised when the mouse wheel is scrolled.
        /// </summary>
        public event MouseWheelEvent OnMouseWheel = null;

        /// <summary>
        /// Event raised when a keyboard key is pressed.
        /// </summary>
        public event KeyboardEvent OnKeyDown = null;

        /// <summary>
        /// Event raised when a keyboard key is released.
        /// </summary>
        public event KeyboardEvent OnKeyUp = null;

        /// <summary>
        /// Event raised when a gamepad button is pressed.
        /// </summary>
        public event GamepadButtonEvent OnGamePadButtonDown = null;

        /// <summary>
        /// Event raised when a gamepad button is released.
        /// </summary>
        public event GamepadButtonEvent OnGamePadButtonUp = null;

        /// <summary>
        /// (Internal) Raises a OnKeyDown event.
        /// </summary>
        /// <param name="key">The key that raised the event</param>
        /// <param name="shift">Was the shift modifier key down?</param>
        /// <param name="control">Was the control modifier key down?</param>
        /// <param name="alt">Was the alt modifier key down?</param>
        /// <param name="isRepeat">Is this a "repeated key press" event, automatically generated while the used holds the key down?</param>
        internal void OnKeyDownInternal(KeyCode key, bool shift, bool control, bool alt, bool isRepeat) { OnKeyDown?.Invoke(key, shift, control, alt, isRepeat); }

        /// <summary>
        /// (Internal) Raises a OnKeyUp event.
        /// </summary>
        /// <param name="key">The key that raised the event</param>
        /// <param name="shift">Was the shift modifier key down?</param>
        /// <param name="control">Was the control modifier key down?</param>
        /// <param name="alt">Was the alt modifier key down?</param>
        /// <param name="isRepeat">Is this a "repeated key press" event, automatically generated while the used holds the key down?</param>
        internal void OnKeyUpInternal(KeyCode key, bool shift, bool control, bool alt, bool isRepeat) { OnKeyUp?.Invoke(key, shift, control, alt, isRepeat); }

        /// <summary>
        /// (Internal) Raises a OnMouseMove event.
        /// </summary>
        /// <param name="tile">The tile the mouse cursor is currently hovering, or null if none</param>
        internal void OnMouseMoveInternal(Position? tile) { OnMouseMove?.Invoke(tile); }

        /// <summary>
        /// (Internal) Raises a OnMouseDown event.
        /// </summary>
        /// <param name="button">The button that raised the event</param>
        /// <param name="tile">The tile the mouse cursor is currently hovering, or null if none</param>
        internal void OnMouseDownInternal(MouseButton button, Position? tile) { OnMouseDown?.Invoke(button, tile); }

        /// <summary>
        /// (Internal) Raises a OnMouseUp event.
        /// </summary>
        /// <param name="button">The button that raised the event</param>
        /// <param name="tile">The tile the mouse cursor is currently hovering, or null if none</param>
        internal void OnMouseUpInternal(MouseButton button, Position? tile) { OnMouseUp?.Invoke(button, tile); }

        /// <summary>
        /// (Internal) Raises a OnMouseWheel event.
        /// </summary>
        /// <param name="wheelDelta">Variation in the mouse wheel value since the last mouse wheel event</param>
        internal void OnMouseWheelInternal(float wheelDelta) { OnMouseWheel?.Invoke(wheelDelta); }

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        internal InputManager()
        {
            for (int i = 0; i < MAX_GAMEPADS; i++)
                LastGamepadState[i] = new GamePadState();
        }

        /// <summary>
        /// (Internal) Update loop, called on every update.
        /// Only used to check the gamepad state changes since the last frame.
        /// </summary>
        internal void OnUpdate()
        {
            if (EnableGamePads)
            {
                for (int i = 0; i < MAX_GAMEPADS; i++)
                {
                    GamePadState state = GamePad.GetState(i);
                    if (!state.IsConnected) continue;
                    if (state.PacketNumber == LastGamepadState[i].PacketNumber) continue;

                    if (state.Buttons.A != LastGamepadState[i].Buttons.A) CheckGamePadButtonEvent(GamePadButton.A, state.Buttons.A, i);
                    if (state.Buttons.B != LastGamepadState[i].Buttons.B) CheckGamePadButtonEvent(GamePadButton.B, state.Buttons.B, i);
                    if (state.Buttons.Back != LastGamepadState[i].Buttons.Back) CheckGamePadButtonEvent(GamePadButton.Back, state.Buttons.Back, i);
                    if (state.Buttons.BigButton != LastGamepadState[i].Buttons.BigButton) CheckGamePadButtonEvent(GamePadButton.Home, state.Buttons.BigButton, i);
                    if (state.Buttons.LeftShoulder != LastGamepadState[i].Buttons.LeftShoulder) CheckGamePadButtonEvent(GamePadButton.LeftShoulder, state.Buttons.LeftShoulder, i);
                    if (state.Buttons.LeftStick != LastGamepadState[i].Buttons.LeftStick) CheckGamePadButtonEvent(GamePadButton.LeftStick, state.Buttons.LeftStick, i);
                    if (state.Buttons.RightShoulder != LastGamepadState[i].Buttons.RightShoulder) CheckGamePadButtonEvent(GamePadButton.RightShoulder, state.Buttons.RightShoulder, i);
                    if (state.Buttons.RightStick != LastGamepadState[i].Buttons.RightStick) CheckGamePadButtonEvent(GamePadButton.RightStick, state.Buttons.RightStick, i);
                    if (state.Buttons.Start != LastGamepadState[i].Buttons.Start) CheckGamePadButtonEvent(GamePadButton.Start, state.Buttons.Start, i);
                    if (state.Buttons.X != LastGamepadState[i].Buttons.X) CheckGamePadButtonEvent(GamePadButton.X, state.Buttons.X, i);
                    if (state.Buttons.Y != LastGamepadState[i].Buttons.Y) CheckGamePadButtonEvent(GamePadButton.Y, state.Buttons.Y, i);

                    if (state.DPad.Down != LastGamepadState[i].DPad.Down) CheckGamePadButtonEvent(GamePadButton.DPadDown, state.DPad.Down, i);
                    if (state.DPad.Left != LastGamepadState[i].DPad.Left) CheckGamePadButtonEvent(GamePadButton.DPadDown, state.DPad.Left, i);
                    if (state.DPad.Right != LastGamepadState[i].DPad.Right) CheckGamePadButtonEvent(GamePadButton.DPadDown, state.DPad.Right, i);
                    if (state.DPad.Up != LastGamepadState[i].DPad.Up) CheckGamePadButtonEvent(GamePadButton.DPadDown, state.DPad.Up, i);

                    if (state.ThumbSticks.Left.X != LastGamepadState[i].ThumbSticks.Left.X)

                    LastGamepadState[i] = state;
                }
            }
        }

        /// <summary>
        /// Checks the current status of a gamepad button.
        /// </summary>
        /// <param name="gamePadIndex">Index of the gamepad to check, between 0 and <see cref="MAX_GAMEPADS"/></param>
        /// <param name="button">The button</param>
        /// <returns>True if the button is pressed, false if it is released</returns>
        public bool IsGamePadButtonDown(int gamePadIndex, GamePadButton button)
        {
            if (!EnableGamePads) return false;
            if (!IsValidGamePadIndex(gamePadIndex)) return false;

            GamePadState state = GamePad.GetState(gamePadIndex);
            if (!state.IsConnected) return false;
            
            switch (button)
            {
                case GamePadButton.A: return state.Buttons.A == ButtonState.Pressed;
                case GamePadButton.B: return state.Buttons.B == ButtonState.Pressed;
                case GamePadButton.Back: return state.Buttons.Back == ButtonState.Pressed;
                case GamePadButton.DPadDown: return state.DPad.Down == ButtonState.Pressed;
                case GamePadButton.DPadLeft: return state.DPad.Left == ButtonState.Pressed;
                case GamePadButton.DPadRight: return state.DPad.Right == ButtonState.Pressed;
                case GamePadButton.DPadUp: return state.DPad.Up == ButtonState.Pressed;
                case GamePadButton.Home: return state.Buttons.BigButton == ButtonState.Pressed;
                case GamePadButton.LeftShoulder: return state.Buttons.LeftShoulder == ButtonState.Pressed;
                case GamePadButton.LeftStick: return state.Buttons.LeftStick == ButtonState.Pressed;
                case GamePadButton.RightShoulder: return state.Buttons.RightShoulder == ButtonState.Pressed;
                case GamePadButton.RightStick: return state.Buttons.RightStick == ButtonState.Pressed;
                case GamePadButton.Start: return state.Buttons.Start == ButtonState.Pressed;
                case GamePadButton.X: return state.Buttons.X == ButtonState.Pressed;
                case GamePadButton.Y: return state.Buttons.Y == ButtonState.Pressed;
            }

            return false;
        }

        /// <summary>
        /// Checks the current status of a gamepad button on ANY of the current gamepads.
        /// </summary>
        /// <param name="button">The button</param>
        /// <returns>True if the button is pressed on ANY gamepad, false otherwise</returns>
        public bool IsGamePadButtonDownOnAnyGamePad(GamePadButton button)
        {
            for (int i = 0; i < MAX_GAMEPADS; i++)
                if (IsGamePadButtonDown(i, button)) return true;

            return false;
        }

        /// <summary>
        /// Gets the current value of an axis of a gamepad thumbstick.
        /// </summary>
        /// <param name="gamePadIndex">Index of the gamepad to check, between 0 and <see cref="MAX_GAMEPADS"/></param>
        /// <param name="side">The stick side (left or right)</param>
        /// <param name="axis">The stick axis (X or Y)</param>
        /// <returns></returns>
        public float GetGamePadThumbStick(int gamePadIndex, GamePadSide side, GamePadAxis axis)
        {
            if (!EnableGamePads) return 0;
            if (!IsValidGamePadIndex(gamePadIndex)) return 0;

            GamePadState state = GamePad.GetState(gamePadIndex);
            if (!state.IsConnected) return 0;

            if (side == GamePadSide.Left)
            {
                if (axis == GamePadAxis.X)
                    return state.ThumbSticks.Left.X;
                else
                    return state.ThumbSticks.Left.Y;
            }
            else
            {
                if (axis == GamePadAxis.X)
                    return state.ThumbSticks.Right.X;
                else
                    return state.ThumbSticks.Right.Y;
            }
        }

        /// <summary>
        /// Gets the current value of a gamepad trigger.
        /// </summary>
        /// <param name="gamePadIndex">Index of the gamepad to check, between 0 and <see cref="MAX_GAMEPADS"/></param>
        /// <param name="side">The trigger side (left or right)</param>
        /// <returns>A floating point value between 0.0 and 1.0.</returns>
        public float GetGamePadTrigger(int gamePadIndex, GamePadSide side)
        {
            if (!EnableGamePads) return 0;
            if (!IsValidGamePadIndex(gamePadIndex)) return 0;

            GamePadState state = GamePad.GetState(gamePadIndex);
            if (!state.IsConnected) return 0;

            if (side == GamePadSide.Left)
                return state.Triggers.Left;
            else
                return state.Triggers.Right;
        }

        /// <summary>
        /// Is a gamepad with this index currently connected?
        /// </summary>
        /// <param name="gamePadIndex">Index of the gamepad to check, between 0 and <see cref="MAX_GAMEPADS"/></param>
        /// <returns>True if a gamepad is connected, false otherwise</returns>
        public bool IsGamePadConnected(int gamePadIndex)
        {
            if (!EnableGamePads) return false;
            if (!IsValidGamePadIndex(gamePadIndex)) return false;
            GamePadState state = GamePad.GetState(gamePadIndex);
            return state.IsConnected;
        }

        /// <summary>
        /// Gets the name of the gamepad with the provided index.
        /// </summary>
        /// <param name="gamePadIndex">Index of the gamepad to check, between 0 and <see cref="MAX_GAMEPADS"/></param>
        /// <returns>The name of the gamepad, or null if no gamepad with this index exists</returns>
        public string GetGamePadName(int gamePadIndex)
        {
            if (!EnableGamePads) return null;
            if (!IsGamePadConnected(gamePadIndex)) return null;
            return GamePad.GetName(gamePadIndex);
        }

        /// <summary>
        /// Is a key currently down?
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key is currently pressed, false if it is released</returns>
        public bool IsKeyDown(KeyCode key)
        {
            return Keyboard.GetState().IsKeyDown((Key)key);
        }

        /// <summary>
        /// Is any key currently down on the keyboard?
        /// </summary>
        /// <returns>True if any key is down, false otherwise</returns>
        public bool IsAnyKeyDown()
        {
            return Keyboard.GetState().IsAnyKeyDown;
        }

        /// <summary>
        /// Is a mouse button currently pressed?
        /// </summary>
        /// <param name="button">The mouse button</param>
        /// <returns>True if the button is pressed, false if it is released</returns>
        public bool IsMouseButtonDown(MouseButton button)
        {
            return Mouse.GetState().IsButtonDown((OTKMouseButton)button);
        }

        /// <summary>
        /// (Private) Raises an event according to the state of a given gamepad button.
        /// </summary>
        /// <param name="button">The button</param>
        /// <param name="buttonState">Current state of the button</param>
        /// <param name="gamePadIndex">Index of the gamepad to check, between 0 and <see cref="MAX_GAMEPADS"/></param>
        private void CheckGamePadButtonEvent(GamePadButton button, ButtonState buttonState, int gamePadIndex)
        {
            if (buttonState == ButtonState.Released)
                OnGamePadButtonUp?.Invoke(gamePadIndex, button);
            else
                OnGamePadButtonDown?.Invoke(gamePadIndex, button);
        }

        /// <summary>
        /// (Private) Checks if a gamepad index is between 0 and <see cref="MAX_GAMEPADS"/>.
        /// </summary>
        /// <param name="gamePadIndex">Gamepad index to check</param>
        /// <returns>True if the index is valid, false otherwise</returns>
        private bool IsValidGamePadIndex(int gamePadIndex)
        {
            return (gamePadIndex >= 0) && (gamePadIndex < MAX_GAMEPADS);
        }
    }
}

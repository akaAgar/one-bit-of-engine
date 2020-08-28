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

using OpenTK.Input;

using OTKMouseButton = OpenTK.Input.MouseButton;

namespace Asterion.Input
{
    /// <summary>
    /// Input managers. Provides events to manage all keyboard, mouse and gamepad actions.
    /// </summary>
    public sealed class InputManager
    {
        public const int MAX_GAMEPADS = 8;

        public bool EnableGamePads { get; set; } = false;

        public delegate void MouseEvent(Position tile);
        public delegate void MouseButtonEvent(MouseButton button, Position tile);
        public delegate void MouseWheelEvent(float wheelDelta);
        public delegate void KeyboardEvent(KeyCode key, bool shift, bool control, bool alt, bool isRepeat);
        public delegate void GamepadButtonEvent(int gamePadIndex, GamePadButton button);

        public event MouseEvent OnMouseMove = null;
        public event MouseButtonEvent OnMouseDown = null;
        public event MouseButtonEvent OnMouseUp = null;
        public event MouseWheelEvent OnMouseWheel = null;

        public event KeyboardEvent OnKeyDown = null;
        public event KeyboardEvent OnKeyUp = null;

        public event GamepadButtonEvent OnGamePadButtonDown = null;
        public event GamepadButtonEvent OnGamePadButtonUp = null;

        internal void OnKeyDownInternal(KeyCode key, bool shift, bool control, bool alt, bool isRepeat) { OnKeyDown?.Invoke(key, shift, control, alt, isRepeat); }
        internal void OnKeyUpInternal(KeyCode key, bool shift, bool control, bool alt, bool isRepeat) { OnKeyUp?.Invoke(key, shift, control, alt, isRepeat); }

        internal void OnMouseMoveInternal(Position tile) { OnMouseMove?.Invoke(tile); }
        internal void OnMouseDownInternal(MouseButton button, Position tile) { OnMouseDown?.Invoke(button, tile); }
        internal void OnMouseUpInternal(MouseButton button, Position tile) { OnMouseUp?.Invoke(button, tile); }
        internal void OnMouseWheelInternal(float wheelDelta) { OnMouseWheel?.Invoke(wheelDelta); }

        private readonly GamePadState[] LastGamepadState = new GamePadState[MAX_GAMEPADS];

        internal InputManager()
        {
            for (int i = 0; i < MAX_GAMEPADS; i++)
                LastGamepadState[i] = new GamePadState();
        }

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

        public bool IsGamePadButtonDown(int gamePadIndex, GamePadButton button)
        {
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

        public float GetGamePadThumbStick(int gamePadIndex, GamePadSide stick, GamePadAxis axis)
        {
            if (!IsValidGamePadIndex(gamePadIndex)) return 0;

            GamePadState state = GamePad.GetState(gamePadIndex);
            if (!state.IsConnected) return 0;

            if (stick == GamePadSide.Left)
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

        public float GetGamePadTrigger(int gamePadIndex, GamePadSide side)
        {
            if (!IsValidGamePadIndex(gamePadIndex)) return 0;

            GamePadState state = GamePad.GetState(gamePadIndex);
            if (!state.IsConnected) return 0;

            if (side == GamePadSide.Left)
                return state.Triggers.Left;
            else
                return state.Triggers.Right;
        }

        public bool IsGamePadConnected(int gamePadIndex)
        {
            if (!IsValidGamePadIndex(gamePadIndex)) return false;
            GamePadState state = GamePad.GetState(gamePadIndex);
            return state.IsConnected;
        }

        public string GetGamePadName(int gamePadIndex)
        {
            if (!IsGamePadConnected(gamePadIndex)) return "";
            return GamePad.GetName(gamePadIndex);
        }

        private bool IsValidGamePadIndex(int gamePadIndex)
        {
            return (gamePadIndex >= 0) && (gamePadIndex < MAX_GAMEPADS);
        }

        public bool IsKeyDown(KeyCode key)
        {
            return Keyboard.GetState().IsKeyDown((Key)key);
        }

        public bool IsAnyKeyDown()
        {
            return Keyboard.GetState().IsAnyKeyDown;
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            return Mouse.GetState().IsButtonDown((OTKMouseButton)button);
        }

        private void CheckGamePadButtonEvent(GamePadButton button, ButtonState buttonState, int gamePadIndex)
        {
            if (buttonState == ButtonState.Released)
                OnGamePadButtonUp?.Invoke(gamePadIndex, button);
            else
                OnGamePadButtonDown?.Invoke(gamePadIndex, button);
        }

        internal void Dispose() { }
    }
}

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

using OpenTK.Input;

namespace Asterion.Input
{
    /// <summary>
    /// Input manager. Provides methods to manage all keyboard, mouse and gamepad events.
    /// </summary>
    public sealed class InputManager
    {
        /// <summary>
        /// (Private) Delay (in seconds) before repeat button presses event begin to be raised when a gamepad button is kept down.
        /// </summary>
        private const float GAMEPAD_REPEAT_DELAY = 1.0f;

        /// <summary>
        /// (Private) Delay (in seconds) between repeat button events.
        /// </summary>
        private const float GAMEPAD_REPEAT_RATE = 0.1f;

        /// <summary>
        /// (Private) First gamepad button in the KeyCode enum.
        /// </summary>
        private const KeyCode FIRST_GAMEPAD_BUTTON = KeyCode.GamepadA;

        /// <summary>
        /// (Private) Last gamepad button in the KeyCode enum.
        /// </summary>
        private const KeyCode LAST_GAMEPAD_BUTTON = KeyCode.GamepadY;

        /// <summary>
        /// (Private) Total number of gamepad buttons.
        /// </summary>
        private const int TOTAL_GAMEPAD_BUTTONS = (int)LAST_GAMEPAD_BUTTON - (int)FIRST_GAMEPAD_BUTTON + 1;

        /// <summary>
        /// Maximum number of gamepads.
        /// </summary>
        public const int GAMEPADS_COUNT = 4;

        /// <summary>
        /// Should gamepads be enabled. If false, no gamepad events will be raised and all gamepad-related methods will return default values.
        /// </summary>
        public bool EnableGamePads { get; set; } = false;

        /// <summary>
        /// Gamepad trigger activation threshold. If the trigger value (expressed on a 0 to 1 scale) is greater than this, the input manager will consider the trigger is pressed.
        /// Default is 0.5.
        /// </summary>
        public float GamepadTriggerThreshold { get { return GamepadTriggerThreshold_; } set { GamepadTriggerThreshold_ = AsterionTools.Clamp(value, 0.01f, 0.99f); } }
        private float GamepadTriggerThreshold_ = 0.5f;

        /// <summary>
        /// Gamepad stick axis activation threshold. If the absolute stick value (expressed on a -1 to 1 scale, 0 being the center) is greater than this, the input manager will consider the stick is pointing in a direction.
        /// Default is 0.25.
        /// </summary>
        public float GamepadStickAxisThreshold { get { return GamepadStickAxisThreshold_; } set { GamepadStickAxisThreshold_ = AsterionTools.Clamp(value, 0.01f, 0.99f); } }
        private float GamepadStickAxisThreshold_ = 0.25f;

        /// <summary>
        /// (Private) The <see cref="AsterionGame"/> this <see cref="=InputManager"/> belongs to.
        /// </summary>
        private readonly AsterionGame Game;

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        /// <param name="game">The <see cref="AsterionGame"/> this <see cref="=InputManager"/> belongs to</param>
        internal InputManager(AsterionGame game)
        {
            Game = game;
        }

        /// <summary>
        /// Stores the time during which each gamepad button has been held down.
        /// </summary>
        private readonly float[,] GamepadFirstPress = new float[GAMEPADS_COUNT, TOTAL_GAMEPAD_BUTTONS];

        /// <summary>
        /// Timer used to keep track of the repeat key pulses to send.
        /// </summary>
        private float RepeatKeyPressTimer = 0f;

        /// <summary>
        /// (Internal) Update loop, called on every update.
        /// Only used to check the gamepad state changes since the last frame.
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed second since last update</param>
        internal void OnUpdate(float elapsedSeconds)
        {
            if (!EnableGamePads) return;

            int gamepad, button;

            bool repeatFrame = false;
            RepeatKeyPressTimer += elapsedSeconds;
            if (RepeatKeyPressTimer >= GAMEPAD_REPEAT_RATE)
            {
                RepeatKeyPressTimer = 0f;
                repeatFrame = true;
            }

            for (gamepad = 0; gamepad < GAMEPADS_COUNT; gamepad++)
            {
                GamePadState state = GamePad.GetState(gamepad);

                for (button = 0; button < TOTAL_GAMEPAD_BUTTONS; button++)
                {
                    if (GetGamepadButtonStatus(state, FIRST_GAMEPAD_BUTTON + button)) // button is pressed
                    {
                        if (GamepadFirstPress[gamepad, button] == 0) // button has JUST been pressed, send an event
                            Game.OnInputEventInternal(FIRST_GAMEPAD_BUTTON + button, 0, gamepad, false);

                        GamepadFirstPress[gamepad, button] += elapsedSeconds;

                        if (repeatFrame && (GamepadFirstPress[gamepad, button] >= GAMEPAD_REPEAT_DELAY))
                            Game.OnInputEventInternal(FIRST_GAMEPAD_BUTTON + button, 0, gamepad, true);
                    }
                    else // button is released
                    {
                        GamepadFirstPress[gamepad, button] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the state (pressed or released) of a given gamepad button.
        /// </summary>
        /// <param name="state">OpenInput gamepad state to read</param>
        /// <param name="key">Button to check</param>
        /// <returns>True if the button is pressed, false if it is released</returns>
        private bool GetGamepadButtonStatus(GamePadState state, KeyCode key)
        {
            if (!state.IsConnected) return false;

            switch (key)
            {
                case KeyCode.GamepadA: return state.Buttons.A == ButtonState.Pressed;
                case KeyCode.GamepadB: return state.Buttons.B == ButtonState.Pressed;
                case KeyCode.GamepadBack: return state.Buttons.Back == ButtonState.Pressed;
                case KeyCode.GamepadDPadDown: return state.DPad.Down == ButtonState.Pressed;
                case KeyCode.GamepadDPadLeft: return state.DPad.Left == ButtonState.Pressed;
                case KeyCode.GamepadDPadRight: return state.DPad.Right == ButtonState.Pressed;
                case KeyCode.GamepadDPadUp: return state.DPad.Up == ButtonState.Pressed;
                case KeyCode.GamepadHome: return state.Buttons.BigButton == ButtonState.Pressed;
                case KeyCode.GamepadLeftShoulder: return state.Buttons.LeftShoulder == ButtonState.Pressed;
                case KeyCode.GamepadLeftStickPress: return state.Buttons.LeftStick == ButtonState.Pressed;
                case KeyCode.GamepadLeftStickDown: return state.ThumbSticks.Left.Y > GamepadStickAxisThreshold_;
                case KeyCode.GamepadLeftStickLeft: return state.ThumbSticks.Left.X < -GamepadStickAxisThreshold_;
                case KeyCode.GamepadLeftStickRight: return state.ThumbSticks.Left.X > GamepadStickAxisThreshold_;
                case KeyCode.GamepadLeftStickUp: return state.ThumbSticks.Left.Y < -GamepadStickAxisThreshold_;
                case KeyCode.GamepadLeftTrigger: return state.Triggers.Left > GamepadTriggerThreshold_;
                case KeyCode.GamepadRightShoulder: return state.Buttons.RightShoulder == ButtonState.Pressed;
                case KeyCode.GamepadRightStickPress: return state.Buttons.RightStick == ButtonState.Pressed;
                case KeyCode.GamepadRightStickDown: return state.ThumbSticks.Right.Y > GamepadStickAxisThreshold_;
                case KeyCode.GamepadRightStickLeft: return state.ThumbSticks.Right.X < -GamepadStickAxisThreshold_;
                case KeyCode.GamepadRightStickRight: return state.ThumbSticks.Right.X > GamepadStickAxisThreshold_;
                case KeyCode.GamepadRightStickUp: return state.ThumbSticks.Right.Y < -GamepadStickAxisThreshold_;
                case KeyCode.GamepadRightTrigger: return state.Triggers.Right > GamepadTriggerThreshold_;
                case KeyCode.GamepadStart: return state.Buttons.Start == ButtonState.Pressed;
                case KeyCode.GamepadX: return state.Buttons.X == ButtonState.Pressed;
                case KeyCode.GamepadY: return state.Buttons.Y == ButtonState.Pressed;
            }

            return false;
        }

        /// <summary>
        /// Is a gamepad with this index currently connected?
        /// </summary>
        /// <param name="gamepadIndex">Index of the gamepad to check, between 0 and <see cref="GAMEPADS_COUNT"/></param>
        /// <returns>True if a gamepad is connected, false otherwise</returns>
        public bool IsGamePadConnected(int gamepadIndex)
        {
            if (!EnableGamePads) return false;
            if ((gamepadIndex < 0) || (gamepadIndex >= GAMEPADS_COUNT)) return false;
            return GamePad.GetState(gamepadIndex).IsConnected;
        }

        /// <summary>
        /// Gets the name of the gamepad with the provided index.
        /// </summary>
        /// <param name="gamepadIndex">Index of the gamepad to check, between 0 and <see cref="GAMEPADS_COUNT"/></param>
        /// <returns>The name of the gamepad, or null if no gamepad with this index exists</returns>
        public string GetGamePadName(int gamepadIndex)
        {
            if (!EnableGamePads) return null;
            if ((gamepadIndex < 0) || (gamepadIndex >= GAMEPADS_COUNT)) return null;
            if (!IsGamePadConnected(gamepadIndex)) return null;
            return GamePad.GetName(gamepadIndex);
        }
    }
}

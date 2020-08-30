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

namespace Asterion.Input
{
    /// <summary>
    /// Enumerates all keyboard keys and gamepad buttons.
    /// </summary>
    public enum KeyCode
    {
        /// <summary>
        /// A key outside the known keys.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The left shift key.
        /// </summary>
        ShiftLeft = 1,
        /// <summary>
        /// The left shift key (equivalent to ShiftLeft).
        /// </summary>
        LShift = 1,
        /// <summary>
        /// The right shift key.
        /// </summary>
        ShiftRight = 2,
        /// <summary>
        /// The right shift key (equivalent to ShiftRight).
        /// </summary>
        RShift = 2,
        /// <summary>
        /// The left control key.
        /// </summary>
        ControlLeft = 3,
        /// <summary>
        /// The left control key (equivalent to ControlLeft).
        /// </summary>
        LControl = 3,
        /// <summary>
        /// The right control key.
        /// </summary>
        ControlRight = 4,
        /// <summary>
        /// The right control key (equivalent to ControlRight).
        /// </summary>
        RControl = 4,
        /// <summary>
        /// The left alt key.
        /// </summary>
        AltLeft = 5,
        /// <summary>
        /// The left alt key (equivalent to AltLeft.
        /// </summary>
        LAlt = 5,
        /// <summary>
        /// The right alt key.
        /// </summary>
        AltRight = 6,
        /// <summary>
        /// The right alt key (equivalent to AltRight).
        /// </summary>
        RAlt = 6,
        /// <summary>
        /// The left win key.
        /// </summary>
        WinLeft = 7,
        /// <summary>
        /// The left win key (equivalent to WinLeft).
        /// </summary>
        LWin = 7,
        /// <summary>
        /// The right win key.
        /// </summary>
        WinRight = 8,
        /// <summary>
        /// The right win key (equivalent to WinRight).
        /// </summary>
        RWin = 8,
        /// <summary>
        /// The menu key.
        /// </summary>
        Menu = 9,
        /// <summary>
        /// The F1 key.
        /// </summary>
        F1 = 10,
        /// <summary>
        /// The F2 key.
        /// </summary>
        F2 = 11,
        /// <summary>
        /// The F3 key.
        /// </summary>
        F3 = 12,
        /// <summary>
        /// The F4 key.
        /// </summary>
        F4 = 13,
        /// <summary>
        /// The F5 key.
        /// </summary>
        F5 = 14,
        /// <summary>
        /// The F6 key.
        /// </summary>
        F6 = 15,
        /// <summary>
        /// The F7 key.
        /// </summary>
        F7 = 16,
        /// <summary>
        /// The F8 key.
        /// </summary>
        F8 = 17,
        /// <summary>
        /// The F9 key.
        /// </summary>
        F9 = 18,
        /// <summary>
        /// The F10 key.
        /// </summary>
        F10 = 19,
        /// <summary>
        /// The F11 key.
        /// </summary>
        F11 = 20,
        /// <summary>
        /// The F12 key.
        /// </summary>
        F12 = 21,
        /// <summary>
        /// The F13 key.
        /// </summary>
        F13 = 22,
        /// <summary>
        /// The F14 key.
        /// </summary>
        F14 = 23,
        /// <summary>
        /// The F15 key.
        /// </summary>
        F15 = 24,
        /// <summary>
        /// The F16 key.
        /// </summary>
        F16 = 25,
        /// <summary>
        /// The F17 key.
        /// </summary>
        F17 = 26,
        /// <summary>
        /// The F18 key.
        /// </summary>
        F18 = 27,
        /// <summary>
        /// The F19 key.
        /// </summary>
        F19 = 28,
        /// <summary>
        /// The F20 key.
        /// </summary>
        F20 = 29,
        /// <summary>
        /// The F21 key.
        /// </summary>
        F21 = 30,
        /// <summary>
        /// The F22 key.
        /// </summary>
        F22 = 31,
        /// <summary>
        /// The F23 key.
        /// </summary>
        F23 = 32,
        /// <summary>
        /// The F24 key.
        /// </summary>
        F24 = 33,
        /// <summary>
        /// The F25 key.
        /// </summary>
        F25 = 34,
        /// <summary>
        /// The F26 key.
        /// </summary>
        F26 = 35,
        /// <summary>
        /// The F27 key.
        /// </summary>
        F27 = 36,
        /// <summary>
        /// The F28 key.
        /// </summary>
        F28 = 37,
        /// <summary>
        /// The F29 key.
        /// </summary>
        F29 = 38,
        /// <summary>
        /// The F30 key.
        /// </summary>
        F30 = 39,
        /// <summary>
        /// The F31 key.
        /// </summary>
        F31 = 40,
        /// <summary>
        /// The F32 key.
        /// </summary>
        F32 = 41,
        /// <summary>
        /// The F33 key.
        /// </summary>
        F33 = 42,
        /// <summary>
        /// The F34 key.
        /// </summary>
        F34 = 43,
        /// <summary>
        /// The F35 key.
        /// </summary>
        F35 = 44,
        /// <summary>
        /// The up arrow key.
        /// </summary>
        Up = 45,
        /// <summary>
        /// The down arrow key.
        /// </summary>
        Down = 46,
        /// <summary>
        /// The left arrow key.
        /// </summary>
        Left = 47,
        /// <summary>
        /// The right arrow key.
        /// </summary>
        Right = 48,
        /// <summary>
        /// The enter key.
        /// </summary>
        Enter = 49,
        /// <summary>
        /// The escape key.
        /// </summary>
        Escape = 50,
        /// <summary>
        /// The space key.
        /// </summary>
        Space = 51,
        /// <summary>
        /// The tab key.
        /// </summary>
        Tab = 52,
        /// <summary>
        /// The backspace key.
        /// </summary>
        BackSpace = 53,
        /// <summary>
        /// The backspace key (equivalent to BackSpace).
        /// </summary>
        Back = 53,
        /// <summary>
        /// The insert key.
        /// </summary>
        Insert = 54,
        /// <summary>
        /// The delete key.
        /// </summary>
        Delete = 55,
        /// <summary>
        /// The page up key.
        /// </summary>
        PageUp = 56,
        /// <summary>
        /// The page down key.
        /// </summary>
        PageDown = 57,
        /// <summary>
        /// The home key.
        /// </summary>
        Home = 58,
        /// <summary>
        /// The end key.
        /// </summary>
        End = 59,
        /// <summary>
        /// The caps lock key.
        /// </summary>
        CapsLock = 60,
        /// <summary>
        /// The scroll lock key.
        /// </summary>
        ScrollLock = 61,
        /// <summary>
        /// The print screen key.
        /// </summary>
        PrintScreen = 62,
        /// <summary>
        /// The pause key.
        /// </summary>
        Pause = 63,
        /// <summary>
        /// The num lock key.
        /// </summary>
        NumLock = 64,
        /// <summary>
        /// The clear key (Keypad5 with NumLock disabled, on typical keyboards).
        /// </summary>
        Clear = 65,
        /// <summary>
        /// The sleep key.
        /// </summary>
        Sleep = 66,
        /// <summary>
        /// The keypad 0 key.
        /// </summary>
        Keypad0 = 67,
        /// <summary>
        /// The keypad 1 key.
        /// </summary>
        Keypad1 = 68,
        /// <summary>
        /// The keypad 2 key.
        /// </summary>
        Keypad2 = 69,
        /// <summary>
        /// The keypad 3 key.
        /// </summary>
        Keypad3 = 70,
        /// <summary>
        /// The keypad 4 key.
        /// </summary>
        Keypad4 = 71,
        /// <summary>
        /// The keypad 5 key.
        /// </summary>
        Keypad5 = 72,
        /// <summary>
        /// The keypad 6 key.
        /// </summary>
        Keypad6 = 73,
        /// <summary>
        /// The keypad 7 key.
        /// </summary>
        Keypad7 = 74,
        /// <summary>
        /// The keypad 8 key.
        /// </summary>
        Keypad8 = 75,
        /// <summary>
        /// The keypad 9 key.
        /// </summary>
        Keypad9 = 76,
        /// <summary>
        /// The keypad divide key.
        /// </summary>
        KeypadDivide = 77,
        /// <summary>
        /// The keypad multiply key.
        /// </summary>
        KeypadMultiply = 78,
        /// <summary>
        /// The keypad subtract key.
        /// </summary>
        KeypadSubtract = 79,
        /// <summary>
        /// The keypad minus key (equivalent to KeypadSubtract).
        /// </summary>
        KeypadMinus = 79,
        /// <summary>
        /// The keypad add key.
        /// </summary>
        KeypadAdd = 80,
        /// <summary>
        /// The keypad plus key (equivalent to KeypadAdd).
        /// </summary>
        KeypadPlus = 80,
        /// <summary>
        /// The keypad decimal key.
        /// </summary>
        KeypadDecimal = 81,
        /// <summary>
        /// The keypad period key (equivalent to KeypadDecimal).
        /// </summary>
        KeypadPeriod = 81,
        /// <summary>
        /// The keypad enter key.
        /// </summary>
        KeypadEnter = 82,
        /// <summary>
        /// The A key.
        /// </summary>
        A = 83,
        /// <summary>
        /// The B key.
        /// </summary>
        B = 84,
        /// <summary>
        /// The C key.
        /// </summary>
        C = 85,
        /// <summary>
        /// The D key.
        /// </summary>
        D = 86,
        /// <summary>
        /// The E key.
        /// </summary>
        E = 87,
        /// <summary>
        /// The F key.
        /// </summary>
        F = 88,
        /// <summary>
        /// The G key.
        /// </summary>
        G = 89,
        /// <summary>
        /// The H key.
        /// </summary>
        H = 90,
        /// <summary>
        /// The I key.
        /// </summary>
        I = 91,
        /// <summary>
        /// The J key.
        /// </summary>
        J = 92,
        /// <summary>
        /// The K key.
        /// </summary>
        K = 93,
        /// <summary>
        /// The L key.
        /// </summary>
        L = 94,
        /// <summary>
        /// The M key.
        /// </summary>
        M = 95,
        /// <summary>
        /// The N key.
        /// </summary>
        N = 96,
        /// <summary>
        /// The O key.
        /// </summary>
        O = 97,
        /// <summary>
        /// The P key.
        /// </summary>
        P = 98,
        /// <summary>
        /// The Q key.
        /// </summary>
        Q = 99,
        /// <summary>
        /// The R key.
        /// </summary>
        R = 100,
        /// <summary>
        /// The S key.
        /// </summary>
        S = 101,
        /// <summary>
        /// The T key.
        /// </summary>
        T = 102,
        /// <summary>
        /// The U key.
        /// </summary>
        U = 103,
        /// <summary>
        /// The V key.
        /// </summary>
        V = 104,
        /// <summary>
        /// The W key.
        /// </summary>
        W = 105,
        /// <summary>
        /// The X key.
        /// </summary>
        X = 106,
        /// <summary>
        /// The Y key.
        /// </summary>
        Y = 107,
        /// <summary>
        /// The Z key.
        /// </summary>
        Z = 108,
        /// <summary>
        /// The number 0 key.
        /// </summary>
        Number0 = 109,
        /// <summary>
        /// The number 1 key.
        /// </summary>
        Number1 = 110,
        /// <summary>
        /// The number 2 key.
        /// </summary>
        Number2 = 111,
        /// <summary>
        /// The number 3 key.
        /// </summary>
        Number3 = 112,
        /// <summary>
        /// The number 4 key.
        /// </summary>
        Number4 = 113,
        /// <summary>
        /// The number 5 key.
        /// </summary>
        Number5 = 114,
        /// <summary>
        /// The number 6 key.
        /// </summary>
        Number6 = 115,
        /// <summary>
        /// The number 7 key.
        /// </summary>
        Number7 = 116,
        /// <summary>
        /// The number 8 key.
        /// </summary>
        Number8 = 117,
        /// <summary>
        /// The number 9 key.
        /// </summary>
        Number9 = 118,
        /// <summary>
        /// The tilde key.
        /// </summary>
        Tilde = 119,
        /// <summary>
        /// The grave key (equivaent to Tilde).
        /// </summary>
        Grave = 119,
        /// <summary>
        /// The minus key.
        /// </summary>
        Minus = 120,
        /// <summary>
        /// The plus key.
        /// </summary>
        Plus = 121,
        /// <summary>
        /// The left bracket key.
        /// </summary>
        BracketLeft = 122,
        /// <summary>
        /// The left bracket key (equivalent to BracketLeft).
        /// </summary>
        LBracket = 122,
        /// <summary>
        /// The right bracket key.
        /// </summary>
        BracketRight = 123,
        /// <summary>
        /// The right bracket key (equivalent to BracketRight).
        /// </summary>
        RBracket = 123,
        /// <summary>
        /// The semicolon key.
        /// </summary>
        Semicolon = 124,
        /// <summary>
        /// The quote key.
        /// </summary>
        Quote = 125,
        /// <summary>
        /// The comma key.
        /// </summary>
        Comma = 126,
        /// <summary>
        /// The period key.
        /// </summary>
        Period = 127,
        /// <summary>
        /// The slash key.
        /// </summary>
        Slash = 128,
        /// <summary>
        /// The backslash key.
        /// </summary>
        BackSlash = 129,
        /// <summary>
        /// The secondary backslash key.
        /// </summary>
        NonUSBackSlash = 130,
        /// <summary>
        /// Indicates the last available keyboard key.
        /// </summary>
        LastKey = 131,
        /// <summary>
        /// The Command key.
        /// </summary>
        Command = 132,

        // =====================================
        // GAMEPAD BUTTONS START HERE
        // =====================================

        /// <summary>
        /// The gamepad A button.
        /// </summary>
        GamepadA = 1001,
        /// <summary>
        /// The gamepad B button.
        /// </summary>
        GamepadB = 1002,
        /// <summary>
        /// The gamepad Back button.
        /// </summary>
        GamepadBack = 1003,
        /// <summary>
        /// The gamepad D-Pad down direction.
        /// </summary>
        GamepadDPadDown = 1004,
        /// <summary>
        /// The gamepad D-Pad left direction.
        /// </summary>
        GamepadDPadLeft = 1005,
        /// <summary>
        /// The gamepad D-Pad right direction.
        /// </summary>
        GamepadDPadRight = 1006,
        /// <summary>
        /// The gamepad D-Pad up direction.
        /// </summary>
        GamepadDPadUp = 1008,
        /// <summary>
        /// The gamepad home ("big center button") button.
        /// </summary>
        GamepadHome = 1009,
        /// <summary>
        /// The gamepad left shoulder button.
        /// </summary>
        GamepadLeftShoulder = 1010,
        /// <summary>
        /// The gamepad left stick button (stick pressed down).
        /// </summary>
        GamepadLeftStickPress = 1011,
        /// <summary>
        /// The gamepad left stick up direction.
        /// </summary>
        GamepadLeftStickDown = 1012,
        /// <summary>
        /// The gamepad left stick left direction.
        /// </summary>
        GamepadLeftStickLeft = 1013,
        /// <summary>
        /// The gamepad left stick right direction.
        /// </summary>
        GamepadLeftStickRight = 1014,
        /// <summary>
        /// The gamepad left stick up direction.
        /// </summary>
        GamepadLeftStickUp = 1015,
        /// <summary>
        /// The gamepad left trigger.
        /// </summary>
        GamepadLeftTrigger = 1016,
        /// <summary>
        /// The gamepad right shoulder button.
        /// </summary>
        GamepadRightShoulder = 1017,
        /// <summary>
        /// The gamepad right stick button (stick pressed down).
        /// </summary>
        GamepadRightStickPress = 1018,
        /// <summary>
        /// The gamepad right stick down direction.
        /// </summary>
        GamepadRightStickDown = 1019,
        /// <summary>
        /// The gamepad right stick left direction.
        /// </summary>
        GamepadRightStickLeft = 1020,
        /// <summary>
        /// The gamepad right stick right direction.
        /// </summary>
        GamepadRightStickRight = 1021,
        /// <summary>
        /// The gamepad right stick up direction.
        /// </summary>
        GamepadRightStickUp = 1022,
        /// <summary>
        /// The gamepad right trigger.
        /// </summary>
        GamepadRightTrigger = 1023,
        /// <summary>
        /// The gamepad start button.
        /// </summary>
        GamepadStart = 1024,
        /// <summary>
        /// The gamepad X button.
        /// </summary>
        GamepadX = 1025,
        /// <summary>
        /// The gamepad Y button.
        /// </summary>
        GamepadY = 1026
    }
}
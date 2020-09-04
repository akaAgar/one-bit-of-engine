using System;

namespace OneBitOfEngine.Input
{
    /// <summary>
    /// Enumerates modifier keys.
    /// </summary>
    [Flags]
    public enum ModifierKeys
    {
        /// <summary>
        /// The alt key modifier (option on Mac).
        /// </summary>
        Alt = 1,
        /// <summary>
        /// The control key modifier.
        /// </summary>
        Control = 2,
        /// <summary>
        /// The shift key modifier.
        /// </summary>
        Shift = 4,
        /// <summary>
        /// The command key modifier on a Mac.
        /// </summary>
        Command = 8
    }
}
using OneBitOfEngine.Core;
using OneBitOfEngine.Input;
using OneBitOfEngine.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneBitOfEngine.UI.Controls
{
    /// <summary>
    /// A control offering a selection of items the user can scroll through and select.
    /// </summary>
    public class UIMenu : UIControl
    {
        /// <summary>
        /// UI Menu event
        /// </summary>
        /// <param name="index">Index of the currently selected menu item</param>
        /// <param name="key">Key of the currently selected menu item</param>
        /// <param name="text">Text of the currently selected menu item</param>
        public delegate void UIMenuEvent(int index, string key, string text);

        /// <summary>
        /// (Private) Menu items
        /// </summary>
        private readonly List<UIMenuItem?> MenuItems = new List<UIMenuItem?>();

        /// <summary>
        /// The tile to use for this control's font.
        /// Font tiles must follow one another on the tilemap (but can be on multiple rows) and provide all the ASCII characters in the 32 (white space) to 126 (~) range.
        /// </summary>
        public int FontTile { get { return FontTile_; } set { FontTile_ = value; Page.UI.Invalidate(); } }
        private int FontTile_ = 0;

        /// <summary>
        /// Max length of the text of each menu entry. Zero or less means no max length.
        /// </summary>
        public int MaxLength { get { return MaxLength_; } set { MaxLength_ = value; Page.UI.Invalidate(); } }
        private int MaxLength_ = 0;

        /// <summary>
        /// The color of the selected menu item.
        /// </summary>
        public RGBColor SelectedColor { get { return SelectedColor_; } set { SelectedColor_ = value; Page.UI.Invalidate(); } }
        private RGBColor SelectedColor_ = RGBColor.Yellow;

        /// <summary>
        /// The tile special effect to use on the selected menu item.
        /// </summary>
        public TileVFX SelectedVFX { get { return SelectedVFX_; } set { SelectedVFX_ = value; Page.UI.Invalidate(); } }
        private TileVFX SelectedVFX_ = TileVFX.None;

        /// <summary>
        /// Index of the currently selected menu item
        /// </summary>
        public int SelectedIndex { get { return SelectedIndex_; }
            set {
                if (MenuItems.Count == 0) { SelectedIndex = 0; return; }

                SelectedIndex_ = (MenuItems.Count > 0) ? OneBitOfTools.Clamp(value, 0, MenuItems.Count - 1) : 0;
                SelectedIndex_ = GetNextNonSeparatorIndex(SelectedIndex_, 1);

                if (MenuItems[SelectedIndex_].HasValue)
                    OnSelectedItemChanged?.Invoke(SelectedIndex, MenuItems[SelectedIndex_].Value.Key, MenuItems[SelectedIndex_].Value.Text);

                Page.UI.Invalidate();
            }
        }
        private int SelectedIndex_ = 0;

        /// <summary>
        /// Number of items in the menu
        /// </summary>
        public int ItemsCount { get { return MenuItems.Count; } }

        /// <summary>
        /// An array of keys which, when pressed, will decrease this menu's selected item (aka move selection up).
        /// </summary>
        public KeyCode[] SelectionUpKeys { get; set; } = new KeyCode[] { KeyCode.Up, KeyCode.GamepadDPadUp, KeyCode.GamepadLeftStickUp, KeyCode.GamepadRightStickUp };

        /// <summary>
        /// An array of keys which, when pressed, will increase this menu's selected item (aka move selection down).
        /// </summary>
        public KeyCode[] SelectionDownKeys { get; set; } = new KeyCode[] { KeyCode.Down, KeyCode.GamepadDPadDown, KeyCode.GamepadLeftStickDown, KeyCode.GamepadRightStickDown };

        /// <summary>
        /// An array of keys which, when pressed, will raise an item validation event.
        /// </summary>
        public KeyCode[] ValidationKeys { get; set; } = new KeyCode[] { KeyCode.Space, KeyCode.Enter, KeyCode.KeypadEnter, KeyCode.GamepadA, KeyCode.GamepadX, KeyCode.GamepadY, KeyCode.GamepadLeftTrigger, KeyCode.GamepadRightTrigger };

        /// <summary>
        /// If true, when the selected menu item reaches the top or the bottom of the list, it goes all the way to the other side.
        /// If false, it stays there.
        /// </summary>
        public bool LoopItemSelection { get; set; } = true;

        /// <summary>
        /// Event raised when the selected menu item changes.
        /// </summary>
        public event UIMenuEvent OnSelectedItemChanged = null;

        /// <summary>
        /// Event raised when the selected menu item is validated.
        /// </summary>
        public event UIMenuEvent OnSelectedItemValidated = null;

        /// <summary>
        /// If true, will check inputs to allow seleciton 
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Adds a new item to the menu.
        /// </summary>
        /// <param name="text">Text of the new item</param>
        /// <param name="key">Key of the new item</param>
        /// <returns>The index of the new item</returns>
        public int AddMenuItem(string text, string key = "")
        {
            MenuItems.Add(new UIMenuItem(text, key));
            Page.UI.Invalidate();
            return MenuItems.Count - 1;
        }

        /// <summary>
        /// Adds a new separator to the menu.
        /// </summary>
        /// <returns>The index of the new separator</returns>
        public int AddSeparator()
        {
            MenuItems.Add(null);
            return MenuItems.Count - 1;
        }

        /// <summary>
        /// Removes all menu items.
        /// </summary>
        public void Clear()
        {
            MenuItems.Clear();
            Page.UI.Invalidate();
        }

        private int GetMenuItemIndexByKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return -1;
            key = key.ToLowerInvariant().Trim();

            for (int i = 0; i < MenuItems.Count; i++)
            {
                if (MenuItems[i].HasValue && (MenuItems[i].Value.Key == key))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Changes the text of a menu item.
        /// </summary>
        /// <param name="index">Index of the menu item to change</param>
        /// <param name="text">New text for the menu item</param>
        public void ChangeMenuItemText(int index, string text)
        {
            if ((index < 0) || (index >= MenuItems.Count)) return;
            if (!MenuItems[index].HasValue) return;

            MenuItems[index] = new UIMenuItem(text, MenuItems[index].Value.Key);
            Page.UI.Invalidate();
        }

        /// <summary>
        /// Changes the text of a menu item.
        /// </summary>
        /// <param name="key">Key of the menu item to change</param>
        /// <param name="text">New text for the menu item</param>
        public void ChangeMenuItemText(string key, string text)
        {
            int index = GetMenuItemIndexByKey(key);
            if (index >= 0)
                ChangeMenuItemText(index, text);
        }

        /// <summary>
        /// Removes a menu item. CAUTION: Will mess up the menu item indices.
        /// </summary>
        /// <param name="index">Index of the menu item to remove</param>
        public void RemoveMenuItem(int index)
        {
            if ((index < 0) || (index >= MenuItems.Count)) return;
            MenuItems.RemoveAt(index);

            if (SelectedIndex_ >= MenuItems.Count)
            {
                SelectedIndex_ = MenuItems.Count - 1;

                if (MenuItems[SelectedIndex_].HasValue)
                    OnSelectedItemChanged?.Invoke(SelectedIndex, MenuItems[SelectedIndex_].Value.Key, MenuItems[SelectedIndex_].Value.Text);
            }

            Page.UI.Invalidate();
        }

        public void RemoveMenuItem(string key)
        {
            int index = GetMenuItemIndexByKey(key);
            if (index >= 0)
                RemoveMenuItem(index);
        }

        /// <summary>
        /// Raises a <see cref="OnSelectedItemValidated"/> event with the currently selected menu item.
        /// </summary>
        public void ValidateSelection()
        {
            if ((MenuItems.Count == 0) || !MenuItems[SelectedIndex_].HasValue) return;

            OnSelectedItemValidated?.Invoke(SelectedIndex, MenuItems[SelectedIndex_].Value.Key, MenuItems[SelectedIndex_].Value.Text);
        }

        /// <summary>
        /// (Internal) Draws the control on the provided VBO.
        /// </summary>
        /// <param name="vbo">UI VBO on which to draw the control.</param>
        internal override void UpdateVBOTiles(VBO vbo)
        {
            for (int i = 0; i < MenuItems.Count; i++)
            {
                if (!MenuItems[i].HasValue) continue;

                DrawTextOnVBO(
                    vbo, MenuItems[i].Value.Text, Position.X, Position.Y + i, FontTile_,
                    (Enabled && (SelectedIndex == i)) ? SelectedColor_ : Color,
                    (Enabled && (SelectedIndex == i)) ? SelectedVFX_ : TileEffect);
            }
        }

        private int GetNextNonSeparatorIndex(int initialIndex, int direction)
        {
            direction = Math.Sign(direction);

            for (int i = 0; i < MenuItems.Count; i += direction)
            {
                int index = (initialIndex + i) % MenuItems.Count;
                if (index < 0) index += MenuItems.Count;
                if (MenuItems[index].HasValue) return index;
            }

            return initialIndex;
        }

        /// <summary>
        /// (Internal) Called whenever an input event is raised when this control is displayed.
        /// </summary>
        /// <param name="key">The key or gamepad button that raised the event</param>
        /// <param name="modifiers">Which modifier keys are down?</param>
        /// <param name="gamepadIndex">Index of the gamepad that raised the event, if the key is a gamepad button, or -1 if it was a keyboard key</param>
        /// <param name="isRepeat">Is this a "repeated key press" event, automatically generated while the used holds the key down?</param>
        internal override void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            if (!Enabled || (MenuItems.Count == 0)) return; // Not enabled or no menu items, do nothing

            if (SelectionUpKeys.Contains(key))
            {
                //SelectedIndex_--;
                //if (SelectedIndex_ < 0) SelectedIndex_ = LoopItemSelection ? MenuItems.Count - 1 : 0;
                SelectedIndex_ = GetNextNonSeparatorIndex(SelectedIndex - 1, -1);

                if (MenuItems[SelectedIndex_].HasValue)
                    OnSelectedItemChanged?.Invoke(SelectedIndex, MenuItems[SelectedIndex_].Value.Key, MenuItems[SelectedIndex_].Value.Text);
                Page.UI.Invalidate();
            }
            else if (SelectionDownKeys.Contains(key))
            {
                //SelectedIndex_++;
                //if (SelectedIndex_ >= MenuItems.Count) SelectedIndex_ = LoopItemSelection? 0 : MenuItems.Count - 1;
                SelectedIndex_ = GetNextNonSeparatorIndex(SelectedIndex + 1, 1);

                if (MenuItems[SelectedIndex_].HasValue)
                    OnSelectedItemChanged?.Invoke(SelectedIndex, MenuItems[SelectedIndex_].Value.Key, MenuItems[SelectedIndex_].Value.Text);
                Page.UI.Invalidate();
            }
            else if (ValidationKeys.Contains(key))
                ValidateSelection();
        }
    }
}

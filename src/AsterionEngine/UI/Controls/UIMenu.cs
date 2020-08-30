using Asterion.Core;
using Asterion.OpenGL;
using System.Collections.Generic;

namespace Asterion.UI.Controls
{
    /// <summary>
    /// A control offering a selection of items the user can scroll through and select.
    /// </summary>
    public class UIMenu : UIControl
    {
        /// <summary>
        /// (Private) Menu items
        /// </summary>
        private readonly List<string> MenuItems = new List<string>();

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
        /// Index of the currently selected menu item
        /// </summary>
        public int SelectedIndex { get { return SelectedIndex_; }
            set {
                SelectedIndex_ = (MenuItems.Count > 0) ? AsterionTools.Clamp(value, 0, MenuItems.Count - 1) : 0;
                Page.UI.Invalidate();
            }
        }
        private int SelectedIndex_ = 0;

        /// <summary>
        /// Number of items in the menu
        /// </summary>
        public int ItemsCount { get { return MenuItems.Count; } }

        /// <summary>
        /// Adds a new item to the menu.
        /// </summary>
        /// <param name="text">Text of the new item</param>
        /// <returns>The index of the new item</returns>
        public int AddMenuItem(string text)
        {
            MenuItems.Add(text);
            Page.UI.Invalidate();
            return MenuItems.Count - 1;
        }

        /// <summary>
        /// Changes the text of a menu item.
        /// </summary>
        /// <param name="index">Index of the menu item to change</param>
        /// <param name="text">New text for the menu item</param>
        public void ChangeMenuItemText(int index, string text)
        {
            if ((index < 0) || (index >= MenuItems.Count)) return;
            MenuItems[index] = text;
            Page.UI.Invalidate();
        }

        /// <summary>
        /// Removes a menu item.
        /// </summary>
        /// <param name="index">Index of the menu item to remove</param>
        public void RemoveMenuItem(int index)
        {
            if ((index < 0) || (index >= MenuItems.Count)) return;
            MenuItems.RemoveAt(index);
            Page.UI.Invalidate();
        }

        /// <summary>
        /// (Internal) Draws the control on the provided VBO.
        /// </summary>
        /// <param name="vbo">UI VBO on which to draw the control.</param>
        internal override void UpdateVBOTiles(VBO vbo)
        {
            for (int i = 0; i < MenuItems.Count; i++)
                DrawTextOnVBO(
                    vbo, MenuItems[i], Position.X, Position.Y + i, FontTile_,
                    (SelectedIndex == i) ? SelectedColor_ : Color);
        }
    }
}

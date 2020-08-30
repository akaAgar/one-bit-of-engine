using Asterion.Core;
using Asterion.OpenGL;
using System;
using System.Text;

namespace Asterion.UI
{
    /// <summary>
    /// A label control: a single line string of text
    /// </summary>
    public class UILabel : UIControl
    {
        /// <summary>
        /// The tile to use for this control's font.
        /// Font tiles must follow one another on the tilemap (but can be on multiple rows) and handle all the ASCII character in the 32 (white space) to 126 (~) range.
        /// </summary>
        public int FontTile { get { return FontTile_; } set { FontTile_ = value; Page.UI.Invalidate(); } }
        private int FontTile_ = 0;

        /// <summary>
        /// The text of this label.
        /// </summary>
        public string Text { get { return Text_; } set { Text_ = value; Page.UI.Invalidate(); } }
        private string Text_ = "";

        /// <summary>
        /// Max length of the text. Zero or less means no max length.
        /// </summary>
        public int MaxLength { get { return MaxLength_; } set { MaxLength_ = value; Page.UI.Invalidate(); } }
        private int MaxLength_ = 0;

        /// <summary>
        /// (Internal) Draws the control on the provided VBO.
        /// </summary>
        /// <param name="vbo">UI VBO on which to draw the control.</param>
        internal override void UpdateVBOTiles(VBO vbo)
        {
            if (string.IsNullOrEmpty(Text_)) return;

            string realText = Text_;
            if (MaxLength_ > 0) realText = Text.Substring(0, Math.Min(realText.Length, MaxLength_));

            DrawTextOnVBO(vbo, realText, Position.X, Position.Y, FontTile_);
        }
    }
}

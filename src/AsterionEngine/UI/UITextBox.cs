using Asterion.OpenGL;
using System;

namespace Asterion.UI
{
    public class UITextBox : UIControlRectangle
    {
        /// <summary>
        /// The tile to use for this control's font.
        /// Font tiles must follow one another on the tilemap (but can be on multiple rows) and handle all the ASCII character in the 32 (white space) to 126 (~) range.
        /// </summary>
        public int FontTile { get { return FontTile_; } set { FontTile_ = value; Page.UI.Invalidate(); } }
        private int FontTile_ = 0;

        /// <summary>
        /// The text of this textbox.
        /// </summary>
        public string Text { get { return Text_; } set { Text_ = value; Page.UI.Invalidate(); } }
        private string Text_ = "";

        /// <summary>
        /// (Internal) Draws the control on the provided VBO.
        /// </summary>
        /// <param name="vbo">UI VBO on which to draw the control.</param>
        internal override void UpdateVBOTiles(VBO vbo)
        {
            string[] lines = AsterionTools.WordWrap(Text_, Width);

            for (int i = 0; i < lines.Length; i++)
            {
                if (i >= Height) break;
                DrawTextOnVBO(vbo, lines[i], Position.X, Position.Y + i, FontTile_);
            }
        }
    }
}

using OneBitOfEngine.OpenGL;
using System;

namespace OneBitOfEngine.UI.Controls
{
    /// <summary>
    /// A control displaying a multiline text in a given rectangle.
    /// </summary>
    public class UITextBox : UIControlRectangle
    {
        /// <summary>
        /// The tile to use for this control's font.
        /// Font tiles must follow one another on the tilemap (but can be on multiple rows) and provide all the ASCII characters in the 32 (white space) to 126 (~) range.
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
            string[] lines = OneBitOfTools.WordWrap(Text_, Width);

            if (lines.Length >= Height) // Too many lines
            {
                lines[Height - 1] = lines[Height - 1].Substring(0, Math.Min(lines[Height - 1].Length, Width - 4));
                lines[Height - 1] += "...";
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (i >= Height) break;
                DrawTextOnVBO(vbo, lines[i], Position.X, Position.Y + i, FontTile_, Color, TileEffect);
            }
        }
    }
}

using OneBitOfEngine.Core;
using OneBitOfEngine.Input;
using OneBitOfEngine.OpenGL;
using System.Text;

namespace OneBitOfEngine.UI.Controls
{
    /// <summary>
    /// Allows the user to input text. Basically an editable <see cref="UILabel"/>.
    /// <see cref="UIControl.InputEnabled"/> must be enabled for this control before the keyboard input is read.
    /// </summary>
    public class UIInputField : UILabel
    {
        public bool ReadInputs { get { return ReadInputs_; } set { ReadInputs_ = value; Page.UI.Invalidate(); } }

        private bool ReadInputs_ = false;

        internal override void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            if (!ReadInputs_) return;

            switch (key)
            {
                case KeyCode.BackSpace:
                    if (Text.Length > 0) Text = Text.Substring(0, Text.Length - 1);
                    return;
                case KeyCode.Enter:
                case KeyCode.KeypadEnter:
                    ReadInputs = false;
                    return;
            }
        }

        internal override void OnKeyPressEvent(char keyChar)
        {
            string keyString = keyChar.ToString().Normalize(NormalizationForm.FormD);
            if (keyString.Length < 1) return;
            
            int asciiCode = Encoding.ASCII.GetBytes(keyString)[0];
            if ((asciiCode < 32) || (asciiCode > 126)) keyString = "?";

            Text += keyString[0];
            Page.UI.Invalidate();
        }

        internal override void UpdateVBOTiles(VBO vbo)
        {
            base.UpdateVBOTiles(vbo);

            if (ReadInputs_)
            {
                vbo.UpdateTileData(Position.X + Text.Length, Position.Y, FontTile + 63, Color, Tilemap, TileVFX.BlinkFast);
            }
        }
    }
}

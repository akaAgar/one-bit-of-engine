using OneBitOfEngine.Input;

namespace OneBitOfEngine.UI.Controls
{
    /// <summary>
    /// Allows the user to input text. Basically an editable <see cref="UILabel"/>.
    /// <see cref="UIControl.InputEnabled"/> must be enabled for this control before the keyboard input is read.
    /// </summary>
    public class UIInputBox : UILabel
    {
        internal override void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            switch (key)
            {
                case KeyCode.A: Text += modifiers.HasFlag(ModifierKeys.Shift) ? "A" : "a"; break;
                default: return;
            }

            // TODO: check text length

            Page.UI.Invalidate();
        }
    }
}

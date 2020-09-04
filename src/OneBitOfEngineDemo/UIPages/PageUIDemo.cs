using OneBitOfEngine.Core;
using OneBitOfEngine.Input;
using OneBitOfEngine.UI;
using OneBitOfEngine.UI.Controls;

namespace OneBitOfEngine.Demo.UIPages
{
    public sealed class PageUIDemo : UIPage
    {
        private UIInputField InputField;

        protected override void OnInitialize(object[] parameters)
        {
            AddFrame(0, 0, UI.Game.Renderer.TileCount.Width, UI.Game.Renderer.TileCount.Height, (int)TileID.Frame, RGBColor.Goldenrod);
            AddLabel(2, 2, "UI CONTROLS DEMO", (int)TileID.Font, RGBColor.PaleGoldenrod);

            InputField = AddInputField(2, 4, "Your text here", (int)TileID.Font, RGBColor.White);
            InputField.MaxLength = 32;
            InputField.ReadInputs = true;

            AddLabel(2, UI.Game.Renderer.TileCount.Height - 3, "ESC / Gamepad B button: Back", (int)TileID.Font, RGBColor.PaleGoldenrod);
        }

        private void OnMenuItemValidated(int selectedIndex, string selectedText)
        {
            switch (selectedIndex)
            {
                case 0: UI.Game.Audio.PlaySound("impact.wav"); return;
                case 1: UI.Game.Audio.PlaySound("fire.wav"); return;
                case 2: UI.Game.Audio.PlayLoopingSound("fire.wav"); return;
            }
        }

        protected override void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            switch (key)
            {
                case KeyCode.Escape:
                case KeyCode.GamepadB:
                    UI.ShowPage<PageMainMenu>();
                    return;
            }
        }
    }
}

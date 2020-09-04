using Asterion.Core;
using Asterion.Input;
using Asterion.UI;
using Asterion.UI.Controls;

namespace Asterion.Demo.UIPages
{
    public sealed class PageInputDemo : UIPage
    {
        private UILabel KeyNameLabel;

        protected override void OnInitialize(object[] parameters)
        {
            AddFrame(0, 0, UI.Game.Renderer.TileCount.Width, UI.Game.Renderer.TileCount.Height, (int)TileID.Frame, RGBColor.Goldenrod);

            AddLabel(2, 2, "INPUT DEMO", (int)TileID.Font, RGBColor.PaleGoldenrod);
            KeyNameLabel = AddLabel(2, 4, "Press any key or gamepad button", (int)TileID.Font, RGBColor.White);

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

                default:
                    KeyNameLabel.Text = "You pressed " + key.ToString();
                    return;
            }
        }
    }
}

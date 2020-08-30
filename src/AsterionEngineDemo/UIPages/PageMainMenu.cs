using Asterion.Core;
using Asterion.Input;
using Asterion.UI;
using Asterion.UI.Controls;

namespace Asterion.Demo.UIPages
{
    public sealed class PageMainMenu : UIPage
    {
        protected override void OnInitialize(object[] parameters)
        {
            UIFrame frame = AddFrame(
                0, 0, UI.Game.Renderer.TileCount.Width, UI.Game.Renderer.TileCount.Height,
                (int)TileID.Frame, RGBColor.Goldenrod);
            frame.ZOrder = 1;

            AddLabel(2, 2, "WELCOME TO THE ASTERION DEMO GAME PROJECT", (int)TileID.Font, RGBColor.PaleGoldenrod);

            AddLabel(2, 4, "Please select an option:", (int)TileID.Font, RGBColor.PaleGoldenrod);
            UIMenu menu = AddMenu(2, 6, (int)TileID.Font, RGBColor.White);
            menu.SelectedColor = RGBColor.Yellow;
            menu.AddMenuItem("User interface demo");
            menu.AddMenuItem("Drawing board");
            menu.AddMenuItem("Audio demo");
            menu.AddMenuItem("Game world demo");
            menu.OnSelectedItemValidated += OnMenuItemValidated;

            AddLabel(2, UI.Game.Renderer.TileCount.Height - 4, "[UP,DOWN]: select, [ENTER]: validate", (int)TileID.Font, RGBColor.PaleGoldenrod);
            AddLabel(2, UI.Game.Renderer.TileCount.Height - 3, "[F]: fullscreen toggle, [ESC]: quit", (int)TileID.Font, RGBColor.PaleGoldenrod);
        }

        private void OnMenuItemValidated(int selectedIndex, string selectedText)
        {
            switch (selectedIndex)
            {
                case 0: return;
                case 1: UI.ShowPage<PageDrawingBoard>(); return;
                case 2: UI.ShowPage<PageAudioDemo>(); return;
                case 3: return;
            }
        }

        protected override void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            switch (key)
            {
                case KeyCode.Escape:
                    UI.Game.Close();
                    return;
            }
        }
    }
}

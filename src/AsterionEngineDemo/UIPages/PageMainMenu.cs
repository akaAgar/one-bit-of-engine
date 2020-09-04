using Asterion.Core;
using Asterion.Input;
using Asterion.UI;
using Asterion.UI.Controls;

namespace Asterion.Demo.UIPages
{
    public sealed class PageMainMenu : UIPage
    {
        private int MenuUI, MenuVFX, MenuAudio, MenuGameWorld, MenuDrawingBoard, MenuExit;

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
            menu.SelectedVFX = TileVFX.Negative;

            MenuUI = menu.AddMenuItem("User interface demo");
            MenuVFX = menu.AddMenuItem("Visual FX demo");
            MenuAudio = menu.AddMenuItem("Audio demo");
            MenuGameWorld = menu.AddMenuItem("Game world demo");
            MenuDrawingBoard = menu.AddMenuItem("Drawing board");
            MenuExit = menu.AddMenuItem("Exit");
            menu.OnSelectedItemValidated += OnMenuItemValidated;

            AddLabel(2, UI.Game.Renderer.TileCount.Height - 4, "[UP,DOWN]: select, [ENTER]: validate", (int)TileID.Font, RGBColor.PaleGoldenrod);
            AddLabel(2, UI.Game.Renderer.TileCount.Height - 3, "[F]: fullscreen toggle, [ESC]: quit", (int)TileID.Font, RGBColor.PaleGoldenrod);
        }

        private void OnMenuItemValidated(int selectedIndex, string selectedText)
        {
            if (selectedIndex == MenuVFX) UI.ShowPage<PageVFXDemo>();
            else if (selectedIndex == MenuAudio) UI.ShowPage<PageAudioDemo>();
            else if (selectedIndex == MenuDrawingBoard) UI.ShowPage<PageDrawingBoard>();
            else if (selectedIndex == MenuExit) UI.Game.Close();
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

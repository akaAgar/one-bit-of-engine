using OneBitOfEngine.Core;
using OneBitOfEngine.Input;
using OneBitOfEngine.UI;
using OneBitOfEngine.UI.Controls;

namespace OneBitOfEngine.Demo.UIPages
{
    public sealed class PageMainMenu : UIPage
    {
        private int MenuUI, MenuVFX, MenuAudio, MenuGameWorld, MenuDrawingBoard, MenuInput, MenuExit;

        protected override void OnInitialize(object[] parameters)
        {
            UIFrame frame = AddFrame(
                0, 0, UI.Game.Renderer.TileCount.Width, UI.Game.Renderer.TileCount.Height,
                (int)TileID.Frame, RGBColor.Goldenrod);
            frame.ZOrder = 1;

            AddLabel(2, 2, "WELCOME TO THE ONE BIT OF ENGINE DEMO", (int)TileID.Font, RGBColor.PaleGoldenrod);

            AddLabel(2, 4, "Please select an option:", (int)TileID.Font, RGBColor.PaleGoldenrod);
            
            UIMenu menu = AddMenu(2, 6, (int)TileID.Font, RGBColor.White);
            menu.SelectedColor = RGBColor.Yellow;
            menu.SelectedVFX = TileVFX.Negative;

            MenuUI = menu.AddMenuItem("User interface demo");
            MenuGameWorld = menu.AddMenuItem("Game world demo");
            MenuDrawingBoard = menu.AddMenuItem("Drawing board");
            MenuVFX = menu.AddMenuItem("Visual FX demo");
            MenuAudio = menu.AddMenuItem("Audio demo");
            MenuInput = menu.AddMenuItem("Input demo");
            MenuExit = menu.AddMenuItem("Exit");
            menu.OnSelectedItemValidated += OnMenuItemValidated;
            menu.OnSelectedItemChanged += OnMenuItemChanged;

            AddLabel(2, UI.Game.Renderer.TileCount.Height - 4, "[UP,DOWN]: select, [ENTER]: validate", (int)TileID.Font, RGBColor.PaleGoldenrod);
            AddLabel(2, UI.Game.Renderer.TileCount.Height - 3, "[F]: fullscreen toggle, [ESC]: quit", (int)TileID.Font, RGBColor.PaleGoldenrod);
        }

        private void OnMenuItemChanged(int selectedIndex, string selectedText)
        {
            UI.Game.Audio.PlaySound("select.wav");
        }

        private void OnMenuItemValidated(int selectedIndex, string selectedText)
        {
            UI.Game.Audio.PlaySound("validate.wav");

            if (selectedIndex == MenuVFX) UI.ShowPage<PageVFXDemo>();
            else if (selectedIndex == MenuUI) UI.ShowPage<PageUIDemo>();
            else if (selectedIndex == MenuAudio) UI.ShowPage<PageAudioDemo>();
            else if (selectedIndex == MenuGameWorld) UI.ShowPage<PageGameWorld>();
            else if (selectedIndex == MenuDrawingBoard) UI.ShowPage<PageDrawingBoard>();
            else if (selectedIndex == MenuInput) UI.ShowPage<PageInputDemo>();
            else if (selectedIndex == MenuExit) UI.Game.Close();
        }

        protected override void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            switch (key)
            {
                case KeyCode.Escape:
                case KeyCode.GamepadB:
                    UI.Game.Close();
                    return;
                case KeyCode.F:
                    UI.Game.WindowState = (UI.Game.WindowState == GameWindowState.Normal) ? GameWindowState.Fullscreen : GameWindowState.Normal;
                    return;
            }
        }
    }
}

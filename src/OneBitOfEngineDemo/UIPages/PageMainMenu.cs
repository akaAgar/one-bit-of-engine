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

            AddLabel(2, 2, "ONE BIT OF ENGINE DEMO", (int)TileID.Font, RGBColor.PaleGoldenrod);

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
            menu.AddSeparator();
            MenuExit = menu.AddMenuItem("Exit");

            menu.OnSelectedItemValidated += OnMenuItemValidated;
            menu.OnSelectedItemChanged += OnMenuItemChanged;

            AddLabel(2, UI.Game.Renderer.TileCount.Height - 4, "[UP,DOWN]: select, [ENTER]: validate", (int)TileID.Font, RGBColor.PaleGoldenrod);
            AddLabel(2, UI.Game.Renderer.TileCount.Height - 3, "[F]: fullscreen toggle, [ESC]: quit", (int)TileID.Font, RGBColor.PaleGoldenrod);
        }

        private void OnMenuItemChanged(int index, string key, string text)
        {
            UI.Game.Audio.PlaySound("select.wav");
        }

        private void OnMenuItemValidated(int index, string key, string text)
        {
            UI.Game.Audio.PlaySound("validate.wav");

            if (index == MenuVFX) UI.ShowPage<PageVFXDemo>();
            else if (index == MenuUI) UI.ShowPage<PageUIDemo>();
            else if (index == MenuAudio) UI.ShowPage<PageAudioDemo>();
            else if (index == MenuGameWorld) UI.ShowPage<PageGameWorld>();
            else if (index == MenuDrawingBoard) UI.ShowPage<PageDrawingBoard>();
            else if (index == MenuInput) UI.ShowPage<PageInputDemo>();
            else if (index == MenuExit) UI.Game.Close();
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

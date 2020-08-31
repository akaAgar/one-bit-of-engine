using Asterion.Core;
using Asterion.Input;
using Asterion.UI;
using Asterion.UI.Controls;

namespace Asterion.Demo.UIPages
{
    public sealed class PageAudioDemo : UIPage
    {
        protected override void OnInitialize(object[] parameters)
        {
            UIFrame frame = AddFrame(
                0, 0, UI.Game.Renderer.TileCount.Width, UI.Game.Renderer.TileCount.Height,
                (int)TileID.Frame, RGBColor.Goldenrod);
            frame.ZOrder = 1;

            AddLabel(2, 2, "AUDIO DEMO", (int)TileID.Font, RGBColor.PaleGoldenrod);

            AddLabel(2, 4, "Please select an option:", (int)TileID.Font, RGBColor.PaleGoldenrod);
            UIMenu menu = AddMenu(2, 6, (int)TileID.Font, RGBColor.White);
            menu.SelectedColor = RGBColor.Yellow;
            menu.AddMenuItem("Play an explosion");
            menu.AddMenuItem("Play an fireball sound");
            menu.AddMenuItem("Play a looping fireball sound");
            menu.OnSelectedItemValidated += OnMenuItemValidated;

            AddLabel(2, UI.Game.Renderer.TileCount.Height - 3, "[F]: fullscreen toggle, [ESC]: back", (int)TileID.Font, RGBColor.PaleGoldenrod);
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
                    UI.ShowPage<PageMainMenu>();
                    return;
            }
        }
    }
}

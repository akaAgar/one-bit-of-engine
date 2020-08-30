using Asterion.Core;
using Asterion.Input;
using Asterion.UI;
using Asterion.UI.Controls;

namespace Asterion.Demo.UIPages
{
    public sealed class PageDrawingBoard : UIPage
    {
        protected override void OnInitialize(object[] parameters)
        {
            UIFrame frame = AddFrame(
                0, 0, UI.Game.Renderer.TileCount.Width, UI.Game.Renderer.TileCount.Height,
                (int)TileID.Frame, RGBColor.Goldenrod);
            frame.ZOrder = 1;

            AddLabel(2, UI.Game.Renderer.TileCount.Height - 3, "[F]: fullscreen toggle, [ESC]: back", (int)TileID.Font, RGBColor.PaleGoldenrod);
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

using Asterion.Core;
using Asterion.Input;
using Asterion.UI;
using Asterion.UI.Controls;

namespace Asterion.Demo.UIPages
{
    public sealed class PageVFXDemo : UIPage
    {
        protected override void OnInitialize(object[] parameters)
        {
            AddLabel(2, 2, "VISUAL FX DEMO", (int)TileID.Font, RGBColor.PaleGoldenrod);

            UILabel label = AddLabel(2, 4, "Frame animation in the GLSL shader: ", (int)TileID.Font, RGBColor.White);
            AddImage(2 + label.Text.Length, 4, 1, 1, (int)TileID.AnimationDemo, RGBColor.White);
            AddImage(3 + label.Text.Length, 4, 1, 1, (int)TileID.Skeleton, RGBColor.White);

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

using OneBitOfEngine.Core;
using OneBitOfEngine.Input;
using OneBitOfEngine.UI;
using OneBitOfEngine.UI.Controls;

namespace OneBitOfEngine.Demo.UIPages
{
    public sealed class PageVFXDemo : UIPage
    {
        private static readonly int VFX_COUNT = OneBitOfTools.EnumCount<TileVFX>();

        private UILabel LabelEffectDemo, LabelEffectName;
        private TileVFX CurrentEffect = (TileVFX)1;

        protected override void OnInitialize(object[] parameters)
        {
            AddLabel(2, 2, "VISUAL FX DEMO", (int)TileID.Font, RGBColor.PaleGoldenrod);

            AddLabel(2, 4, "Frame animation in the GLSL shader: ", (int)TileID.Font, RGBColor.PaleGoldenrod);
            AddImage(2, 5, 1, 1, (int)TileID.AnimationDemo, RGBColor.White);
            AddImage(3, 5, 1, 1, (int)TileID.Skeleton, RGBColor.White);

            LabelEffectName = AddLabel(2, 7, "", (int)TileID.Font, RGBColor.PaleGoldenrod);
            LabelEffectDemo = AddLabel(2, 8, "This is a text with a special effect", (int)TileID.Font, RGBColor.White);
            UpdateSelectedVFX();

            AddLabel(2, UI.Game.Renderer.TileCount.Height - 4, "[Left], [Right]: change VFX", (int)TileID.Font, RGBColor.PaleGoldenrod);
            AddLabel(2, UI.Game.Renderer.TileCount.Height - 3, "[F]: fullscreen toggle, [ESC]: back", (int)TileID.Font, RGBColor.PaleGoldenrod);
        }

        private void UpdateSelectedVFX()
        {
            LabelEffectDemo.TileEffect = CurrentEffect;
            LabelEffectName.Text = $"Effect {(int)CurrentEffect}/{VFX_COUNT - 1} ({CurrentEffect}):";
        }

        protected override void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            switch (key)
            {
                case KeyCode.Escape:
                case KeyCode.GamepadB:
                    UI.ShowPage<PageMainMenu>();
                    return;

                case KeyCode.Left:
                case KeyCode.GamepadDPadLeft:
                case KeyCode.GamepadLeftStickLeft:
                case KeyCode.GamepadRightStickLeft:
                    CurrentEffect--;
                    if ((int)CurrentEffect < 1) CurrentEffect = (TileVFX)(VFX_COUNT - 1);
                    UpdateSelectedVFX();
                    return;

                case KeyCode.Right:
                case KeyCode.GamepadDPadRight:
                case KeyCode.GamepadLeftStickRight:
                case KeyCode.GamepadRightStickRight:
                    CurrentEffect++;
                    if (CurrentEffect > (TileVFX)(VFX_COUNT - 1)) CurrentEffect = (TileVFX)1;
                    UpdateSelectedVFX();
                    return;
            }
        }
    }
}

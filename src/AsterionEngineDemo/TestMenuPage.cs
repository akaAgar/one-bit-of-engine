using Asterion.Core;
using Asterion.UI;

namespace Asterion.Demo
{
    public sealed class TestMenuPage : UIPage
    {
        protected override void OnInitialize(object[] parameters)
        {
            UILabel label = AddLabel(3, 3, "Hello world!", (int)TileID.Font, RGBColor.CornflowerBlue);
            label.ZOrder = 2;

            UIFrame frame = AddFrame(2, 2, 8, 8, (int)TileID.Frame, RGBColor.Goldenrod);
            frame.ZOrder = 0;
        }
    }
}

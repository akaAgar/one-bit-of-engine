using Asterion.Core;
using Asterion.UI;

namespace Asterion.Demo
{
    public sealed class TestMenuPage : UIPage
    {
        protected override void OnInitialize(object[] parameters)
        {
            UILabel label = AddControl<UILabel>();
            label.Text = "Hello world";
            label.Color = RGBColor.CornflowerBlue;
            label.Position = new Position(3, 3);
            label.ZOrder = 2;

            UIFrame frame = AddControl<UIFrame>();
            frame.TileID = (int)TileID.Font;
            frame.Position = new Position(2, 2);
            frame.Size = new Dimension(8, 8);
            frame.ZOrder = 0;
        }
    }
}

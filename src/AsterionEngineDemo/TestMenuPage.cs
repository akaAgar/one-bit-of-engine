using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Asterion.Input;
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
            frame.Tile = (int)TileID.Font;
            frame.Position = new Position(2, 2);
            frame.Size = new Dimension(8, 8);
            frame.ZOrder = 0;
        }
    }
}

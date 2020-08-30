using Asterion.Core;
using Asterion.UI;
using Asterion.UI.Controls;

namespace Asterion.Demo
{
    public sealed class TestMenuPage : UIPage
    {
        protected override void OnInitialize(object[] parameters)
        {
            UILabel label = AddLabel(2, 1, "Hello world!", (int)TileID.Font, RGBColor.CornflowerBlue);

            UIFrame frame = AddFrame(2, 2, 16, 8, (int)TileID.Frame, RGBColor.Goldenrod);
            frame.ZOrder = -1;

            UITextBox textBox = AddTextBox(3, 3, 14, 6, "", (int)TileID.Font, RGBColor.AntiqueWhite);
            textBox.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam bibendum vel augue vel consectetur. Praesent vel urna eros. Ut a blandit lacus, eget rutrum lacus.";

            UIMenu menu = AddMenu(19, 2, (int)TileID.Font, RGBColor.White);
            for (int i = 0; i < 3; i++)
                menu.AddMenuItem($"Option #{i + 1}");
        }
    }
}

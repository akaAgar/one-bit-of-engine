using Asterion.Core;
using Asterion.UI;
using Asterion.UI.Controls;

namespace Asterion.Demo
{
    public sealed class TestMenuPage : UIPage
    {
        private UILabel Label;
        private UIFrame Frame;
        private UITextBox TextBox;
        private UIMenu Menu;

        protected override void OnInitialize(object[] parameters)
        {
            Label = AddLabel(2, 1, "Hello world!", (int)TileID.Font, RGBColor.CornflowerBlue);

            Frame = AddFrame(2, 2, 16, 8, (int)TileID.Frame, RGBColor.Goldenrod);
            Frame.ZOrder = -1;

            TextBox = AddTextBox(3, 3, 14, 6, "", (int)TileID.Font, RGBColor.AntiqueWhite);
            TextBox.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam bibendum vel augue vel consectetur. Praesent vel urna eros. Ut a blandit lacus, eget rutrum lacus.";

            Menu = AddMenu(19, 2, (int)TileID.Font, RGBColor.White);
            for (int i = 0; i < 3; i++)
                Menu.AddMenuItem($"Option #{i + 1}");

            Menu.OnSelectedItemChanged += Menu_OnSelectedItemChanged;
            Menu.OnSelectedItemValidated += Menu_OnSelectedItemValidated;
        }

        private void Menu_OnSelectedItemValidated(int selectedIndex, string selectedText)
        {
            TextBox.Text = "You VALIDATED menu " + selectedText;
        }

        private void Menu_OnSelectedItemChanged(int selectedIndex, string selectedText)
        {
            TextBox.Text = "You selected menu " + selectedText;
        }
    }
}

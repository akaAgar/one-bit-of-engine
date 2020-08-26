using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Asterion.Input;
using Asterion.Menus;

namespace Asterion.Demo
{
    public sealed class TestMenuPage : MenuPage
    {
        protected override void OnInitialize(object[] parameters)
        {
            MenuLabel label = AddControl<MenuLabel>();
            label.Text = "Hello world";
            label.Color = Color.CornflowerBlue;
            label.Position = new Point(3, 3);
        }
    }
}

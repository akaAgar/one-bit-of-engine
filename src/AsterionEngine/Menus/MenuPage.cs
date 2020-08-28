using Asterion.Input;
using Asterion.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Asterion.Menus
{
    public class MenuPage
    {
        private List<MenuControl> Controls = new List<MenuControl>();

        public Tile BackgroundTile { get; set; } = new Tile(0, RGBColor.Black);

        public MenuManager Menus { get; private set; }

        public MenuPage() { }

        internal void Dispose() { }

        protected T AddControl<T>() where T : MenuControl, new()
        {
            T newControl = new T();
            newControl.Initialize(this);
            Controls.Add(newControl);
            Menus.UpdateTiles();
            return newControl;
        }

        internal void Initialize(MenuManager menuManager, object[] parameters)
        {
            Menus = menuManager;
            OnInitialize(parameters);
            Menus.UpdateTiles();
        }

        protected virtual void OnInitialize(object[] parameters) { }

        protected virtual void OnKeyDown(KeyCode key, bool shift, bool control, bool alt, bool isRepeat) { }

        public virtual void KeyDown(KeyCode key, bool shift, bool control, bool alt, bool isRepeat)
        {
            OnKeyDown(key, shift, control, alt, isRepeat);
        }

        internal void SetTiles(VBO vbo)
        {
            // Order controls by Z-Order
            Controls = Controls.OrderBy(x => x.ZOrder).ToList();

            foreach (MenuControl control in Controls)
                control.SetTiles(vbo);
        }
    }
}

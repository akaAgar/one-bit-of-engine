using Asterion.Input;
using Asterion.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asterion.Menus
{
    public class MenuPage
    {
        private List<MenuControl> Controls = new List<MenuControl>();

        public Tile BackgroundTile { get; set; } = new Tile(0, Color.Black);

        public MenuManager Menus { get; private set; }

        public MenuPage() { }

        internal void Dispose()
        {

        }

        protected T AddControl<T>() where T : MenuControl, new()
        {
            T newControl = new T();
            Controls.Add(newControl);
            UpdateControlsZOrder();
            return newControl;
        }

        internal void Initialize(MenuManager menuManager, object[] parameters)
        {
            Menus = menuManager;
            OnInitialize(parameters);
        }

        internal void UpdateControlsZOrder()
        {
            Controls = Controls.OrderBy(x => x.ZOrder).ToList();
        }

        protected virtual void OnInitialize(object[] parameters) { }

        internal void OnRender()
        {
            foreach (MenuControl control in Controls)
                control.Render();
        }

        protected virtual void OnKeyDown(KeyCode key, bool shift, bool control, bool alt, bool isRepeat) { }

        public virtual void KeyDown(KeyCode key, bool shift, bool control, bool alt, bool isRepeat)
        {
            OnKeyDown(key, shift, control, alt, isRepeat);
        }
    }
}

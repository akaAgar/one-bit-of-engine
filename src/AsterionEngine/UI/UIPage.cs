using Asterion.Input;
using Asterion.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Asterion.UI
{
    public class UIPage
    {
        private List<UIControl> Controls = new List<UIControl>();

        public Tile BackgroundTile { get; set; } = new Tile(0, RGBColor.Black);

        public UIEnvironment UI { get; private set; }

        public UIPage() { }

        internal void Dispose() { }

        protected T AddControl<T>() where T : UIControl, new()
        {
            T newControl = new T();
            newControl.Initialize(this);
            Controls.Add(newControl);
            UI.UpdateTiles();
            return newControl;
        }

        internal void Initialize(UIEnvironment menuManager, object[] parameters)
        {
            UI = menuManager;
            OnInitialize(parameters);
            UI.UpdateTiles();
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

            foreach (UIControl control in Controls)
                control.UpdateVBOTiles(vbo);
        }
    }
}

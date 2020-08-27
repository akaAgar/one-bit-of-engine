using Asterion.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asterion.Menus
{
    public abstract class MenuControl
    {
        internal MenuPage Page { get; private set; }

        public Point Position { get; set; } = Point.Empty;

        public Color Color { get; set; } = Color.White;

        public int Tile { get; set; } = 0;
        
        public int Tilemap { get; set; } = 0;

        public int ZOrder = 0;

        public MenuControl() { }

        internal void Initialize(MenuPage page) { Page = page; }

        internal virtual void Render() { }

        internal void Dispose() { }

        internal abstract void SetTiles(VBO vbo);
    }
}

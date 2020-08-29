using Asterion.Core;
using Asterion.Video;

namespace Asterion.UI
{
    public abstract class UIControl
    {
        internal UIPage Page { get; private set; }

        public Position Position { get; set; } = Position.Zero;

        public RGBColor Color { get; set; } = RGBColor.White;

        public int TileID { get; set; } = 0;
        
        public int Tilemap { get; set; } = 0;

        public int ZOrder = 0;

        public UIControl() { }

        internal virtual void Initialize(UIPage page) { Page = page; }

        internal virtual void Render() { }

        internal void Dispose() { }

        internal abstract void UpdateVBOTiles(VBO vbo);
    }
}

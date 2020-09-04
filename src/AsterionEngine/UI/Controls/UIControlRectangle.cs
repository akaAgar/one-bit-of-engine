using Asterion.Core;

namespace Asterion.UI.Controls
{
    /// <summary>
    /// A base abstract class for all rectangular controls (that is, controls with a width and an height).
    /// </summary>
    public abstract class UIControlRectangle : UIControl
    {
        /// <summary>
        /// Size of the control.
        /// </summary>
        public virtual Dimension Size { get { return Size_; } set { Size_ = value; Page.UI.Invalidate(); } }
        private Dimension Size_ = Dimension.One;

        /// <summary>
        /// Width of the control.
        /// </summary>
        public int Width { get { return Size_.Width; } set { Size_ = new Dimension(value, Size_.Height); Page.UI.Invalidate(); } }

        /// <summary>
        /// Height of the control.
        /// </summary>
        public int Height { get { return Size_.Height; } set { Size_ = new Dimension(Size_.Width, value); Page.UI.Invalidate(); } }
    }
}

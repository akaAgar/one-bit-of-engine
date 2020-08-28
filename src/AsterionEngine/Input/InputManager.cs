using System;

namespace Asterion.Input
{
    public sealed class InputManager
    {
        public delegate void MouseEvent(Position tile);
        public delegate void MouseButtonEvent(MouseButton button, Position tile);
        public delegate void MouseWheelEvent(float wheelDelta);
        public delegate void KeyboardEvent(KeyCode key, bool shift, bool control, bool alt, bool isRepeat);

        public event MouseEvent OnMouseMove = null;
        public event MouseButtonEvent OnMouseDown = null;
        public event MouseButtonEvent OnMouseUp = null;
        public event MouseWheelEvent OnMouseWheel = null;

        public event KeyboardEvent OnKeyDown = null;
        public event KeyboardEvent OnKeyUp = null;

        internal void OnKeyDownInternal(KeyCode key, bool shift, bool control, bool alt, bool isRepeat) { OnKeyDown?.Invoke(key, shift, control, alt, isRepeat); }
        internal void OnKeyUpInternal(KeyCode key, bool shift, bool control, bool alt, bool isRepeat) { OnKeyUp?.Invoke(key, shift, control, alt, isRepeat); }

        internal void OnMouseMoveInternal(Position tile) { OnMouseMove?.Invoke(tile); }
        internal void OnMouseDownInternal(MouseButton button, Position tile) { OnMouseDown?.Invoke(button, tile); }
        internal void OnMouseUpInternal(MouseButton button, Position tile) { OnMouseUp?.Invoke(button, tile); }
        internal void OnMouseWheelInternal(float wheelDelta) { OnMouseWheel?.Invoke(wheelDelta); }

        internal InputManager() { }

        internal void Dispose() { }
    }
}

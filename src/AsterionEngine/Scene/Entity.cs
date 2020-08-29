using Asterion.Core;

namespace Asterion.Scene
{
    public class Entity
    {
        public delegate void EntityEvent(Entity sender);

        public Position Position { get; set; } = Position.Zero;

        public Tile Tile { get; set; } = new Tile();
        public int Layer { get; set; } = 0;

        public SceneManager Scene { get; private set; } = null;

        public bool Removed { get; private set; } = false;

        internal bool Initialize(SceneManager sceneManager, Position position, object[] parameters)
        {
            Scene = sceneManager;
            Position = position;
            return OnInitialize(parameters);
        }

        protected virtual bool OnInitialize(object[] parameters) { return true; }
        protected virtual void OnRemove() { }

        public void Remove()
        {
            Removed = true;
            OnRemove();
        }

        public bool MoveBy(int deltaX, int deltaY)
        {
            Position newPosition = Position + new Position(deltaX, deltaY);
            if (!Scene.IsCellFree(newPosition, Layer)) return false;
            Position = newPosition;
            OnMove?.Invoke(this);
            Scene.RequireVBOUpdate();
            return true;
        }

        public event EntityEvent OnMove = null;
    }
}

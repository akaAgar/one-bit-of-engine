using Asterion.Core;
using Asterion.OpenGL;

namespace Asterion.UI
{
    public sealed class UICursor
    {
        private Tile Tile = new Tile(0, RGBColor.Black);
        private Position Position = Position.NegativeOne;

        private VBO CursorVBO = null;

        public bool Visible { get; set; } = false;

        internal UICursor() { }

        internal void OnLoad(AsterionGame game)
        {
            CursorVBO = new VBO(game.Renderer, 1, 1);
            UpdateCursor();
        }

        public void MoveBy(Position positionOffset)
        {
            MoveBy(positionOffset.X, positionOffset.Y);
        }

        public void MoveBy(int offsetX, int offsetY)
        {
            Position newCursorPosition = new Position(Position.X + offsetX, Position.Y + offsetY);
            MoveTo(newCursorPosition);
        }

        public void MoveTo(Position position)
        {
            MoveTo(position.X, position.Y);
        }
        
        public void MoveTo(int x, int y)
        {
            Position = new Position(x, y);

            UpdateCursor();
        }

        public void SetTile(Tile tile)
        {
            Tile = tile;
            UpdateCursor();
        }

        internal void Render()
        {
            if (!Visible) return;
            CursorVBO.Render();
        }

        private void UpdateCursor()
        {
            CursorVBO.UpdateTileData(0, 0, Position.X, Position.Y, Tile);
        }

        internal void Dispose()
        {
            CursorVBO?.Dispose();
        }
    }
}

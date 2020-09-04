using Asterion.Core;
using Asterion.Input;
using Asterion.UI;
using Asterion.UI.Controls;

namespace Asterion.Demo.UIPages
{
    public sealed class PageDrawingBoard : UIPage
    {
        private static readonly Position BOARD_POSITION = new Position(2, 4);
        private static readonly Dimension BOARD_SIZE = new Dimension(16, 16);

        private UITileBoard TileBoard;

        protected override void OnInitialize(object[] parameters)
        {
            UI.Cursor.Enabled = true;
            UI.Cursor.Position = BOARD_POSITION;
            UI.Cursor.BoundingBox = new Area(BOARD_POSITION, BOARD_SIZE);
            UI.Cursor.Moveable = true;

            AddLabel(1, 1, "DRAWING BOARD", (int)TileID.Font, RGBColor.PaleGoldenrod);

            AddFrame(
                BOARD_POSITION - Position.One, BOARD_SIZE + Dimension.One * 2,
                (int)TileID.Frame, RGBColor.White);

            TileBoard = AddTileBoard(BOARD_POSITION.X, BOARD_POSITION.Y, BOARD_SIZE.Width, BOARD_SIZE.Height);
            TileBoard.Clear(new UITileBoardTile(1, RGBColor.Blue));

            AddLabel(2, UI.Game.Renderer.TileCount.Height - 4, "Arrow keys, gamepad sticks/DPad: move cursor", (int)TileID.Font, RGBColor.PaleGoldenrod);
            AddLabel(2, UI.Game.Renderer.TileCount.Height - 3, "F: fullscreen toggle, ESC: back", (int)TileID.Font, RGBColor.PaleGoldenrod);
        }

        protected override void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            switch (key)
            {
                case KeyCode.Space:
                    Position boardPosition = UI.Cursor.Position - BOARD_POSITION;
                    TileBoard[boardPosition] = new UITileBoardTile(2, RGBColor.BurlyWood);
                    return;
                case KeyCode.Escape:
                    UI.ShowPage<PageMainMenu>();
                    return;
            }
        }

        protected override void OnClose()
        {
            UI.Cursor.Enabled = false;
        }
    }
}

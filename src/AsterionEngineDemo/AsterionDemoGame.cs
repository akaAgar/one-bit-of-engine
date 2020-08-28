/*
==========================================================================
This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
WadPacker is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
WadPacker is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with Asterion Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using Asterion.Input;
using Asterion.Scene;
using Asterion.Video;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;

namespace Asterion.Demo
{
    /// <summary>
    /// Main demo project class
    /// </summary>
    public class AsterionDemoGame : AsterionGame
    {
        /// <summary>
        /// Entrypoint of the application.
        /// </summary>
        private static void Main() { using (AsterionDemoGame game = new AsterionDemoGame()) { game.Run(30.0f); } }

        /// <summary>
        /// Constructor.
        /// </summary>
        //public AsterionDemoGame() : base(new Size(16, 16), new Size(32, 18), new Size(512, 64)) { }
        public AsterionDemoGame() : base(new Size(16, 16), new Size(32, 18), new Size(512, 64)) { }


        private Entity SkeletonEntity;

        /// <summary>
        /// Positions of the various walls
        /// </summary>
        //private readonly Position[] Walls = new Point[] { new Point(6, 9), new Point(34, 2), new Point(6, 11), new Point(29, 7), new Point(30, 10), new Point(17, 9), new Point(17, 6), new Point(33, 12), new Point(1, 13), new Point(25, 2), new Point(30, 9), new Point(14, 5), new Point(29, 3), new Point(30, 6), new Point(21, 11), new Point(28, 7), new Point(12, 14), new Point(16, 12), new Point(13, 5) };

        /// <summary>
        /// OnLoad() override, sets up all tiles.
        /// </summary>
        protected override void OnLoad()
        {
            Title = "OneBitOfEngine Demo Game";
            Tiles.SetTilemap(0, Image.FromFile("../../media/tilemap.png"));

            if (true)
            {
                Menu.ShowPage<TestMenuPage>();
            }
            else
            {
                GUI.ClearTiles(new Tile(0, RGBColor.Black));

                GUI.DrawFrame(new Area(0, Tiles.TileCountY - 4, Tiles.TileCountX, 4), new Tile((int)TileID.Frame, RGBColor.CornflowerBlue));
                GUI.DrawText(1, Tiles.TileCountY - 3, "Arrows: move, l-click: fire", new Tile((int)TileID.Font, RGBColor.White));
                GUI.DrawText(1, Tiles.TileCountY - 2, "F: fullscreen, ESC: exit", new Tile((int)TileID.Font, RGBColor.White));

                //TileBoard.FX.OnFXStart += OnFXStart;

                GUI.SetCursor(new Tile((int)TileID.Cursor, RGBColor.White));
                Scene.Create(128, 128);
                Scene.Map.Clear(new MapCell(new Tile((int)TileID.Grass, RGBColor.ForestGreen)));
                for (int i = 0; i < 16; i++) Scene.Map[i * 2, i] = new MapCell(new Tile((int)TileID.Wall, RGBColor.Gray));

                SkeletonEntity = Scene.AddEntity<Entity>(new Point(1, 1));
                SkeletonEntity.Tile = new Tile((int)TileID.Skeleton, RGBColor.AntiqueWhite, true);

                Scene.Viewport = new Area(0, 0, Tiles.TileCountX, Tiles.TileCountY - 4);
            }

            //DrawWorld();
            AdjustToTileScreenSize(2.0f);
        }

        /// <summary>
        /// OnMouseMove override. Moves the cursor to the currently hovered tile.
        /// </summary>
        /// <param name="tile">Currently hovered tile</param>
        public override void OnMouseMove(Position tile)
        {
            if (tile.X == -1)
                GUI.CursorVisible = false;
            else
            {
                GUI.MoveCursorTo(tile);
                GUI.CursorVisible = true;
            }
        }

        /// <summary>
        /// OnKeyDown override. Used to check if the user presses the arrow keys to move the skeleton.
        /// </summary>
        /// <param name="key">Key code</param>
        /// <param name="shift">Is shift down?</param>
        /// <param name="control">Is control down?</param>
        /// <param name="alt">Is alt down?</param>
        /// <param name="isRepeat">Is this a repeat keystroke (key held down)?</param>
        public override void OnKeyDown(KeyCode key, bool shift, bool control, bool alt, bool isRepeat)
        {
            switch (key)
            {
                case KeyCode.Up: SkeletonEntity.MoveBy(0, -1); break;
                case KeyCode.Down: SkeletonEntity.MoveBy(0, 1); break;
                case KeyCode.Left: SkeletonEntity.MoveBy(-1, 0); break;
                case KeyCode.Right: SkeletonEntity.MoveBy(1, 0); break;
                case KeyCode.F:
                    if (WindowState == GameWindowState.Normal)
                        WindowState = GameWindowState.Fullscreen;
                    else
                        WindowState = GameWindowState.Normal;
                    break;
                case KeyCode.Escape: Close(); break;
            }
        }

        /// <summary>
        /// OnMouseDown override. Used to know when the user pressed the left mouse button to throw a fireball.
        /// </summary>
        /// <param name="button">Mouse button</param>
        /// <param name="position">Currenly hovered tile</param>
        public override void OnMouseDown(MouseButton button, Position position)
        {
            //if (TileBoard.FX.InProgress) return; // Cannot fire another fireball while the fireball animation is in progress
            //if (button != MouseButton.Left) return; // Only the left button can be used to shoot fireballs

            //TileBoard.FX.AddMovingFX("fireball", SkeletonPosition, position, 0.25f, (int)TileID.Fireball, Color.Orange);

            //TileBoard.FX.AddStaticFX("fireballImpact", 0.1f,
            //    new Point[] { position, Point.Add(position, new Size(1, 0)), Point.Add(position, new Size(-1, 0)), Point.Add(position, new Size(0, -1)), Point.Add(position, new Size(0, 1)) },
            //    (int)TileID.FireballExplosion, Color.Orange, 3, 0);
        }

        /// <summary>
        /// Tries to move the skeleton in the given direction.
        /// </summary>
        /// <param name="xDir">X offset</param>
        /// <param name="yDir">Y offset</param>
        private void MoveSkeleton(int xDir, int yDir)
        {
            //Point newPosition = new Point(SkeletonPosition.X + xDir, SkeletonPosition.Y + yDir);

            //if ((newPosition.X < 0) || (newPosition.Y < 0) ||
            //    (newPosition.X >= TileBoard.TileCountX) || (newPosition.Y >= TileBoard.TileCountY - 4) ||
            //    Walls.Contains(newPosition))
            //{
            //    Audio.PlaySound("noway", "../../media/noway.wav");
            //    return;
            //}

            //Audio.PlaySound("walk", "../../media/walk.wav");

            //SkeletonPosition = newPosition;
            //DrawWorld();
        }

        /// <summary>
        /// Event raised when a new special effect starts
        /// </summary>
        /// <param name="fxName">Name of the FX</param>
        private void OnFXStart(string fxName)
        {
            switch (fxName)
            {
                case "fireball": Audio.PlaySound("fire", "../../media/fire.wav"); return;
                case "fireball_impact": Audio.PlaySound("impact", "../../media/impact.wav"); return;
            }
        }

        /// <summary>
        /// Redraw all tiles for the world (grass, walls, skeleton... everything but the frame at the bottom)
        /// </summary>
        //private void DrawWorld()
        //{
        //    //TileBoard.Tiles.ClearRegion(0, 0, TileBoard.TileCountX, TileBoard.TileCountY - 4, (int)TileID.Grass, Color.ForestGreen);

        //    //foreach (Point w in Walls)
        //    //    TileBoard.Tiles.DrawTile(w, (int)TileID.Wall, Color.Gray);

        //    //TileBoard.Tiles.DrawTile(SkeletonPosition, (int)TileID.Skeleton, Color.WhiteSmoke);
        //}
    }
}

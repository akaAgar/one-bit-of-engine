/*
==========================================================================
This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
Asterion Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
Asterion Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with Asterion Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using Asterion.Core;
using Asterion.Demo.UIPages;
using Asterion.Input;

namespace Asterion.Demo
{
    /// <summary>
    /// Main demo project class
    /// </summary>
    public class AsterionDemoGame : AsterionGame
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        private static void Main() { using (AsterionDemoGame game = new AsterionDemoGame()) { game.Run(30.0f); } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AsterionDemoGame() : base(new Dimension(16, 16), new Dimension(48, 27), new Dimension(512, 64)) { }

        protected override void OnLoad()
        {
            Files.SetFolderAsFileSource(@"..\..\media\");

            Audio.Enable();

            Title = "Asterion Engine Demo Game";
            Renderer.LoadTilemap(0, "tilemap.png");

            UI.ShowPage<PageMainMenu>();
            UI.Cursor.SetTile(new Tile((int)TileID.Cursor, RGBColor.White));
         
            AdjustToTileScreenSize(1.5f);
        }

        protected override void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            switch (key)
            {
                case KeyCode.F:
                    if (WindowState == GameWindowState.Normal)
                        WindowState = GameWindowState.Fullscreen;
                    else
                        WindowState = GameWindowState.Normal;
                    break;
            }
        }
    }
}

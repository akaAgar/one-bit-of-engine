/*
==========================================================================
This file is part of One Bit of Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
One Bit of Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
One Bit of Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with One Bit of Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using OneBitOfEngine.Core;
using OneBitOfEngine.Demo.UIPages;
using OneBitOfEngine.Input;

namespace OneBitOfEngine.Demo
{
    /// <summary>
    /// Main demo project class
    /// </summary>
    public class OneBitOfDemoGame : OneBitOfGame
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        private static void Main() { using (OneBitOfDemoGame game = new OneBitOfDemoGame()) { game.Run(30.0f); } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public OneBitOfDemoGame() : base(new Dimension(16, 16), new Dimension(48, 27), new Dimension(512, 64)) { }

        protected override void OnLoad()
        {
            Files.SetFolderAsFileSource(@"..\..\media\");

            Audio.Enable();
            Input.EnableGamePads = true;

            Title = "One Bit of Engine Demo Game";
            Renderer.LoadTilemap(0, "tilemap.png");
            Renderer.TileAnimationFrameDuration = .7f;
            Renderer.TileAnimationFrames = 3;

            UI.ShowPage<PageMainMenu>();
            UI.Cursor.Tile = (int)TileID.Cursor;
            UI.Cursor.Color = RGBColor.White;

            AdjustToTileScreenSize(1.5f);
        }
    }
}

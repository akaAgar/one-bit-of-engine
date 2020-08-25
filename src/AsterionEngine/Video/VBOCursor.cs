///*
//==========================================================================
//This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
//engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
//WadPacker is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//WadPacker is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with Asterion Engine. If not, see https://www.gnu.org/licenses/
//==========================================================================
//*/

//using System.Drawing;

//namespace Asterion.OpenGL
//{
//    internal sealed class VBOCursor : VBO
//    {
//        private Point Position = Point.Empty;

//        private int LastTile = 0;
//        private Color LastColor = Color.White;
//        private int LastTilemap = 0;

//        public bool Visible { get { return IsVisible; } set { IsVisible = value; } }

//        internal VBOCursor(TileBoard screen) : base(screen, 1) { IsVisible = false; }

//        public void MoveTo(Point position) { MoveTo(position.X, position.Y); }
//        public void MoveTo(int x, int y)
//        {
//            Set(x, y, LastTile, LastColor, LastTilemap);
//        }

//        public void Set(Point position, int tile, Color color, int tilemap = 0) { Set(position.X, position.Y, tile, color, tilemap); }
//        public void Set(int x, int y, int tile, Color color, int tilemap = 0)
//        {
//            Position = new Point(x, y);
//            LastTile = tile;
//            LastColor = color;
//            LastTilemap = tilemap;

//            UpdateTileData(0, x, y, tile, color, tilemap);
//            IsVisible = true;
//        }

//        public void MoveBy(int deltaX, int deltaY)
//        {
//            if (!IsVisible) return;

//            Position = new Point(Position.X + deltaX, Position.Y + deltaY);
//            MoveTo(Position.X, Position.Y);
//        }
//    }
//}

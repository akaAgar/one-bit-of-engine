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

//using System;
//using System.Drawing;
//using System.Text;

//namespace Asterion.Video
//{
//    internal sealed class VBOTiles : VBO
//    {
//        private int TileCountX;
//        private int TileCountY;

//        internal VBOTiles(TileManager tiles, int width, int height) : base(tiles, width * height)
//        {
//            TileCountX = width;
//            TileCountY = height;
//        }

//        internal void Resize(int width, int height)
//        {
//            TileCountX = width;
//            TileCountY = height;

//            CreateNewBuffer(width * height);
//        }

//        internal void Clear(int tile, Color color, int tilemap = 0)
//        {
//            int x, y;

//            for (x = 0; x < TileCountX; x++)
//                for (y = 0; y < TileCountY; y++)
//                    DrawTile(x, y, tile, color, tilemap);
//        }

//        internal void ClearRegion(Rectangle rect, int tile, Color color, int tilemap = 0)
//        { ClearRegion(rect.X, rect.Y, rect.Width, rect.Height, tile, color, tilemap); }

//        internal void ClearRegion(int x, int y, int width, int height, int tile, Color color, int tilemap = 0)
//        {
//            int i, j;

//            for (i = x; i < x + width; i++)
//                for (j = y; j < y + height; j++)
//                    DrawTile(i, j, tile, color, tilemap);
//        }

//        internal void DrawTile(Point pt, int tile, Color color, int tilemap = 0)
//        { DrawTile(pt.X, pt.Y, tile, color, tilemap); }
//        internal void DrawTile(int x, int y, int tile, Color color, int tilemap = 0)
//        {
//            UpdateTileData(y * TileCountX + x, x, y, tile, color, tilemap);
//        }

//        // ASCII: 32-126 Valid characters are: !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~
//        internal void DrawText(int x, int y, string text, int fontTile, Color color, int tilemap = 0, int maxlength = 0)
//        {
//            if (string.IsNullOrEmpty(text)) return;
//            if (maxlength > 0) text = text.Substring(0, Math.Min(text.Length, maxlength));

//            byte[] textBytes = Encoding.ASCII.GetBytes(text);

//            for (int i = 0; i < textBytes.Length; i++)
//            {
//                if ((textBytes[i] < 32) || (textBytes[i] > 126)) textBytes[i] = 32;
//                DrawTile(x + i, y, fontTile + textBytes[i] - 32, color, tilemap);
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="rect">Rectangle in which to draw the frame</param>
//        /// <param name="tile">The index of the first (upper-left corner) of the frame in the tilemap</param>
//        /// <param name="color">Color of the frame</param>
//        /// <param name="tilemap">Index of the tilemap</param>
//        internal void DrawFrame(Rectangle rect, int tile, Color color, int tilemap = 0)
//        {
//            int x, y;
//            int frameTile;

//            for (x = rect.Left; x < rect.Right; x++)
//                for (y = rect.Top; y < rect.Bottom; y++)
//                {
//                    frameTile = tile;

//                    if (x == rect.Left)
//                    {
//                        if (y == rect.Top) frameTile += 0;
//                        else if (y == rect.Bottom - 1) frameTile += 2;
//                        else frameTile += 5;
//                    }
//                    else if (x == rect.Right - 1)
//                    {
//                        if (y == rect.Top) frameTile += 1;
//                        else if (y == rect.Bottom - 1) frameTile += 3;
//                        else frameTile += 7;
//                    }
//                    else if (y == rect.Top) frameTile += 4;
//                    else if (y == rect.Bottom - 1) frameTile += 6;
//                    else
//                        continue;

//                    DrawTile(x, y, frameTile, color, tilemap);
//                }
//        }

//        //internal void DrawCompositeTile(int x, int y, int tile, int tileMap, int tileWidth, int tileHeight, Color color)
//        //{
//        //    int tX, tY;

//        //    for (tX = 0; tX < tileWidth; tX++)
//        //        for (tY = 0; tY < tileHeight; tY++)
//        //        {
//        //            int srcTile = tile + tX + tY * TilemapCountX;
//        //            DrawTile(x + tX, y + tY, srcTile, color, tileMap);
//        //        }
//        //}


//        //public void ClearRegion(int x, int y, int width, int height, int tile, Color color)
//        //{
//        //    int rX, rY;

//        //    for (rX = x; rX < x + width; rX++)
//        //        for (rY = y; rY < y + height; rY++)
//        //            DrawTile(rX, rY, tile, color);
//        //}

//        //internal void DrawTextBox(int left, int top, int right, int bottom, string text, int asciiTile, Color color)
//        //{
//        //    int x = left;
//        //    int y = top;

//        //    string[] words = text.Replace("\\n", "\n").Replace("\n", " \n ").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

//        //    foreach (string word in words)
//        //    {
//        //        if (word == "\n")
//        //        {
//        //            x = left;
//        //            y++;
//        //            continue;
//        //        }

//        //        if (word.Length > right - x)
//        //        {
//        //            x = left;
//        //            y++;
//        //        }

//        //        if (y >= bottom) break;

//        //        DrawText(word, x, y, asciiTile, color);
//        //        x += word.Length + 1;
//        //    }
//        //}
//    }
//}

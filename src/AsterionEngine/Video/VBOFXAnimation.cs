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
//using System.Collections.Generic;
//using System.Drawing;

//namespace Asterion.OpenGL
//{
//    internal struct VBOFXAnimation
//    {
//        internal readonly string Name;
//        internal readonly VBOFXAnimationType AnimType;
//        internal readonly int[] Tiles;
//        internal readonly Color Color;
//        internal readonly int Tilemap;
//        internal readonly float FrameTime;
//        internal readonly Point[] Positions;

//        internal VBOFXAnimation(string name, VBOFXAnimationType animType, int[] tiles, Color color, int tilemap, float time, params Point[] positions)
//        {
//            Name = name;
//            AnimType = animType;
//            Tiles = tiles;
//            Color = color;
//            Tilemap = tilemap;
//            FrameTime = time;
//            Positions = positions;

//            switch (AnimType)
//            {
//                case VBOFXAnimationType.Moving:
//                    Positions = GetPointsBetween(positions[0], positions[1]);
//                    FrameTime = time / Positions.Length;
//                    break;
//            }
//        }

//        private Point[] GetPointsBetween(Point start, Point end)
//        {
//            List<Point> points = new List<Point>();

//            float length = (float)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
//            float dX = (end.X - start.X) / length;
//            float dY = (end.Y - start.Y) / length;

//            for (float f = 0; f <= length; f += 1f)
//                points.Add(new Point((int)(start.X + dX * f), (int)(start.Y + dY * f)));
            
//            return points.ToArray();
//        }
//    }
//}

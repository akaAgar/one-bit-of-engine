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

//using OpenTK.Graphics.OpenGL4;
//using System.Collections.Generic;
//using System.Drawing;

//namespace Asterion.OpenGL
//{
//    internal sealed class VBOFX : VBO
//    {
//        internal delegate void FXEventHandler(string fxName);

//        private readonly List<VBOFXAnimation> Animations = new List<VBOFXAnimation>();

//        internal bool InProgress { get { return Animations.Count > 0; } }

//        private int AnimationFrame = 0;
//        private float FrameTime = 0f;

//        internal event FXEventHandler OnFXStart;
//        internal event FXEventHandler OnFXEnd;

//        internal VBOFX(TileBoard screen) : base(screen, 0) { }

//        internal void AddMovingFX(string name, Point start, Point end, float totalTime, int tile, Color color, int tilemap = 0)
//        {
//            Animations.Add(new VBOFXAnimation(name, VBOFXAnimationType.Moving, new int[] { tile }, color, tilemap, totalTime, start, end));
//            if (Animations.Count == 1) StartNextAnimation();
//        }

//        internal void AddStaticFX(string name, float frameTime, Point[] positions, int tile, Color color, int animFrames, int tilemap = 0)
//        {
//            int[] tiles = new int[animFrames];
//            for (int i = 0; i < animFrames; i++) tiles[i] = tile + i;

//            Animations.Add(new VBOFXAnimation(name, VBOFXAnimationType.Static, tiles, color, tilemap, frameTime, positions));
//            if (Animations.Count == 1) StartNextAnimation();
//        }

//        private void StartNextAnimation()
//        {
//            AnimationFrame = 0;
//            FrameTime = 0f;

//            if (Animations.Count == 0)
//            {
//                CreateNewBuffer(0);
//                return;
//            }

//            VBOFXAnimation anim = Animations[0];
//            OnFXStart?.Invoke(anim.Name);

//            switch (anim.AnimType)
//            {
//                case VBOFXAnimationType.Moving:
//                    CreateNewBuffer(1);
//                    SetupFXFrame();
//                    return;
//                case VBOFXAnimationType.Static:
//                    CreateNewBuffer(anim.Positions.Length);
//                    SetupFXFrame();
//                    return;
//            }
//        }

//        internal void Update(float elapsedSeconds)
//        {
//            if (!InProgress) return;

//            FrameTime += elapsedSeconds;

//            VBOFXAnimation anim = Animations[0];

//            if (FrameTime > anim.FrameTime)
//            {
//                FrameTime = 0f;
//                AnimationFrame++;

//                switch (anim.AnimType)
//                {
//                    case VBOFXAnimationType.Moving:
//                        if (AnimationFrame >= anim.Positions.Length)
//                        {
//                            OnFXEnd?.Invoke(anim.Name);
//                            Animations.RemoveAt(0);
//                            StartNextAnimation();
//                            return;
//                        }

//                        SetupFXFrame();
//                        return;

//                    case VBOFXAnimationType.Static:
//                        if (AnimationFrame >= anim.Tiles.Length)
//                        {
//                            OnFXEnd?.Invoke(anim.Name);
//                            Animations.RemoveAt(0);
//                            StartNextAnimation();
//                            return;
//                        }

//                        SetupFXFrame();
//                        return;
//                }
//            }
//        }

//        private void SetupFXFrame()
//        {
//            VBOFXAnimation anim = Animations[0];

//            switch (anim.AnimType)
//            {
//                case VBOFXAnimationType.Moving:
//                    UpdateTileData(0, anim.Positions[AnimationFrame].X, anim.Positions[AnimationFrame].Y, anim.Tiles[0], anim.Color, anim.Tilemap);
//                    return;

//                case VBOFXAnimationType.Static:
//                    for (int i = 0; i < anim.Positions.Length; i++)
//                        UpdateTileData(i, anim.Positions[i].X, anim.Positions[i].Y, anim.Tiles[AnimationFrame], anim.Color, anim.Tilemap);
//                    return;
//            }
//        }

//        internal void Clear()
//        {
//            Animations.Clear();
//            AnimationFrame = 0;
//            FrameTime = 0f;

//            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
//            GL.BufferData(BufferTarget.ArrayBuffer, 0, new float[0], BufferUsageHint.DynamicDraw);
//        }

//        internal override void Dispose()
//        {
//            Clear();
//            base.Dispose();
//        }
//    }
//}

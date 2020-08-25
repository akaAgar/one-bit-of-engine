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

using Asterion.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;

namespace Asterion
{
    public sealed class TileBoard
    {

        public const int TILEMAP_COUNT = 4;

        private int[] AnimatedTiles = new int[0];
        
        public void SetAnimatedTiles(params int[] animatedTiles)
        {
            AnimatedTiles = animatedTiles;
        }

        public float AnimationTime { get; set; } = 0.5f;

        //public VBOTiles Tiles { get; private set; }
        //public VBOFX FX { get; private set; }
        //public VBOCursor Cursor { get; private set; }

        public bool UseAlphaChannelForFX { get; set; } = false;

        private readonly TilemapTexture[] Tilemaps = new TilemapTexture[TILEMAP_COUNT];
        private TileShader Shader;

        private float TileScale = 1.0f;
        private Point TileOffset = Point.Empty;

        private readonly AsterionGame Game;

        private bool AnimationFrame = false;
        private float TotalFrameTime = 0f;

        internal TileBoard(
            AsterionGame game,
            int tileWidth, int tileHeight,
            int tileCountX, int tileCountY,
            int tilemapWidth, int tilemapHeight)

        {
            Game = game;

            TileCountX = Math.Max(1, tileCountX);
            TileCountY = Math.Max(1, tileCountY);
            TileWidth = Math.Max(1, tileWidth);
            TileHeight = Math.Max(1, tileHeight);
            TilemapWidth = Math.Max(1, tilemapWidth);
            TilemapHeight = Math.Max(1, tilemapHeight);
        }

        public bool SetTilemapImage(int index, Image tilemap)
        {
            if ((index < 0) || (index >= TILEMAP_COUNT)) return false;

            DestroyTileMap(index);
            Tilemaps[index] = new TilemapTexture(tilemap);
            return true;
        }

        private void DestroyTileMap(int index)
        {
            if ((index < 0) || (index >= TILEMAP_COUNT)) return;
            if (Tilemaps[index] == null) return;

            Tilemaps[index].Dispose();
        }

        internal void OnLoad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest); // TODO: Remove?
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // TODO: Use texture arrays - https://community.khronos.org/t/how-do-you-implement-texture-arrays/75315

            Shader = new TileShader();
            
            //Tiles = new VBOTiles(this, TileCountX, TileCountY);
            //FX = new VBOFX(this);
            //Tiles.Clear(0, Color.Black);

            //Cursor = new VBOCursor(this);
        }

        internal void OnUpdate(float elapsedSeconds)
        {
            TotalFrameTime += elapsedSeconds;

            if (TotalFrameTime >= AnimationTime)
            {
                TotalFrameTime = 0f;
                AnimationFrame = !AnimationFrame;
            }

            FX.Update(elapsedSeconds);
        }

        internal void UpdateBackgroundColor(Color color)
        {
            GL.ClearColor(color);
        }

        internal void OnRenderFrame()
        {
            Shader.Use();

            for (int i = 0; i < TILEMAP_COUNT; i++)
            {
                if (Tilemaps[i] == null) continue;
                Tilemaps[i].Use(i);
            }

            GL.Disable(EnableCap.Blend);
            Tiles.Render();
            //if (UseAlphaChannelForFX)
            //{
            //    GL.Enable(EnableCap.Blend);
            //    FX.Render();
            //}
            //else
            //{
            //    FX.Render();
            //    GL.Enable(EnableCap.Blend);
            //}
            //Cursor.Render();
        }

        internal void OnDispose()
        {
            for (int i = 0; i < TILEMAP_COUNT; i++)
                DestroyTileMap(i);

            Tiles.Dispose();
            FX.Dispose();
            Cursor.Dispose();

            // TODO
            //GL.DeleteProgram(Shader.Handle);
            //for (int i = 0; i < Tilemaps.Length; i++)
            //    GL.DeleteTexture(Tilemaps[i].Handle);
        }
    }
}

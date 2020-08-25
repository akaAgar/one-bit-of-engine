﻿/*
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

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;

namespace Asterion.Video
{
    internal class VBO
    {
        protected const int SIZE_OF_FLOAT = sizeof(float);

        protected const int FLOATS_PER_VERTEX = 8; // Vertex format is: x, y, r, g, b, u, v, tilemap
        protected const int FLOATS_PER_TILE = FLOATS_PER_VERTEX * 4;
        protected const int BYTES_PER_VERTEX = FLOATS_PER_VERTEX * SIZE_OF_FLOAT;
        protected const int BYTES_PER_TILE = BYTES_PER_VERTEX * 4;

        private readonly float UVWidth;
        private readonly float UVHeight;
        private readonly int TilemapCountX;

        internal readonly int Handle;

        /// <summary>
        /// Total number of floats (tiles × 4 × floats per vertex) in this VBO.
        /// </summary>
        internal int Length { get { return VertexCount * FLOATS_PER_VERTEX; } }

        /// <summary>
        /// Total number of vertices (tiles × 4) in this VBO.
        /// </summary>
        internal int VertexCount { get { return TileCount * 4; } }

        /// <summary>
        /// Total number of tiles in this VBO.
        /// </summary>
        internal int TileCount { get; set; } = 0;

        internal bool IsVisible = true;

        private static readonly PointF[] TILE_CORNERS = new PointF[] { new PointF(1, 0), new PointF(1, 1), new PointF(0, 1), new PointF(0, 0) };

        //protected readonly TileBoard Screen;

        internal VBO(TileManager tiles, int tileCount)
        {
            UVWidth = (float)tiles.TileWidth / tiles.TilemapWidth;
            UVHeight = (float)tiles.TileHeight / tiles.TilemapHeight;
            TilemapCountX = tiles.TilemapCountX;

            Handle = GL.GenBuffer();
            CreateNewBuffer(tileCount);
        }

        internal void CreateNewBuffer(int tileCount)
        {
            TileCount = tileCount;

            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            GL.BufferData(BufferTarget.ArrayBuffer, SIZE_OF_FLOAT * Length, new float[Length], BufferUsageHint.DynamicDraw);
        }

        internal void UpdateTileData(int index, float x, float y, Tile tile)
        {
            int tileY = tile.TileIndex / TilemapCountX;
            int tileX = tile.TileIndex - tileY * TilemapCountX;

            float[] vertexData = new float[FLOATS_PER_VERTEX * 4];

            Color4 color4 = (Color4)tile.Color;

            for (int i = 0; i < 4; i++)
                Array.Copy(
                    new float[]
                    {
                        x + TILE_CORNERS[i].X,
                        y + TILE_CORNERS[i].Y,
                        color4.R, color4.G, color4.B,
                        (tileX + TILE_CORNERS[i].X) * UVWidth,
                        (tileY + TILE_CORNERS[i].Y) * UVHeight,
                        tile.Tilemap
                    },
                    0, vertexData, FLOATS_PER_VERTEX * i, FLOATS_PER_VERTEX);

            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(index * BYTES_PER_TILE), BYTES_PER_TILE, vertexData);
        }

        internal void Render()
        {
            if (!IsVisible) return;

            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, BYTES_PER_VERTEX, 0); // x, y
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, BYTES_PER_VERTEX, 2 * SIZE_OF_FLOAT); // r, g, b
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, BYTES_PER_VERTEX, 5 * SIZE_OF_FLOAT); // u, v
            GL.VertexAttribPointer(3, 1, VertexAttribPointerType.Float, false, BYTES_PER_VERTEX, 7 * SIZE_OF_FLOAT); // tilemap
            GL.DrawArrays(PrimitiveType.Quads, 0, VertexCount);
        }

        internal virtual void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(Handle);
        }
    }
}
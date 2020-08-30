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
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;

namespace Asterion.OpenGL
{
    /// <summary>
    /// (Internal) A VertexBufferObject storing vertice data for a series of tiles.
    /// </summary>
    internal sealed class VBO : IDisposable
    {
        /// <summary>
        /// (Private) The memory size of a single-precision floating point number, in bytes.
        /// </summary>
        private const int SIZE_OF_FLOAT = sizeof(float);

        /// <summary>
        /// (Private) Number of floats per vertex.
        /// Each vertex contains 8 floats, in this order: X,Y coordinates (2), R,G,B colors (3), U,V coordinates (2), and TILEMAP index (1).
        /// </summary>
        private const int FLOATS_PER_VERTEX = 8;

        /// <summary>
        /// (Private) Number of bytes per vertex. Basically FLOATS_PER_VERTEX × SIZE_OF_FLOAT.
        /// </summary>
        private const int BYTES_PER_VERTEX = FLOATS_PER_VERTEX * SIZE_OF_FLOAT;

        /// <summary>
        /// (Private) Number of bytes per tile. Equals to BYTES_PER_VERTEX × 4 because each tile, being a rectangle, consists of 4 vertices.
        /// </summary>
        private const int BYTES_PER_TILE = BYTES_PER_VERTEX * 4;

        /// <summary>
        /// (Private) OpenGL VBO Buffer handle.
        /// </summary>
        private readonly int Handle;

        /// <summary>
        /// (Internal) Total number of floats (<see cref="TileCount"/> × 4 × <see cref="FLOATS_PER_VERTEX"/>) in this VBO.
        /// </summary>
        internal int Length { get { return VertexCount * FLOATS_PER_VERTEX; } }

        /// <summary>
        /// (Internal) Total number of vertices (<see cref="TileCount"/> × 4) in this VBO.
        /// </summary>
        internal int VertexCount { get { return TileCount * 4; } }

        /// <summary>
        /// (Internal) Total number of tiles in this VBO. Always equalts to <see cref="Columns"/> × <see cref="Rows"/>.
        /// </summary>
        internal int TileCount { get { return Columns * Rows; } }

        /// <summary>
        /// (Internal) Number of tile columns in this VBO.
        /// </summary>
        internal int Columns { get; private set; } = 0;
        
        /// <summary>
        /// (Internal) Number of tile rows in this VBO.
        /// </summary>
        internal int Rows { get; private set; } = 0;

        /// <summary>
        /// (Private, static) Coordinates for all tile corners.
        /// </summary>
        private static readonly PointF[] TILE_CORNERS = new PointF[] { new PointF(1, 0), new PointF(1, 1), new PointF(0, 1), new PointF(0, 0) };

        /// <summary>
        /// (Private) The tile renderer which will render these tiles.
        /// </summary>
        private readonly TileRenderer Renderer;

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        /// <param name="game">The tile renderer to use to render this VBO</param>
        /// <param name="columns">Number of tile columns in this VBO</param>
        /// <param name="rows">Number of tile rows in this VBO</param>
        internal VBO(TileRenderer renderer, int columns, int rows)
        {
            Renderer = renderer;

            Handle = GL.GenBuffer();
            CreateNewBuffer(columns, rows);
        }

        /// <summary>
        /// (Internal) Creates a new buffer. Mostly used to resize the buffer. All data currently in the buffer will be lost.
        /// </summary>
        /// <param name="columns">Number of tile columns in this VBO</param>
        /// <param name="rows">Number of tile rows in this VBO</param>
        internal void CreateNewBuffer(int columns, int rows)
        {
            Columns = Math.Max(1, columns);
            Rows = Math.Max(1, rows);

            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            GL.BufferData(BufferTarget.ArrayBuffer, SIZE_OF_FLOAT * Length, new float[Length], BufferUsageHint.DynamicDraw);
        }

        /// <summary>
        /// (Internal) Updates the VBO data for a given tile.
        /// </summary>
        /// <param name="x">Tile X index in the VBO *AND* X position on the screen</param>
        /// <param name="y">Tile Y index in the VBO *AND* Y position on the screen</param>
        /// <param name="tile">Tile data</param>
        internal void UpdateTileData(int x, int y, Tile tile) { UpdateTileData(x, y, x, y, tile); }

        /// <summary>
        /// (Internal) Updates the VBO data for a given tile.
        /// </summary>
        /// <param name="x">Tile X INDEX (not position) in the VBO</param>
        /// <param name="y">Tile Y INDEX (not position) in the VBO</param>
        /// <param name="xPos">Tile X POSITION (not index) on the screen</param>
        /// <param name="yPos">Tile Y POSITION (not index) on the screen</param>
        /// <param name="tile">Tile data</param>
        internal void UpdateTileData(int x, int y, float xPos, float yPos, Tile tile)
        {
            if ((x < 0) || (y < 0) || (x >= Columns) || (y >= Rows)) return; // Tile index is out of bounds

            int index = y * Columns + x;

            int tileY = tile.TileIndex / Renderer.TilemapCount.Width;
            int tileX = tile.TileIndex - tileY * Renderer.TilemapCount.Width;

            float[] vertexData = new float[FLOATS_PER_VERTEX * 4];

            Color4 color4 = tile.Color.ToColor4();

            for (int i = 0; i < 4; i++)
                Array.Copy(
                    new float[]
                    {
                        xPos + TILE_CORNERS[i].X,
                        yPos + TILE_CORNERS[i].Y,
                        color4.R, color4.G, color4.B,
                        (tileX + TILE_CORNERS[i].X) * Renderer.TileUV.Width,
                        (tileY + TILE_CORNERS[i].Y) * Renderer.TileUV.Height,
                        tile.Tilemap
                    },
                    0, vertexData, FLOATS_PER_VERTEX * i, FLOATS_PER_VERTEX);

            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(index * BYTES_PER_TILE), BYTES_PER_TILE, vertexData);
        }

        /// <summary>
        /// (Internal) Draws the content of this VBO.
        /// </summary>
        internal void Render()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, BYTES_PER_VERTEX, 0); // x, y
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, BYTES_PER_VERTEX, 2 * SIZE_OF_FLOAT); // r, g, b
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, BYTES_PER_VERTEX, 5 * SIZE_OF_FLOAT); // u, v
            GL.VertexAttribPointer(3, 1, VertexAttribPointerType.Float, false, BYTES_PER_VERTEX, 7 * SIZE_OF_FLOAT); // tilemap
            GL.DrawArrays(PrimitiveType.Quads, 0, VertexCount);
        }

        /// <summary>
        /// IDisposable implementation. Clears the data and deletes the OpenGL buffer handle to free memory.
        /// </summary>
        public void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(Handle);
        }
    }
}

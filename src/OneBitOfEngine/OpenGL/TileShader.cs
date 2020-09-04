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

using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace OneBitOfEngine.OpenGL
{
    /// <summary>
    /// The OpenGL shader used to render the tiles.
    /// </summary>
    internal sealed class TileShader : IDisposable
    {
        /// <summary>
        /// OpenGL shader handle.
        /// </summary>
        private readonly int Handle;
        
        /// <summary>
        /// Handle of the Vertex Array Object used to render the tiles VBO.
        /// </summary>
        private readonly int VAO;

        /// <summary>
        /// Pointer to the "projection" Matrix4 uniform in the shader.
        /// </summary>
        private readonly int UniformProjection;

        /// <summary>
        /// Pointer to the "animationFrame" int uniform in the shader.
        /// </summary>
        private readonly int UniformAnimationFrame;

        /// <summary>
        /// Pointer to the "time" float uniform in the shader.
        /// </summary>
        private readonly int UniformTime;

        /// <summary>
        /// Pointers to the texture uniforms in the shader (one texture for each tilemap in TileScreen)
        /// </summary>
        private readonly int[] UniformTexture = new int[TileRenderer.TILEMAP_COUNT];

        /// <summary>
        /// Constructor.
        /// </summary>
        internal TileShader(SizeF tileUV)
        {
            int i;

            // Compile the vertex and fragment shaders and link the program
            int vertexShader, fragmentShader;
            vertexShader = CompileShader("TilesShader.vert", ShaderType.VertexShader);
            fragmentShader = CompileShader("TilesShader.frag", ShaderType.FragmentShader);
            
            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);

            // Delete shaders now that the program is linked
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Get uniform locations from the program
            UniformAnimationFrame = GL.GetUniformLocation(Handle, "animationFrame");
            UniformTime = GL.GetUniformLocation(Handle, "time");
            int uniformTileUVSize = GL.GetUniformLocation(Handle, "tileUVSize");
            UniformProjection = GL.GetUniformLocation(Handle, "projection");
            for (i = 0; i < TileRenderer.TILEMAP_COUNT; i++) UniformTexture[i] = GL.GetUniformLocation(Handle, $"texture{i}");

            // Set default uniforms values
            GL.UseProgram(Handle);
            for (i = 0; i < TileRenderer.TILEMAP_COUNT; i++) GL.Uniform1(UniformTexture[i], i);
            GL.Uniform1(UniformAnimationFrame, 0);
            GL.Uniform1(UniformTime, 0f);
            GL.Uniform2(uniformTileUVSize, new Vector2(tileUV.Width, tileUV.Height));

            // Create the vertex array object
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // Enable the 5 (xy, color, uv, tilemap, vfx) attributes each vertex will use
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.EnableVertexAttribArray(3);
            GL.EnableVertexAttribArray(4);
        }

        private int CompileShader(string shaderCodeResource, ShaderType shaderType)
        {
            int shader = GL.CreateShader(shaderType);

            GL.ShaderSource(shader, ReadShaderSourceCode($"OneBitOfEngine.Resources.Shaders.{shaderCodeResource}"));
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int compileStatus);

            if (compileStatus == 0) // Failed to compile
                throw new Exception($"Failed to compile {shaderType}\r\n{GL.GetShaderInfoLog(shader)}");

            return shader;
        }

        /// <summary>
        /// Sets the projection matrix the shader should use.
        /// </summary>
        /// <param name="projection">The projection matrix</param>
        internal void SetProjection(Matrix4 projection)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(UniformProjection, true, ref projection);
        }

        /// <summary>
        /// Reades shader code from an embedded UTF-8 text file.
        /// </summary>
        /// <param name="embeddedResourcePath">Path to the embedded text file</param>
        /// <returns>The shader code</returns>
        private static string ReadShaderSourceCode(string embeddedResourcePath)
        {
            string shaderCode;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResourcePath))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                { shaderCode = reader.ReadToEnd(); }
            }

            return shaderCode;
        }

        /// <summary>
        /// Sets the shader as the current shader.
        /// </summary>
        internal void Use()
        {
            GL.UseProgram(Handle);
        }

        /// <summary>
        /// Dispose of the shader and release all handles
        /// </summary>
        public void Dispose()
        {
            GL.UseProgram(0);
            GL.BindVertexArray(0);

            GL.DeleteProgram(Handle);
            GL.DeleteVertexArray(VAO);
        }

        /// <summary>
        /// Sets the current animation frame in the shader.
        /// </summary>
        /// <param name="frameIndex">Index of the current frame</param>
        internal void SetAnimationFrame(int frameIndex)
        {
            GL.Uniform1(UniformAnimationFrame, frameIndex);
        }

        /// <summary>
        /// Sets the total elapsed time in the shader.
        /// </summary>
        /// <param name="totalElapsedSeconds">total elapsed seconds</param>
        internal void SetTime(float totalElapsedSeconds)
        {
            GL.Uniform1(UniformTime, totalElapsedSeconds);
        }
    }
}

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

using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Asterion.Video
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
        /// Pointers to the texture uniforms in the shader (one texture for each tilemap in TileScreen)
        /// </summary>
        private readonly int[] UniformTexture = new int[TileManager.TILEMAP_COUNT];

        /// <summary>
        /// Constructor.
        /// </summary>
        internal TileShader()
        {
            int i;
            int vertexShader, fragmentShader;

            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertexShader, ReadShaderSourceCode("Asterion.Shaders.TilesShader.vert"));
            GL.CompileShader(vertexShader);

            GL.ShaderSource(fragmentShader, ReadShaderSourceCode("Asterion.Shaders.TilesShader.frag"));
            GL.CompileShader(fragmentShader);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);

            UniformProjection = GL.GetUniformLocation(Handle, "projection");
            
            for (i = 0; i < TileManager.TILEMAP_COUNT; i++)
                UniformTexture[i] = GL.GetUniformLocation(Handle, $"texture{i}");

            GL.UseProgram(Handle);
            for (i = 0; i < TileManager.TILEMAP_COUNT; i++)
                GL.Uniform1(UniformTexture[i], i);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.EnableVertexAttribArray(3);
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
    }
}

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

using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using OpenGLPixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using WindowsPixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Asterion.OpenGL
{
    /// <summary>
    /// (Internal) Stores .
    /// </summary>
    internal sealed class TilemapTexture : IDisposable
    {
        /// <summary>
        /// (Private) OpenGL texture handle.
        /// </summary>
        private readonly int Handle;

        /// <summary>
        /// (Internal) Is the texture valid?
        /// </summary>
        internal bool IsValid { get; } = false;

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        /// <param name="imageStream">A stream containing data for the System.Drawing.Image to use as a texture.</param>
        internal TilemapTexture(Stream imageStream)
        {
            IsValid = true;
            Handle = GL.GenTexture();

            try
            {
                if (imageStream == null)
                    LoadImage(null);
                else
                    using (Bitmap bitmap = new Bitmap(imageStream))
                    {
                        LoadImage(bitmap);
                    }
            }
            catch (Exception)
            {
                IsValid = false;
                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.DeleteTexture(Handle);
            }
        }

        /// <summary>
        /// (Private) Loads the texture into memory and binds the data to the OpenGL texture handle.
        /// </summary>
        /// <param name="bitmap">The bitmap to use as a texture</param>
        private void LoadImage(Bitmap bitmap)
        {
            GL.BindTexture(TextureTarget.Texture2D, Handle);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);

            if (bitmap == null)
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 0, 0, 0, OpenGLPixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            else
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, OpenGLPixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
                BitmapData bitmap_data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, WindowsPixelFormat.Format32bppArgb);
                GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, bitmap.Width, bitmap.Height, OpenGLPixelFormat.Bgra, PixelType.UnsignedByte, bitmap_data.Scan0);
                bitmap.UnlockBits(bitmap_data);
            }

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        /// <summary>
        /// (Internal) Binds the texture to the provided OpenGL texture index.
        /// </summary>
        /// <param name="textureIndex">OpenGL texture index to which the texture should be bound</param>
        internal void Use(int textureIndex)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + textureIndex);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        /// <summary>
        /// IDisposable implementation. Deletes the texture to free memory.
        /// </summary>
        public void Dispose()
        {
            if (!IsValid) return;

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.DeleteTexture(Handle);
        }
    }
}

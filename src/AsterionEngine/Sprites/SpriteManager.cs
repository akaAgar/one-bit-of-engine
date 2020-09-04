using Asterion.Core;
using Asterion.OpenGL;
using System;
using System.Collections.Generic;

namespace Asterion.Sprites
{
    /// <summary>
    /// A tile cursor (not the mouse cursor) which can be drawn on any tile.
    /// </summary>
    public sealed class SpriteManager
    {
        /// <summary>
        /// Area in which the sprites can be moved.
        /// </summary>
        public Area ViewPort { get { return ViewPort_; } set { ViewPort_ = value; UpdateVBO(); } }
        private Area ViewPort_ = Area.Zero;


        /// <summary>
        /// If true, new sprites will be added at the end of the stack and sprites will be drawn one after the other.
        /// If false, only one sprite will be drawn at a time, the next sprite will only begin its animation when the current one has completed its.
        /// </summary>
        public bool Synchronous { get; set; } = false;

        /// <summary>
        /// (Private) Sprite VBO
        /// </summary>
        private VBO SpriteVBO = null;

        /// <summary>
        /// (Private) List of sprites to draw.
        /// </summary>
        private List<Sprite> Sprites = new List<Sprite>();

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        internal SpriteManager() { }

        /// <summary>
        /// (Internal) Called before the first game loop. Creates the VBO.
        /// </summary>
        /// <param name="renderer">The tile renderer to use to draw the sprites VBO</param>
        internal void OnLoad(TileRenderer renderer)
        {
            SpriteVBO = new VBO(renderer, 0, 0);
            ViewPort_ = new Area(Position.Zero, renderer.TileCount);
        }

        /// <summary>
        /// (Internal) Called each frame. Draws the sprites, if any.
        /// </summary>
        internal void OnRenderFrame()
        {
            if (Sprites.Count == 0) return;
            SpriteVBO.Render();
        }

        /// <summary>
        /// (Private) Updates the VBO. Called each time the sprites moved or enter a new animation frame.
        /// </summary>
        private void UpdateVBO()
        {

        }

        /// <summary>
        /// (Internal) Destroys all sprites, the manager and the VBO.
        /// </summary>
        internal void Destroy()
        {
            SpriteVBO?.Dispose();
        }

        /// <summary>
        /// (Internal) Called on each update cycle.
        /// </summary>
        /// <param name="elapsedSeconds">Number of seconds elapsed since the last update.</param>
        internal void OnUpdate(float elapsedSeconds)
        {

        }
    }
}


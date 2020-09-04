using Asterion.Core;
using Asterion.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asterion.Sprites
{
    /// <summary>
    /// A tile cursor (not the mouse cursor) which can be drawn on any tile.
    /// </summary>
    public sealed class SpriteManager
    {
        public delegate void SpriteEvent(string name);

        public event SpriteEvent OnSpriteCreation = null;
        public event SpriteEvent OnSpriteDestruction = null;

        /// <summary>
        /// Area in which the sprites can be moved.
        /// </summary>
        public Area Viewport { get { return Viewport_; } set { Viewport_ = value; SetupNewFrame(); } }
        private Area Viewport_ = Area.Zero;

        public bool Active { get { return Sprites.Count > 0; } }

        /// <summary>
        /// (Private) Sprite VBO
        /// </summary>
        private VBO SpriteVBO = null;

        /// <summary>
        /// (Private) List of sprites to draw.
        /// </summary>
        private readonly List<Sprite> Sprites;

        private int CurrentFrame = 0;

        private float CurrentFrameTime = 0;

        public int SpriteStack { get { return Sprites.Count; } }

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        internal SpriteManager()
        {
            Sprites = new List<Sprite>();
        }

        public void AddMovingAnimation(string name, Position origin, Position target, float speed, int tile, RGBColor color, TileVFX vfx = TileVFX.None, int tilemap = 0)
        {
            Position[] trajectory = GetPointsBetween(origin, target);

            Sprites.Add(new Sprite(name, SpriteType.Moving, tile, 1, color, tilemap, 1f / Math.Max(1f, speed), trajectory));

            if (Sprites.Count == 1)
                BeginNewSprite();
        }

        public void AddStaticAnimation(string name, Position[] positions, int tile, RGBColor color, int frameCount = 1, float timePerFrame = .1f, int tilemap = 0)
        {
            Sprites.Add(new Sprite(name, SpriteType.Static, tile, frameCount, color, tilemap, timePerFrame, positions));

            if (Sprites.Count == 1)
                BeginNewSprite();
        }

        /// <summary>
        /// (Internal) Called before the first game loop. Creates the VBO.
        /// </summary>
        /// <param name="renderer">The tile renderer to use to draw the sprites VBO</param>
        internal void OnLoad(TileRenderer renderer)
        {
            SpriteVBO = new VBO(renderer, 0, 0);
            Viewport_ = new Area(Position.Zero, renderer.TileCount);
            BeginNewSprite();
        }

        /// <summary>
        /// (Internal) Called each frame. Draws the sprites, if any.
        /// </summary>
        internal void OnRenderFrame()
        {
            if (Sprites.Count == 0) return;
            SpriteVBO.Render();
        }

        private void BeginNewSprite()
        {
            CurrentFrame = 0;
            CurrentFrameTime = 0;

            if (Sprites.Count == 0)
            {
                SpriteVBO.CreateNewBuffer(0, 0);
                return;
            }

            OnSpriteCreation?.Invoke(Sprites[0].Name);

            switch (Sprites[0].SpriteType)
            {
                case SpriteType.Moving:
                    SpriteVBO.CreateNewBuffer(1, 1);
                    break;
                
                case SpriteType.Static:
                    SpriteVBO.CreateNewBuffer(Sprites[0].Positions.Length, 1);
                    break;
            }

            SetupNewFrame();
        }

        /// <summary>
        /// (Private) Updates the VBO. Called each time the sprites moved or enter a new animation frame.
        /// </summary>
        private void SetupNewFrame()
        {
            CurrentFrameTime = 0f;

            if (Sprites.Count == 0) return;

            switch (Sprites[0].SpriteType)
            {
                case SpriteType.Moving:
                    Position position = Sprites[0].Positions[CurrentFrame];
                    if (Viewport_.Contains(position))
                        SpriteVBO.UpdateTileData(0, 0, position.X, position.Y, Sprites[0].Tile, Sprites[0].Color);
                    else
                        SpriteVBO.UpdateTileData(0, 0, -100, -100, 0, RGBColor.Black);
                    return;

                case SpriteType.Static:
                    for (int i = 0; i < Sprites[0].Positions.Length; i++)
                    {
                        if (Viewport_.Contains(Sprites[0].Positions[i]))
                           SpriteVBO.UpdateTileData(i, 0, Sprites[0].Positions[i].X, Sprites[0].Positions[i].Y, Sprites[0].Tile + CurrentFrame, Sprites[0].Color);
                        else
                            SpriteVBO.UpdateTileData(i, 0, -100, -100, 0, RGBColor.Black);
                    }
                    return;
            }
        }

        private Position[] GetPointsBetween(Position start, Position end)
        {
            List<Position> positions = new List<Position>();

            float length = (float)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            float dX = (end.X - start.X) / length;
            float dY = (end.Y - start.Y) / length;

            positions.Add(start);
            for (float f = 0; f <= length; f += 1f)
                positions.Add(new Position((int)(start.X + dX * f), (int)(start.Y + dY * f)));
            positions.Add(end);

            return positions.Distinct().ToArray();
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
            if (Sprites.Count == 0) return;

            CurrentFrameTime += elapsedSeconds;
            if (CurrentFrameTime >= Sprites[0].FrameDuration)
            {
                CurrentFrame++;

                switch (Sprites[0].SpriteType)
                {
                    case SpriteType.Moving:
                        if (CurrentFrame >= Sprites[0].Positions.Length)
                            EndSprite();
                        else
                            SetupNewFrame();
                        return;

                    case SpriteType.Static:
                        if (CurrentFrame >= Sprites[0].TileCount)
                            EndSprite();
                        else
                            SetupNewFrame();
                        return;
                }
            }
        }

        private void EndSprite()
        {
            if (Sprites.Count == 0) return;

            OnSpriteDestruction?.Invoke(Sprites[0].Name);

            Sprites.RemoveAt(0);
            BeginNewSprite();
        }
    }
}


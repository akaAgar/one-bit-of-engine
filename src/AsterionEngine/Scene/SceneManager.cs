using Asterion.Video;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Asterion.Scene
{
    public sealed class SceneManager
    {
        private List<Entity> Entities = new List<Entity>();

        public Map Map { get; private set; }

        public Rectangle Viewport { get { return _Viewport; } set { _Viewport = value; if (Tiles != null) RecreateVBO(); } }
        private Rectangle _Viewport = Rectangle.Empty;

        public bool Visible { get; set; } = true;
        public bool Created { get; private set; } = false;

        public Point CameraPosition { get; set; } = Point.Empty;

        private Size GameWindowTileCount = Size.Empty;

        private VBO Tiles = null;

        private readonly AsterionGame Game = null;

        private bool VBOUpdateRequired = false;

        internal SceneManager(AsterionGame game)
        {
            Game = game;
            Map = new Map(this);
            
            GameWindowTileCount = new Size(game.Tiles.TileCountX, game.Tiles.TileCountY);

            Viewport = new Rectangle(0, 0, GameWindowTileCount.Width, GameWindowTileCount.Height);
        }

        public bool IsCellFree(Point cell, int layer = 0) { return IsCellFree(cell.X, cell.Y, layer); }

        public bool IsCellOnMap(Point cell) { return IsCellOnMap(cell.X, cell.Y); }
        public bool IsCellOnMap(int x, int y)
        {
            return !((x < 0) || (y < 0) || (x >= Map.Width) || (y >= Map.Height));
        }

        public bool IsCellFree(int x, int y, int layer = 0)
        {
            if (!IsCellOnMap(x, y)) return false;
            if (Map[x, y].Value.BlocksMovement) return false;

            return true;
        }

        public T AddEntity<T>(Point position, params object[] parameters)  where T: Entity, new()
        {
            if (!Created) return null;
            
            T newEntity = new T();
            if (!newEntity.Initialize(this, position, parameters)) return null;
            Entities.Add(newEntity);

            RequireVBOUpdate();
            return newEntity;
        }

        public void Create(int width, int height)
        {
            Destroy();

            Map.Create(width, height);

            CameraPosition = Point.Empty;
            Created = true;
        }

        public void Destroy()
        {
            if (!Created) return;

            Map.Destroy();
            Entities.Clear();

            Created = false;
        }

        internal void OnRenderFrame()
        {
            if (!Created || !Visible) return;

            GL.Disable(EnableCap.Blend);
            Tiles.Render();
        }

        internal void Dispose()
        {
            Destroy();
            Tiles.Dispose();
        }

        internal void OnLoad()
        {
            Tiles = new VBO(Game.Tiles, 0);
            RecreateVBO();
        }

        private void RecreateVBO()
        {
            Tiles.CreateNewBuffer(_Viewport.Width * _Viewport.Height);
            UpdateVBO();
        }

        public Entity[] GetEntitiesInCell(Point cell) { return GetEntitiesInCell(cell.X, cell.Y); }
        public Entity[] GetEntitiesInCell(int x, int y)
        {
            return (from Entity e in Entities where e.Position == new Point(x, y) select e).ToArray();
        }

        public Entity GetEntityInCell(Point cell, int layer) { return GetEntityInCell(cell.X, cell.Y, layer); }
        public Entity GetEntityInCell(int x, int y, int layer)
        {
            return (from Entity e in Entities where e.Position == new Point(x, y) && e.Layer == layer select e).SingleOrDefault();
        }

        private void UpdateVBO(params Point[] cellsToUpdate)
        {
            if (cellsToUpdate.Length == 0) // no array of cells to update provided, update everything
            {
                Entities = Entities.OrderBy(e => e.Layer).ToList();

                int x, y;

                for (x = 0; x < _Viewport.Width; x++)
                    for (y = 0; y < _Viewport.Height; y++)
                    {
                        Point? pt = GetScenePointFromTile(x + _Viewport.X, y + _Viewport.Y);
                        if (!pt.HasValue)
                        {
                            Tiles.UpdateTileData(y * _Viewport.Width + x, _Viewport.X + x, _Viewport.Y + y, Map.DefaultTile);
                            continue;
                        }

                        Entity[] e = GetEntitiesInCell(pt.Value);

                        if (e.Length > 0)
                            Tiles.UpdateTileData(y * _Viewport.Width + x, _Viewport.X + x, _Viewport.Y + y, e[0].Tile);
                        else
                            Tiles.UpdateTileData(y * _Viewport.Width + x, _Viewport.X + x, _Viewport.Y + y, Map[pt.Value.X, pt.Value.Y].Value.Tile);

                        //UpdateVBOCell(x, y);
                        // TODO: entities



                        //Tiles.DrawTile(x, y, 1, Color.Yellow);
                        //Tiles.UpdateTileData(y * _Viewport.Width + x, _Viewport.X + x, _Viewport.Y + y, 1, Color.Yellow, 0);
                    }
            }
        }

        internal void RequireVBOUpdate() { VBOUpdateRequired = true; }

        internal void OnUpdate(float elapsedSeconds)
        {
            if (!Created || !Visible || !VBOUpdateRequired) return;

            UpdateVBO();
            VBOUpdateRequired = false;
        }

        //public void UpdateVBOCell(Point scenePoint) { return UpdateVBOCell(scenePoint.X, scenePoint.Y); }
        //private void UpdateVBOCell(int sceneX, int sceneY)
        //{
        //    if (!Map.IsPointOnMap(sceneX, sceneY)) return;
        //}

        public Point? GetScenePointFromTile(Point cell) { return GetScenePointFromTile(cell.X, cell.Y); }
        public Point? GetScenePointFromTile(int x, int y)
        {
            if (!Created || !Visible || !IsTileInViewport(x, y)) return null;

            int sX = x - _Viewport.X - CameraPosition.X;
            int sY = y - _Viewport.Y - CameraPosition.Y;

            Point scenePoint = new Point(sX, sY);

            if (!IsCellOnMap(scenePoint)) return null;

            return scenePoint;
        }

        public bool IsTileInViewport(Point cell) { return IsTileInViewport(cell.X, cell.Y); }
        public bool IsTileInViewport(int x, int y) { return _Viewport.Contains(x, y); }
    }
}

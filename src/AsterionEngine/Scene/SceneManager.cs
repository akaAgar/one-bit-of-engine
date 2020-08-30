using Asterion.Core;
using Asterion.OpenGL;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Asterion.Scene
{
    public sealed class SceneManager
    {
        private List<Entity> Entities = new List<Entity>();

        public Map Map { get; private set; }

        public Area Viewport { get { return _Viewport; } set { _Viewport = value; if (Tiles != null) RecreateVBO(); } }
        private Area _Viewport = Area.Zero;

        public bool Visible { get; set; } = true;
        public bool Created { get; private set; } = false;

        public Position CameraPosition { get; set; } = Position.Zero;

        private Dimension GameWindowTileCount = Dimension.Zero;

        private VBO Tiles = null;

        private readonly AsterionGame Game = null;

        private bool VBOUpdateRequired = false;

        internal SceneManager(AsterionGame game)
        {
            Game = game;
            Map = new Map(this);
            
            GameWindowTileCount = new Dimension(game.Renderer.TileCount.Width, game.Renderer.TileCount.Height);

            Viewport = new Area(0, 0, GameWindowTileCount.Width, GameWindowTileCount.Height);
        }

        public bool IsCellFree(Position cell, int layer = 0) { return IsCellFree(cell.X, cell.Y, layer); }

        public bool IsCellOnMap(Position cell) { return IsCellOnMap(cell.X, cell.Y); }
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

        public T AddEntity<T>(Position position, params object[] parameters)  where T: Entity, new()
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

            CameraPosition = Position.Zero;
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
            Tiles = new VBO(Game.Renderer, 0, 0);
            RecreateVBO();
        }

        private void RecreateVBO()
        {
            Tiles.CreateNewBuffer(_Viewport.Width, _Viewport.Height);
            UpdateVBO();
        }

        public Entity[] GetEntitiesInCell(Position cell) { return GetEntitiesInCell(cell.X, cell.Y); }
        public Entity[] GetEntitiesInCell(int x, int y)
        {
            return (from Entity e in Entities where e.Position == new Position(x, y) select e).ToArray();
        }

        public Entity GetEntityInCell(Position cell, int layer) { return GetEntityInCell(cell.X, cell.Y, layer); }
        public Entity GetEntityInCell(int x, int y, int layer)
        {
            return (from Entity e in Entities where e.Position == new Position(x, y) && e.Layer == layer select e).SingleOrDefault();
        }

        private void UpdateVBO(params Position[] cellsToUpdate)
        {
            if (cellsToUpdate.Length == 0) // no array of cells to update provided, update everything
            {
                Entities = Entities.OrderBy(e => e.Layer).ToList();

                int x, y;

                for (x = 0; x < _Viewport.Width; x++)
                    for (y = 0; y < _Viewport.Height; y++)
                    {
                        Position? pt = GetScenePointFromTile(x + _Viewport.X, y + _Viewport.Y);
                        if (!pt.HasValue)
                        {
                            Tiles.UpdateTileData(x, y, _Viewport.X + x, _Viewport.Y + y, Map.DefaultTile);
                            continue;
                        }

                        Entity[] e = GetEntitiesInCell(pt.Value);

                        if (e.Length > 0)
                            Tiles.UpdateTileData(x, y, _Viewport.X + x, _Viewport.Y + y, e[0].Tile);
                        else
                            Tiles.UpdateTileData(x, y, _Viewport.X + x, _Viewport.Y + y, Map[pt.Value.X, pt.Value.Y].Value.Tile);

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

        public Position? GetScenePointFromTile(Position cell) { return GetScenePointFromTile(cell.X, cell.Y); }
        public Position? GetScenePointFromTile(int x, int y)
        {
            if (!Created || !Visible || !IsTileInViewport(x, y)) return null;

            int sX = x - _Viewport.X - CameraPosition.X;
            int sY = y - _Viewport.Y - CameraPosition.Y;

            Position scenePoint = new Position(sX, sY);

            if (!IsCellOnMap(scenePoint)) return null;

            return scenePoint;
        }

        public bool IsTileInViewport(Position cell) { return IsTileInViewport(cell.X, cell.Y); }
        public bool IsTileInViewport(int x, int y) { return _Viewport.Contains(x, y); }
    }
}

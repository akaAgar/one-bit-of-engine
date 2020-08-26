using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asterion.Video;

namespace Asterion.Menus
{
    public sealed class MenuManager
    {
        private MenuPage Page = null;

        private VBO TilesVBO;

        public bool InMenu { get { return Page != null; } }

        public AsterionGame Game { get; private set; }

        internal MenuManager(AsterionGame game)
        {
            Game = game;
        }

        internal void OnLoad()
        {
            TilesVBO = new VBO(Game.Tiles, Game.Tiles.TileCountX * Game.Tiles.TileCountY);
        }

        public void ShowPage<T>(params object[] parameters) where T : MenuPage, new()
        {
            ClosePage();
            Page = new T();
            Page.Initialize(this, parameters);
        }

        public void ClosePage()
        {
            if (Page == null) return;
            Page.Dispose();
            Page = null;
        }

        internal void Dispose()
        {
            ClosePage();
        }

        public void DrawTile(Point pt, Tile tile) { DrawTile(pt.X, pt.Y, tile); }
        public void DrawTile(int x, int y, Tile tile)
        {
            TilesVBO.UpdateTileData(y * Game.Tiles.TileCountX + x, x, y, tile);
        }

        internal void Render()
        {
            if (!InMenu) return;

            //Page.Render();
            Page.OnRender();
        }
    }
}

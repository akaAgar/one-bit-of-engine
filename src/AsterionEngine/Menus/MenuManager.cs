using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asterion.Video;
using OpenTK.Graphics.ES30;

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
            TilesVBO = new VBO(Game.Tiles, Game.Tiles.TileCountX, Game.Tiles.TileCountY);
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

        internal void Render()
        {
            if (!InMenu) return;

            TilesVBO.Render();
        }

        internal void UpdateTiles()
        {
            ClearTiles();

            if (InMenu)
                Page.SetTiles(TilesVBO);
        }

        private void ClearTiles()
        {
            int x, y;

            for (x = 0; x < Game.Tiles.TileCountX; x++)
                for (y = 0; y < Game.Tiles.TileCountY; y++)
                    TilesVBO.UpdateTileData(x, y, new Tile(0, RGBColor.Black));
        }
    }
}

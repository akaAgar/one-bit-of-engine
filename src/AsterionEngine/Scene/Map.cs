using Asterion.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asterion.Scene
{
    public sealed class Map
    {
        private MapCell[,] Cells = new MapCell[0, 0];

        public Tile DefaultTile { get; set; } = new Tile(0, RGBColor.Black);

        public int Width { get { return Cells.GetLength(0); } }
        public int Height { get { return Cells.GetLength(1); } }

        private readonly SceneManager Scene;

        internal Map(SceneManager scene)
        {
            Scene = scene;
        }

        public MapCell? this[int x, int y]
        {
            get { return GetCell(x, y); }
            set { if (value.HasValue) SetCell(x, y, value.Value); }
        }

        public MapCell? GetCell(int x, int y)
        {
            if (!Scene.IsCellOnMap(x, y)) return null;
            return Cells[x, y];
        }

        public bool SetCell(int x, int y, MapCell cell)
        {
            if (!Scene.IsCellOnMap(x, y)) return false;
            Cells[x, y] = cell;
            return true;
        }

        internal void Create(int width, int height)
        {
            width = Math.Max(1, width);
            height = Math.Max(1, height);

            Cells = new MapCell[width, height];
            Clear(new MapCell(new Tile(0, RGBColor.Black)));
        }

        public void Clear(MapCell cell)
        {
            int x, y;

            for (x= 0; x < Width; x++)
            {
                for (y = 0; y < Height; y++)
                    Cells[x, y] = cell;
            }
        }

        internal void Destroy()
        {
            Cells = new MapCell[0, 0];
        }
    }
}

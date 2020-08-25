using System.Drawing;

namespace Asterion.Video
{
    public struct Tile
    {
        public readonly int TileIndex;
        public readonly Color Color;
        public readonly int Tilemap;
        public readonly bool Animated;

        public Tile(int tileIndex, Color color, int tileMap = 0, bool animated = false)
        {
            TileIndex = tileIndex;
            Color = color;
            Tilemap = tileMap;
            Animated = animated;
        }

        public Tile(int tileIndex, Color color, bool animated)
        {
            TileIndex = tileIndex;
            Color = color;
            Tilemap = 0;
            Animated = animated;
        }
    }
}

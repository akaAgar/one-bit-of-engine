using Asterion.Video;

namespace Asterion.Scene
{
    public struct MapCell
    {
        public readonly Tile Tile;
        public readonly bool BlocksMovement;
        public readonly bool BlocksView;

        public MapCell(Tile tile, bool blocksMovement = false, bool blocksView = false)
        {
            Tile = tile;
            BlocksMovement = blocksMovement;
            BlocksView = blocksView;
        }
    }
}

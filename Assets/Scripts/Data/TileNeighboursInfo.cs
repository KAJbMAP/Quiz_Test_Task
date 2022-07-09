using Enums;
using Interfaces.Data;

namespace Data
{
    public class TileChildsInfo : ITileChildsInfo
    {
        public static ITileChildsInfo Empty => new TileChildsInfo(0, TileChilds.None);
        public float Distance { get; }
        public TileChilds TileChilds { get; }

        public TileChildsInfo(float distance, TileChilds tileChilds)
        {
            Distance = distance;
            TileChilds = tileChilds;
        }
    }
}
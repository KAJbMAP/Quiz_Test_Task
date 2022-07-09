using Data;
using Enums;
using Interfaces.Data;

namespace Zenject.Factories
{
    public class MapTileChildsInfoFactory : PlaceholderFactory<float, TileChilds, ITileChildsInfo>
    {
        
    }

    public class CustomMapTileChildsInfoFactory : IFactory<float, TileChilds, ITileChildsInfo>
    {
        public ITileChildsInfo Create(float distance, TileChilds tileChilds)
        {
            return new TileChildsInfo(distance, tileChilds);
        }
    }
}
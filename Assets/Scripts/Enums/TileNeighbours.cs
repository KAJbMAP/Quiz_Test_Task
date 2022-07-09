using System;

namespace Enums
{
    [Flags]
    public enum TileChilds
    {
        None = 0,
        Left = 1 << 0,
        Top = 1 << 1,
        Right = 1 << 2,
        Bottom = 1 << 3
    }
}
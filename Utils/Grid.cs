using System.Numerics;
using The_Snake.Core;

namespace The_Snake.Utils;

public static class Grid
{
    public static Vector2 ToPixelPosition(Vector2 gridPos)
    {
        return new Vector2(gridPos.X * GameConfig.CellSize, gridPos.Y * GameConfig.CellSize);
    }

    public static bool IsInsideGrid(Vector2 pos)
    {
        return !(!(pos.X >= 0) || !(pos.X < GameConfig.GridWidth) ||
                 !(pos.Y >= 0) || !(pos.Y < GameConfig.GridHeight));
    }
}
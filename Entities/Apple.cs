using System.Numerics;
using Raylib_cs;
using The_Snake.Core;
using The_Snake.Utils;

namespace The_Snake.Entities;

public class Apple
{
    public Vector2 Position;
    private Random _random = new();

    public Apple()
    {
        Respawn();
    }

    public void Respawn()
    {
        Position = new Vector2(
            _random.Next(0, GameConfig.GridWidth),
            _random.Next(0, GameConfig.GridHeight)
            );
    }

    public void Draw()
    {
        var pos = Grid.ToPixelPosition(Position);
        Raylib.DrawRectangle((int)pos.X, (int)pos.Y, GameConfig.CellSize, GameConfig.CellSize, Color.Red);
    }
}
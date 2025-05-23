using System.Numerics;
using Raylib_cs;
using The_Snake.Core;

namespace The_Snake.Gameplay;

public class Apple
{
    public Vector2 Position;
    private Random random = new();

    public Apple()
    {
        Respawn();
    }

    public void Respawn()
    {
        Position = new Vector2(
            random.Next(0, GameConfig.GridWidth),
            random.Next(0, GameConfig.GridHeight)
            );
    }

    public void Draw()
    {
        var pos = Grid.ToPixelPosition(Position);
        Raylib.DrawRectangle((int)pos.X, (int)pos.Y, GameConfig.CellSize, GameConfig.CellSize, Color.Red);
    }
}
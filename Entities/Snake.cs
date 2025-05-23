using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using The_Snake.Core;

namespace The_Snake.Gameplay;

public class Snake
{
    public List<Vector2> Body = new();
    private Vector2 direction = new(1, 0);
    private float moveTimer = 0f;
    private float moveInterval = 0.15f;

    public Snake()
    {
        Body.Add(new Vector2(10, 10));
    }

    public void Update()
    {
        moveTimer += Raylib.GetFrameTime();
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0f;
            
            var newHead = Body[0] + direction;
            Body.Insert(0,newHead);
            Body.RemoveAt(Body.Count - 1);
        }
        
        if (Raylib.IsKeyPressed(KeyboardKey.Up) && direction.Y != 1)
            direction = new Vector2(0, -1);
        if (Raylib.IsKeyPressed(KeyboardKey.Down) && direction.Y != -1)
            direction = new Vector2(0, 1);
        if (Raylib.IsKeyPressed(KeyboardKey.Left) && direction.X != 1)
            direction = new Vector2(-1, 0);
        if (Raylib.IsKeyPressed(KeyboardKey.Right) && direction.X != -1)
            direction = new Vector2(1, 0);
    }
    
    public void Grow()
    {
        Body.Add(Body[^1]);
    }

    public bool IsHeadOn(Vector2 pos)
    {
        return Body[0] == pos;
    }

    public void Draw()
    {
        foreach (var segment in Body)
        {
            var pos = Grid.ToPixelPosition(segment);
            Raylib.DrawRectangle((int)pos.X, (int)pos.Y, GameConfig.CellSize, GameConfig.CellSize, Color.Green);
        }
    }
}
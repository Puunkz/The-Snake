using System.Numerics;
using Raylib_cs;
using The_Snake.Core;
using The_Snake.Utils;

namespace The_Snake.Entities;

public class Snake
{
    private List<Vector2> _body = new();
    private Vector2 _direction = new(1, 0);
    private float _moveTimer = 0f;
    private float _moveInterval = 0.15f;

    public Snake()
    {
        _body.Add(new Vector2(10, 10));
    }

    public void Update()
    {
        _moveTimer += Raylib.GetFrameTime();
        if (_moveTimer >= _moveInterval)
        {
            _moveTimer = 0f;
            
            var newHead = _body[0] + _direction;
            _body.Insert(0,newHead);
            _body.RemoveAt(_body.Count - 1);
        }
        
        if (Raylib.IsKeyPressed(KeyboardKey.Up) && _direction.Y != 1)
            _direction = new Vector2(0, -1);
        if (Raylib.IsKeyPressed(KeyboardKey.Down) && _direction.Y != -1)
            _direction = new Vector2(0, 1);
        if (Raylib.IsKeyPressed(KeyboardKey.Left) && _direction.X != 1)
            _direction = new Vector2(-1, 0);
        if (Raylib.IsKeyPressed(KeyboardKey.Right) && _direction.X != -1)
            _direction = new Vector2(1, 0);
    }
    
    public void Grow()
    {
        _body.Add(_body[^1]);
    }

    public bool IsHeadOn(Vector2 pos)
    {
        return _body[0] == pos;
    }

    public void Draw()
    {
        foreach (var segment in _body)
        {
            var pos = Grid.ToPixelPosition(segment);
            Raylib.DrawRectangle((int)pos.X, (int)pos.Y, GameConfig.CellSize, GameConfig.CellSize, Color.Green);
        }
    }
}
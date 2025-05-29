using System.Numerics;
using Raylib_cs;

namespace TheSnake.Input
{
    public class KeyboardInputService : IInputService
    {
        public Vector2 GetDirection()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP)) return new Vector2(0, -1);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) return new Vector2(0, 1);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) return new Vector2(-1, 0);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) return new Vector2(1, 0);
            return Vector2.Zero;
        }
    }
}
using Raylib_cs;

namespace The_Snake.Services;

public class InputService : IInputService
{
    public bool IsPausePressed() => Raylib.IsKeyPressed(KeyboardKey.Escape);
    public bool IsStartPressed() => Raylib.IsKeyPressed(KeyboardKey.Enter);
}
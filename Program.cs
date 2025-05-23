using Raylib_cs;
using The_Snake.Core;
using The_Snake.Scenes;
using The_Snake.Services;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Snake Game");
        Raylib.SetTargetFPS(60);
        
        ServiceLocator.Register<IInputService>(new InputService());

        SceneManager.ChangeScene(new MenuScene());

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGreen);
            
            SceneManager.Update();
            SceneManager.Draw();
            
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
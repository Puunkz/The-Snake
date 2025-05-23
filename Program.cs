using Raylib_cs;
using The_Snake.Core;
using The_Snake.Scenes;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(1920, 1080, "Snake Game");
        Raylib.SetTargetFPS(60);

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
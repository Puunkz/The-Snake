using Raylib_cs;
using TheSnake.Scenes;
using TheSnake.Input;
using TheSnake.Core;

namespace TheSnake
{
    static class Program
    {
        static void Main()
        {
            Raylib.InitWindow(800, 600, "TheSnake");
            Raylib.SetTargetFPS(60);
            
            ServiceLocator.Register<IInputService>(new KeyboardInputService());
            
            SceneManager.ChangeScene(new MenuScene());

            while (!Raylib.WindowShouldClose())
            {
                float dt = Raylib.GetFrameTime();
                SceneManager.Update(dt);
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);
                SceneManager.Draw();
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}
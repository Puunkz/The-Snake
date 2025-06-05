using Raylib_cs;
using TheSnake.Scenes;
using TheSnake.Input;
using TheSnake.Core;
using System.Numerics;

namespace TheSnake
{
    static class Program
    {
        private const int virtualWidth = 800;
        private const int virtualHeight = 600;
        private static RenderTexture2D _target;
        static void Main()
        {
            Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.InitWindow(800, 600, "TheSnake");
            Raylib.SetTargetFPS(60);
            
            _target = Raylib.LoadRenderTexture(virtualWidth, virtualHeight);
            ServiceLocator.Register<IInputService>(new KeyboardInputService());
            SceneManager.ChangeScene(new MenuScene());

            while (!Raylib.WindowShouldClose()) // Main game loop
            {
                float dt = Raylib.GetFrameTime();
                SceneManager.Update(dt);
                
                Raylib.BeginTextureMode(_target);
                Raylib.ClearBackground(Color.DARKGRAY);
                SceneManager.Draw();
                Raylib.EndTextureMode();
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                float scale = Math.Min(
                    (float)Raylib.GetScreenWidth() / virtualWidth,
                    (float)Raylib.GetScreenHeight() / virtualHeight
                );
                
                int scaledWidth = (int)(virtualWidth * scale);
                int scaledHeight = (int)(virtualHeight * scale);
                int offsetX = (Raylib.GetScreenWidth() - scaledWidth) / 2;
                int offsetY = (Raylib.GetScreenHeight() - scaledHeight) / 2;
                
                Raylib.DrawTexturePro(
                    _target.texture,
                    new Rectangle(0, 0, virtualWidth, -virtualHeight),
                    new Rectangle(offsetX, offsetY, scaledWidth, scaledHeight),
                    new Vector2(0, 0),
                    0f,
                    Color.WHITE
                );
                Raylib.EndDrawing();
            }
            Raylib.UnloadRenderTexture(_target);
            Raylib.CloseWindow();
        }
    }
}
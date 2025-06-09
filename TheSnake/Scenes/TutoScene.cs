using Raylib_cs;
using TheSnake.Core;

namespace TheSnake.Scenes
{
    public class TutoScene : IScene
    {
        public void Load()
        {
            
        }

        public void Update(float deltaTime)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                SceneManager.ChangeScene(new GameScene());
            }
        }

        public void Draw()
        {
            Raylib.ClearBackground(Color.DARKGRAY);
            
            Raylib.DrawText("Tutoriel", 300, 30, 40, Color.YELLOW);
            
            Raylib.DrawRectangle(100, 100, 20, 20, Color.GREEN);
            Raylib.DrawRectangleLines(100, 100, 20, 20, Color.BLACK);
            Raylib.DrawText("Serpent", 130, 100, 20, Color.WHITE);

            Raylib.DrawRectangle(100, 150, 20, 20, Color.RED);
            Raylib.DrawRectangleLines(100, 150, 20, 20, Color.BROWN);
            Raylib.DrawText("Fruit normal (+1 pt)", 130, 150, 20, Color.WHITE);

            Raylib.DrawRectangle(100, 200, 20, 20, Color.GOLD);
            Raylib.DrawRectangleLines(100, 200, 20, 20, Color.ORANGE);
            Raylib.DrawText("Fruit spécial (+5 pts + Score X2 pendant 10s)", 130, 200, 20, Color.WHITE);

            Raylib.DrawRectangle(100, 250, 20, 20, Color.BLUE);
            Raylib.DrawRectangleLines(100, 250, 20, 20, Color.DARKBLUE);
            Raylib.DrawText("Obstacle (évite-les !)", 130, 250, 20, Color.WHITE);

            Raylib.DrawText("Appuie sur ESPACE pour commencer", 200, 400, 25, Color.LIGHTGRAY);
        }
        public void Unload(){}
    }
}
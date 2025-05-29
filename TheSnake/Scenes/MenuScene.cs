using Raylib_cs;

namespace TheSnake.Scenes
{
    public class MenuScene : IScene
    {
        public void Load()
        {
            
        }

        public void Update(float deltaTime)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                SceneManager.ChangeScene(new GameScene());
            }
        }
        
        public void Draw()
        {
            Raylib.DrawText("Welcome to The Snake!", 200, 200, 40, Color.WHITE);
            Raylib.DrawText("Press Enter to Start", 200, 300, 20, Color.LIGHTGRAY);
        }
        
        public void Unload()
        {
        }
    }
}
using Raylib_cs;

namespace TheSnake.Scenes
{
    public class MenuScene : IScene
    {
        public void load()
        {
            
        }

        public void update(float deltaTime)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                SceneManager.ChangeScene(new GameScene());
            }
        }
        
        public void draw()
        {
            Raylib.DrawText("Welcome to The Snake!", 350, 200, 40, Color.WHITE);
            Raylib.DrawText("Press Enter to Start", 240, 300, 20, Color.LIGHTGRAY);
        }
        
        public void unload()
        {
        }
    }
}
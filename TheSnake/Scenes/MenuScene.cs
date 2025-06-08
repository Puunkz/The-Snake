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
                SceneManager.ChangeScene(new GameScene());
        }

        public void Draw()
        {
            Raylib.DrawText("SNAKE GAME", 250, 150, 50, Color.LIME);
            Raylib.DrawText("Appuie sur ENTER pour commencer", 200, 250, 25, Color.WHITE);
        }

        public void Unload() {}
    }
}
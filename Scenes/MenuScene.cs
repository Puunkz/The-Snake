using Raylib_cs;
using The_Snake.Core;

namespace The_Snake.Scenes;

public class MenuScene : IScene
{
    public void Load()
    {
        // Load resources for the menu scene here
    }

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            SceneManager.ChangeScene(new GameScene());
        }
    }

    public void Draw()
    {
        Raylib.DrawText("SNAKE - MENU", 300, 300, 30, Color.White);
        Raylib.DrawText("Press Enter to start", 200, 250, 20, Color.LightGray);
    }
    public void Unload()
    {
        // Unload resources for the menu scene here
    }
}
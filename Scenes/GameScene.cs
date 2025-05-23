using Raylib_cs;
using The_Snake.Scenes;
using The_Snake.Core;

public class GameScene : IScene
{
    public void Load()
    {
        // Load resources for the game scene here
    }

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            SceneManager.ChangeScene(new PauseScene());
        }
    }

    public void Draw()
    {
        Raylib.DrawText("Jeu en cours...", 10, 10, 20, Color.White);
    }
    public void Unload()
    {
        // Unload resources for the game scene here
    }
}
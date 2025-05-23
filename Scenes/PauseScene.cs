using Raylib_cs;
using The_Snake.Core;
using The_Snake.Scenes;

namespace The_Snake.Scenes;

public class PauseScene : IScene
{
    public void Load()
    {
        
    }

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.P))
        {
            SceneManager.ChangeScene(new GameScene());
        }
    }
    
    public void Draw()
    {
        Raylib.DrawText("PAUSE", 350, 250, 30, Color.Yellow);
        Raylib.DrawText("Press P to resume", 250, 300, 20, Color.LightGray);
    }
    
    public void Unload()
    {
        
    }
}
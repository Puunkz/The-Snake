using Raylib_cs;
using The_Snake.Entities;

namespace The_Snake.Scenes;
public class GameScene : IScene
{
    private Snake _snake = new ();
    private Apple _apple = new ();
    
    public void Load()
    {
        _snake = new Snake();
        _apple = new Apple();
    }

    public void Update()
    {
        _snake.Update();

        if (_snake.IsHeadOn(_apple.Position))
        {
            _snake.Grow();
            _apple.Respawn();
        }
        
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            
        }
    }

    public void Draw()
    {
        _snake.Draw();
        _apple.Draw();
    }
    public void Unload()
    {
        // Unload resources for the game scene here
    }
}
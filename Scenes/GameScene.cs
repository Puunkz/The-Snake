using Raylib_cs;
using The_Snake.Gameplay;
using System.Numerics;

namespace The_Snake.Scenes;
public class GameScene : IScene
{
    private Snake snake = new ();
    private Apple apple = new ();
    
    public void Load()
    {
        snake = new Snake();
        apple = new Apple();
    }

    public void Update()
    {
        snake.Update();

        if (snake.IsHeadOn(apple.Position))
        {
            snake.Grow();
            apple.Respawn();
        }
        
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            
        }
    }

    public void Draw()
    {
        snake.Draw();
        apple.Draw();
    }
    public void Unload()
    {
        // Unload resources for the game scene here
    }
}
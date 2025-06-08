using Raylib_cs;
using TheSnake.Core;

namespace TheSnake.Scenes
{
    public class PauseScene : IScene
    {
        public void Load()
        {
            
        }

        public void Update(float deltaTIme)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P)) //repprendre le jeu
                SceneManager.PopScene();
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R)) //recommencer le jeu
                SceneManager.ChangeScene(new GameScene());
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_A)) //quitter le jeu -- A au lieu de Q pour le francais 
                Raylib.CloseWindow();
        }

        public void Draw()
        {
            SceneManager.DrawUnderlyingScenes(); // Draw underlying scenes if any
            Raylib.DrawRectangle(0, 0, 800, 600, new Color(0, 0, 0, 150)); // semi-transparent
            Raylib.DrawText("PAUSE", 300, 150, 50, Color.YELLOW);
            Raylib.DrawText("Appuie sur P pour reprendre le jeu", 200, 250, 25, Color.WHITE);
            Raylib.DrawText("Appuie sur R pour recommencer le jeu", 200, 300, 25, Color.WHITE);
            Raylib.DrawText("Appuie sur Q pour quitter le jeu", 200, 350, 25, Color.WHITE);
        }

        public void Unload()
        {
            
        }
    }
}
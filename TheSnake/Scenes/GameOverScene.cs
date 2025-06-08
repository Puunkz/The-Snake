using Raylib_cs;
using TheSnake.Core;

namespace TheSnake.Scenes
{
    public class GameOverScene(int score) : IScene
    {
        private int _highscore;

        public void Load()
        {
            _highscore = HighscoreManager.LoadHighscore();

            if (score > _highscore)
            {
                _highscore = score;
                HighscoreManager.SaveHighscore(_highscore);
            }
        }

        public void Update(float deltaTime)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                SceneManager.ChangeScene(new GameScene());
        }

        public void Draw()
        {
            Raylib.DrawText("GAME OVER", 250, 150, 50, Color.RED);
            Raylib.DrawText($"Score: {score}", 320, 230, 30, Color.WHITE);
            Raylib.DrawText($"Highscore: {_highscore}", 300, 270, 25, Color.YELLOW);
            Raylib.DrawText("Appuie sur ENTER pour recommencer", 200, 350, 20, Color.GRAY);
        }

        public void Unload() { }
    }
}
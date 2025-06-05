using Raylib_cs;
using TheSnake.Core;

namespace TheSnake.Scenes
{
    public class GameOverScene : IScene
    {
        private int _score;
        private int _highscore;

        public GameOverScene(int score)
        {
            _score = score;
        }

        public void Load()
        {
            _highscore = HighscoreManager.LoadHighscore();
        }

        public void Update(float deltaTime)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                SceneManager.ChangeScene(new GameScene());
        }

        public void Draw()
        {
            Raylib.DrawText("GAME OVER", 250, 150, 50, Color.RED);
            Raylib.DrawText($"Score: {_score}", 320, 230, 30, Color.WHITE);
            Raylib.DrawText($"Highscore: {_highscore}", 300, 270, 25, Color.YELLOW);
            Raylib.DrawText("Appuie sur ENTER pour recommencer", 200, 350, 20, Color.GRAY);
        }

        public void Unload() { }
    }
}
namespace TheSnake.Core
{
    public static class HighscoreManager
    {
        private static readonly string HighscoreFilePath = "highscore.txt";

        public static int LoadHighscore()
        {
            if(!File.Exists(HighscoreFilePath)) // Check si le fichier existe
                return 0;
            
            var content = File.ReadAllText(HighscoreFilePath);
            return int.TryParse(content, out int score) ? score : 0;
        }

        public static void SaveHighscore(int score)
        {
            File.WriteAllText(HighscoreFilePath, score.ToString());
        }
    }
}
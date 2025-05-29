namespace TheSnake.Scenes
{
    public static class SceneManager
    {
        private static IScene? _currentScene;
        
        public static void ChangeScene(IScene newScene)
        {
            _currentScene?.Unload();
            _currentScene = newScene;
            _currentScene.Load();
        }
        
        public static void Update(float deltaTime)
        {
            _currentScene?.Update(deltaTime);
        }

        public static void Draw()
        {
            _currentScene?.Draw();
        }
    }  
}


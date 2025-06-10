namespace TheSnake.Scenes
{
    public static class SceneManager
    {
        private static IScene? _currentScene;
        private static readonly Stack<IScene> SceneStack = new();
        
        public static void ChangeScene(IScene newScene)
        {
            _currentScene?.Unload();
            _currentScene = newScene;
            _currentScene.Load();
        }

        public static void PushScene(IScene newScene)
        {
            if (_currentScene != null)
            {
                SceneStack.Push(_currentScene);
            }
            
            _currentScene = newScene;
            _currentScene.Load();
        }

        public static void PopScene()
        {
            _currentScene?.Unload();
            
            if (SceneStack.Count > 0)
            {
                _currentScene = SceneStack.Pop();
            }
        }
        
        public static void Update(float deltaTime)
        {
            _currentScene?.Update(deltaTime);
        }

        public static void Draw()
        {
            _currentScene?.Draw();
        }

        public static void DrawUnderlyingScenes()
        {
            if (SceneStack.Count > 0)
            {
                var underlyingScenes = SceneStack.Peek();
                underlyingScenes.Draw();
            }
        }
    }  
}


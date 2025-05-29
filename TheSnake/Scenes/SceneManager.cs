namespace TheSnake.Scenes
{
    public static class SceneManager
    {
        private static IScene? currentScene;
        
        public static void ChangeScene(IScene newScene)
        {
            currentScene?.unload();
            currentScene = newScene;
            currentScene.load();
        }
        
        public static void Update(float deltaTime)
        {
            currentScene?.update(deltaTime);
        }

        public static void Draw()
        {
            currentScene?.draw();
        }
    }  
}


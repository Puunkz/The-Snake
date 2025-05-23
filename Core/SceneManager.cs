using The_Snake.Scenes;
namespace The_Snake.Core;

public static class SceneManager
{
    private static IScene currentScene;
    public static void ChangeScene(IScene newScene)
    {
        currentScene?.Unload();
        currentScene = newScene;
        currentScene.Load();
    }
    public static void Update() => currentScene?.Update();
    public static void Draw() => currentScene?.Draw();
}
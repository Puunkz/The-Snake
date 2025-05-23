using The_Snake.Scenes;
namespace The_Snake.Core;

public static class SceneManager
{
    private static IScene? _currentScene;
    public static void ChangeScene(IScene newScene)
    {
        _currentScene?.Unload();
        _currentScene = newScene;
        _currentScene.Load();
    }
    public static void Update() => _currentScene?.Update();
    public static void Draw() => _currentScene?.Draw();
}
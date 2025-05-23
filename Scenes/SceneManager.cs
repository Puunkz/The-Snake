using The_Snake.Scenes;
namespace The_Snake.Core;

public static class SceneManager
{
    private static IScene CurrentScene;
    public static void ChangeScene(IScene newScene)
    {
        CurrentScene.Unload();
        CurrentScene = newScene;
        CurrentScene.Load();
    }
    public static void Update() => CurrentScene.Update();
    public static void Draw() => CurrentScene.Draw();
}
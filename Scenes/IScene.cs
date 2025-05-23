namespace The_Snake.Scenes
{
    public interface IScene
    {
        void Load();
        void Update();
        void Draw();
        void Unload();
    }
}
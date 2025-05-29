namespace TheSnake.Scenes
{
    public interface IScene
    {
        void load();
        
        void update(float deltaTime);
        
        void draw();
        
        void unload();
    }
}
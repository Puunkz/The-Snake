using System.Numerics;

namespace TheSnake.Core
{
    public abstract class GameObject(Vector2 position)
    {
        public Vector2 Position = position;

        public abstract void Update(float deltaTime);
        public abstract void Draw();
    }
}

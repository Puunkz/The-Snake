using System.Numerics;

namespace TheSnake.Core
{
    public abstract class GameObject
    {
        public Vector2 Position;

        public GameObject(Vector2 position)
        {
            Position = position;
        }
        public abstract void Update(float deltaTime);
        public abstract void Draw();
    }
}

using System.Numerics;
using TheSnake.Core;
using Raylib_cs;

namespace TheSnake.Entities
{
    public class Fruit(Vector2 position) : GameObject(position)
    {
        private const int CellsSize = 20;

        public override void Update(float deltaTime)
        {
            
        }

        public override void Draw()
        {
            Raylib.DrawRectangle((int)(Position.X * CellsSize), (int)(Position.Y * CellsSize), CellsSize, CellsSize, Color.RED);
        }
    }
}
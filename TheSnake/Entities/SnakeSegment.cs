using System.Numerics;
using TheSnake.Core;
using Raylib_cs;

namespace TheSnake.Entities
{
    public class SnakeSegment : GameObject
    {
        private const int CellsSize = 20; // Taille d'une cellule du serpent
        public SnakeSegment(Vector2 position) : base(position)
        {
        }
        public override void Update(float deltaTime)
        {
        }

        public override void Draw()
        {
            Raylib.DrawRectangle((int)(Position.X * CellsSize), (int)(Position.Y * CellsSize), CellsSize, CellsSize, Color.GREEN);
        }
    }
}
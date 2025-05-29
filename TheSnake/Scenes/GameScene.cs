using System.Numerics;
using Raylib_cs;
using TheSnake.Core;
using TheSnake.Entities;
using TheSnake.Input;

namespace TheSnake.Scenes
{
    public class GameScene : IScene
    {
        private readonly List<SnakeSegment> _snake = new();
        private Fruit _fruit;
        private Vector2 _direction = new(1, 0);
        private float _moveTimer = 0f;
        private const float MoveInterval = 0.2f; // Intervalle de temps entre les mouvements
        
        private bool _growNextMove = false;
        private readonly int _gridWidth = 40;
        private readonly int _gridHeight = 30;
        private IInputService _inputService;

        public void Load()
        {
            _snake.Clear();
            _snake.Add(new SnakeSegment(new Vector2(_gridWidth / 2, _gridHeight / 2)));
            
            SpawnFruit();

            _inputService = ServiceLocator.Get<IInputService>();
            _direction = new Vector2(1, 0);
            _moveTimer = 0f;
            _growNextMove = false;
        }

        public void Update(float deltaTime)
        {
            HandleInput();
            _moveTimer += deltaTime;

            if (_moveTimer >= MoveInterval)
            {
                _moveTimer = 0f;
                MoveSnake();
            }
        }

        public void Draw()
        {
            foreach (var segment in _snake)
            {
                segment.Draw();
            }
            
            _fruit.Draw();
            Raylib.DrawText($"Score: {_snake.Count - 1}", 10, 10, 20, Color.WHITE);
        }

        public void Unload()
        {
            _snake.Clear();
        }

        private void HandleInput()
        {
            Vector2 inputDir = _inputService.GetDirection();
            if (inputDir != Vector2.Zero)
            {
                if (_snake.Count > 1)
                {
                    Vector2 opposite = -_direction;
                    if (inputDir != opposite)
                    {
                        _direction = inputDir;
                    }
                }
                else
                {
                    _direction = inputDir;
                }
            }
        }
        
        private void MoveSnake()
        {
            Vector2 newHeadPos = _snake[0].Position + _direction;
        
            if (newHeadPos.X < 0 ) newHeadPos.X = _gridWidth - 1;
            if (newHeadPos.X >= _gridWidth) newHeadPos.X = 0;
            if (newHeadPos.Y < 0) newHeadPos.Y = _gridHeight - 1;
            if (newHeadPos.Y >= _gridHeight) newHeadPos.Y = 0;

            foreach (var segment in _snake)
            {
                if (segment.Position == newHeadPos)
                {
                    SceneManager.ChangeScene(new MenuScene());
                    return; // Collision avec le corps du serpent, fin du jeu
                }
            }
        
            _snake.Insert(0, new SnakeSegment(newHeadPos));

            if (_growNextMove)
            {
                _growNextMove = false; // Réinitialiser le flag de croissance
            }
        
            else
            {
                _snake.RemoveAt(_snake.Count - 1); // Retirer la dernière segment si pas de croissance
            }

            if (newHeadPos == _fruit.Position)
            {
                _growNextMove = true; // Prochain mouvement, le serpent grandit
                SpawnFruit();
            }
        }

        private void SpawnFruit()
        {
            Random rnd = new();

            Vector2 pos;
            do
            {
                pos = new Vector2(rnd.Next(0, _gridWidth), rnd.Next(0, _gridHeight));
            }
            while (IsPositionOnSnake(pos));
            _fruit = new Fruit(pos);
        }

        private bool IsPositionOnSnake(Vector2 pos)
        {
            foreach (var segment in _snake)
            {
                if (segment.Position == pos) return true;
            }
            return false;
        }
    }
}


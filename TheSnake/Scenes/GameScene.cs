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
        private Fruit? _fruit;
        private Fruit? _specialFruit;
        private Vector2 _direction = new(1, 0); // Direction initiale du serpent (vers la droite)
        private float _moveTimer = 0f;
        private float _moveInterval = MoveIntervalStart;
        private const float MoveIntervalStart = 0.2f; // Intervalle de temps entre les mouvements
        private const float MinMoveInterval = 0.05f; // Intervalle minimum de mouvement
        
        private float _speedIncreaseTimer = 0f; // Timer pour augmenter la vitesse
        private const float SpeedIncreaseInterval = 10f; // Intervalle pour augmenter la vitesse
        
        private bool _growNextMove = false;  
        private readonly int _gridWidth = 40; 
        private readonly int _gridHeight = 30;
        private IInputService? _inputService; 

        private readonly List<Vector2> _obstacles = new(); 
        private int _score = 0; 
        private int _lastObstacleScore = 0; // Dernier score auquel un obstacle a été ajouté
        
        private bool _scoreBoostActive = false; // Indique si le boost de score est actif
        private float _scoreBoostTimer = 0f;
        private const float ScoreBoostDuration = 5f; // Durée du boost de score
        
        public void Load() 
        {
                _snake.Clear(); 

                var headPos = new Vector2(_gridWidth / 2, _gridHeight / 2);
                _snake.Add(new SnakeSegment(headPos));
                _snake.Add(new SnakeSegment(headPos - new Vector2(1, 0)));
                _snake.Add(new SnakeSegment(headPos - new Vector2(2, 0)));

                _inputService = ServiceLocator.Get<IInputService>();
                _direction = new Vector2(1, 0);
                _moveTimer = 0f;
                _moveInterval = MoveIntervalStart;
                _speedIncreaseTimer = 0f;
                
                _growNextMove = false;
                _score = 0; // Réinitialise le score
                _obstacles.Clear(); // Réinitialise les obstacles
                _scoreBoostActive = false;
                _scoreBoostTimer = 0f; // Réinitialise le timer du boost de score

                SpawnFruit();
                _specialFruit = null;
        }

        public void Update(float deltaTime)  
        {
            HandleInput(); ; 
            _moveTimer += deltaTime;
            _speedIncreaseTimer += deltaTime;
            
            if (_speedIncreaseTimer >= SpeedIncreaseInterval && _moveInterval > MinMoveInterval) // verifie si le timer est fini et que l'intervalle de mouvement n'est pas déjà au minimum
            {
                _speedIncreaseTimer = 0f;
                _moveInterval -= 0.01f; // Augmente la vitesse du serpent
            }

            if (_scoreBoostActive)
            {
                _scoreBoostTimer -= deltaTime;
                if (_scoreBoostTimer <= 0f)
                {
                    _scoreBoostActive = false;
                }
            }

            if (_moveTimer >= _moveInterval) 
            {
                _moveTimer = 0f;
                MoveSnake();
            }
            
            if (_specialFruit == null && Raylib.GetRandomValue(0, 1000) < 1) // 1 chance sur 1000 de générer un fruit spécial
            {
                SpawnSpecialFruit();
            }
        }

        public void Draw()
        { 
            foreach (var segment in _snake) // Dessine chaque segment du serpent
            {
                segment.Draw();
            }
            
            foreach (var obstacle in _obstacles)
            {
                Raylib.DrawRectangle((int)(obstacle.X * 20), (int)(obstacle.Y * 20), 20, 20, Color.BLUE);
                Raylib.DrawRectangleLines((int)(obstacle.X * 20), (int)(obstacle.Y * 20), 20, 20, Color.DARKBLUE);
            }
            
            _fruit?.Draw();

            if (_specialFruit != null)
            {
                Raylib.DrawRectangle((int)(_specialFruit.Position.X * 20), (int)(_specialFruit.Position.Y * 20), 20, 20, Color.GOLD);
                Raylib.DrawRectangleLines((int)(_specialFruit.Position.X * 20), (int)(_specialFruit.Position.Y * 20), 20, 20, Color.ORANGE);
            }
            
            string scoreText = $"Score: {_score}";
            if (_scoreBoostActive)
                scoreText += " (x2)";
            Raylib.DrawText(scoreText, 10, 10, 20, Color.WHITE);
            
            string pauseText = "Pause = P";
            Raylib.DrawText(pauseText, 690, 10, 20, Color.WHITE);
        }

        public void Unload()
        {
            _snake.Clear();
        }

        private void HandleInput()
        {
            if (_inputService == null) return;
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

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            {
                SceneManager.PushScene(new PauseScene());
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
                    SceneManager.ChangeScene(new GameOverScene(_score));
                    return; // Collision avec le corps du serpent, fin du jeu
                }
            }
        
            foreach (var obs in _obstacles){
                if (obs == newHeadPos)
                {
                    SceneManager.ChangeScene(new GameOverScene(_score));
                    return; // Collision avec un obstacle, fin du jeu
                }
            }
            
            _snake.Insert(0, new SnakeSegment(newHeadPos));

            if (_growNextMove) // Si le serpent doit grandir
            {
                _growNextMove = false;
            }
            else
            {
                _snake.RemoveAt(_snake.Count - 1);
            }

            if (newHeadPos == _fruit?.Position) // Si le serpent mange le fruit
            {
                _growNextMove = true;
                _score+= _scoreBoostActive ? 2 : 1; // Double le score si le boost est actif
                
                while (_score / 5 > _lastObstacleScore / 5)
                {
                    AddRandomObstacle();
                    _lastObstacleScore += 5;
                }
                SpawnFruit();
            }

            if (_specialFruit != null && newHeadPos == _specialFruit.Position)
            {
                _growNextMove = true; // Le serpent grandit lorsqu'il mange le fruit spécial
                _score += _scoreBoostActive ? 10 : 5; // Double le score si le boost est actif
                _scoreBoostActive = true; // Active le boost de score
                _scoreBoostTimer = ScoreBoostDuration; // Réinitialise le timer du boost de score
                _specialFruit = null; // Supprime le fruit spécial après l'avoir mangé
                
                while (_score / 5 > _lastObstacleScore / 5)
                {
                    AddRandomObstacle();
                    _lastObstacleScore += 5;
                }
            }
        }

        private void SpawnFruit() // Génère une position aléatoire pour le fruit
        {
            Random rnd = new();
            Vector2 pos;
            
            do
            {
                pos = new Vector2(rnd.Next(0, _gridWidth), rnd.Next(0, _gridHeight));
            }
            while (IsPositionOnSnake(pos) || _obstacles.Contains(pos));

            _fruit = new Fruit(pos);
        }

        private void SpawnSpecialFruit() // Génère un fruit spécial
        {
            Random rnd = new();
            Vector2 pos;

            do
            {
                pos = new Vector2(rnd.Next(0, _gridWidth), rnd.Next(0, _gridHeight));
            }
            while (IsPositionOnSnake(pos) || _obstacles.Contains(pos) || (_fruit != null && pos == _fruit.Position));

            _specialFruit = new Fruit(pos);
        }
        
        private void AddRandomObstacle() // Ajoute un obstacle aléatoire sur la grille
        {
            Random rnd = new();
            Vector2 pos;

            do
            {
                pos = new Vector2(rnd.Next(0, _gridWidth), rnd.Next(0, _gridHeight));
            }
            while (IsPositionOnSnake(pos) || pos == _fruit?.Position || _obstacles.Contains(pos)); // Assure que l'obstacle n'est pas sur le serpent, le fruit ou déjà un obstacle
            
            _obstacles.Add(pos);
        }

        private bool IsPositionOnSnake(Vector2 pos) // Vérifie si une position est occupée par le serpent
        {
            foreach (var segment in _snake)
            {
                if (segment.Position == pos) return true;
            }
            return false;
        }

        public int CurrentScore => _score;
    }
}


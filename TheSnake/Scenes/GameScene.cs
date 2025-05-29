using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using TheSnake.Core;
using TheSnake.Entities;
using TheSnake.Input;

namespace TheSnake.Scenes
{
    public class GameScene : IScene
    {
        private List<SnakeSegmment> snake = new();
        private Fruit fruit;
        private Vector2 direction = new(1, 0);
        private float moveTimer = 0f;
        private const float MoveInterval = 0.2f; // Intervalle de temps entre les mouvements
        private bool growNextMove = false;
        
        private int gridWidth = 40;
        private int gridHeight = 30;
        
        private IInputService inputService;

        public void load()
        {
            snake.Clear();
            snake.Add(new SnakeSegmment(new Vector2(gridWidth / 2, gridHeight / 2)));
            
            SpawnFruit();

            inputService = ServiceLocator.Get<IInputService>();
            direction = new Vector2(1, 0);
            moveTimer = 0f;
            growNextMove = false;
        }

        public void Update(float deltaTime)
        {
            HandleInput();
            moveTimer += deltaTime;

            if (moveTimer >= MoveInterval)
            {
                moveTimer = 0f;
                MoveSnake();
            }
        }

        private void HandleInput()
        {
            Vector2 inputDir = inputService.GetDirection();
            if (inputDir == Vector2.Zero)
            {
                if (snake.Count > 1)
                {
                    Vector2 opposite = -direction;
                    if (inputDir != opposite)
                    {
                        direction = inputDir;
                    }
                }
                else
                {
                    direction = inputDir;
                }
            }
        }
    }
    
    private void MoveSnake()
    {
        Vector2 newHeadPos = snake[0].Position + direction;
        
        if (newHeadPos.X < 0 ) newHeadPos.X = gridWidth - 1;
        if (newHeadPos.X >= gridWidth) newHeadPos.X = 0;
        if (newHeadPos.Y < 0) newHeadPos.Y = gridHeight - 1;
        if (newHeadPos.Y >= gridHeight) newHeadPos.Y = 0;

        foreach (var segment in snake)
        {
            if (segment.Postition == newHeadPos)
            {
                SceneManager.ChangeScene(new MenuScene());
                return; // Collision avec le corps du serpent, fin du jeu
            }
        }
        
        snake.Insert(0, new SnakeSegmment(newHeadPos));

        if (growNextMove)
        {
            growNextMove = false; // Réinitialiser le flag de croissance
        }
        
        else
        {
            snake.RemoveAt(snake.Count - 1); // Retirer la dernière segment si pas de croissance
        }

        if (newHeadPos == fruit.postition)
        {
            growNextMove = true; // Prochain mouvement, le serpent grandit
            SpawnFruit();
        }
    }

    private void SpawnFruit()
    {
        Random rnd = new();

        Vector2 pos;
        do
        {
            pos = new Vector2(rnd.Next(0, gridWidth), rnd.Next(0, gridHeight));
        }
        while (IsPositionOnSnake(pos));
        fruit = new Fruit(pos);
    }

    private bool IsPositionOnSnake(Vector2 pos)
    {
        foreach (var segment in snake)
        {
            segment.Draw();
        }
        
        fruit.Draw();
        Raylib.DrawText($"Score: {snake.Count - 1}", 10, 10, 20, Color.WHITE);
    }

    public void Unload()
    {
        snake.Clear();
    }
}


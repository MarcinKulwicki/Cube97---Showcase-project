using System;
using System.Collections.Generic;
using Cube.Controllers;
using UnityEngine;

namespace Cube.Gameplay
{
    public class GameHandler : Singleton<GameHandler>
    {
        [SerializeField]
        GameStatus _initStatus;

        [SerializeField]
        private GameController _controller;

        private Dictionary<GameStatus, Action> _states;

        public void SetStatus(GameStatus command) => _states[command].Invoke();

        protected override void Awake() 
        {
            base.Awake();

            _states = new()
            {
                { GameStatus.StartGame, () => _controller.StartGame() },
                { GameStatus.StopGame, () => _controller.StopGame() },
                { GameStatus.ResumeGame, () => _controller.ResumeGame() },
                
                { GameStatus.MainMenu, () => _controller.MainMenu() },
                { GameStatus.Settings, () => _controller.Settings() },
                { GameStatus.HighScore, () => _controller.HighScore() },
                { GameStatus.Workshop, () => _controller.Workshop() },
                { GameStatus.Achievements, () => _controller.Achievements() },
            };
        }

        private void Start() => SetStatus(_initStatus);
    }

    public enum GameStatus
    {
        StartGame = 0,
        StopGame = 1,
        ResumeGame = 2,
        MainMenu = 3,
        Settings = 4,
        HighScore = 5,
        Workshop = 6,
        Achievements = 7
    }
}

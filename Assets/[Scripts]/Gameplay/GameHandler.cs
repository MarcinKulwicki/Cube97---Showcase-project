using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cube.Gameplay
{
    /// <summary>
    ///     Simplified game state machine
    /// </summary>
    public class GameHandler : Singleton<GameHandler>
    {
        #region  Events
        public Action OnGameStart, OnGameStop, OnGameResume, OnNextLevel;
        public Action OnMainMenu, OnSettings, OnHighScore, OnWorkshop, OnAchievements;
        #endregion

        [Header("Properties")]
        [SerializeField]
        private GameStatus _initStatus;

        private Dictionary<GameStatus, Action> _states;

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            _states = new()
            {
                { GameStatus.StartGame, () => OnGameStart?.Invoke() },
                { GameStatus.StopGame, () => OnGameStop?.Invoke() },
                { GameStatus.ResumeGame, () => OnGameResume?.Invoke() },
                { GameStatus.NextLevel, () => OnNextLevel?.Invoke() },
                { GameStatus.MainMenu, () => OnMainMenu?.Invoke() },
                { GameStatus.Settings, () => OnSettings?.Invoke() },
                { GameStatus.HighScore, () => OnHighScore?.Invoke() },
                { GameStatus.Workshop, () => OnWorkshop?.Invoke() },
                { GameStatus.Achievements, () => OnAchievements?.Invoke() },
            };
        }

        private void Start() => SetStatus(_initStatus);
        #endregion

        public void SetStatus(GameStatus command) => _states[command].Invoke();
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
        Achievements = 7,
        NextLevel = 8
    }
}
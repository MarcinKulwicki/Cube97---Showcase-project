using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Cube.Data;
using Cube.Gameplay;
using UnityEngine;

namespace Cube.Controllers
{
    public class GameController : MonoBehaviour, IGameController
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private ViewController _viewController;
        [SerializeField] private PlayerController _playerController;

        GameData _gameData;
        GameSettings _gameSettings;
        CoroutineContainer _update;

        private void Awake() 
        {
            // It must be in Awake mathod because in constructor PlayerPrefs are used
            _gameData = new();
            _gameSettings = new();
        }

#region States
        public void StartGame()
        {
            _gameData.SetScore(0);
            _levelController.Reset();
            _viewController.Active(ViewType.GameOverlayView, _gameData, this);
            TriggerLayer.Reset?.Invoke(); // Clear all collision information
            _levelController.Generate();
            _playerController.Respawn(_levelController.InitData);
            CameraControl.Instance.Move(_levelController.InitData.StartPos);

            _update = CoroutineContainer.Create(CUpdate());
        }

        public void StopGame()
        {
            _levelController.DeactivateAll();
            CameraControl.Instance.Move(_levelController.InitData.StartPos);
            _viewController.Active(ViewType.KilledView, _gameData, this);

            if (_update != null)
                _update.Interrupt();
        }

        public void ResumeGame()
        {
            _update = CoroutineContainer.Create(CUpdate());
        }

        public void MainMenu()
        {
            _viewController.Active(ViewType.MainMenuView, _gameData, this);
        }

        public void Settings()
        {
            _viewController.Active(ViewType.Settings, _gameData, this);
        }

        public void HighScore()
        {
            _viewController.Active(ViewType.HighScore, _gameData, this);
        }

        public void Workshop()
        {
            _viewController.Active(ViewType.Workshop, _gameData, this);
        }

        public void Achievements()
        {
            _viewController.Active(ViewType.Achievements, _gameData, this);
        }

#endregion

#region Interface

        public void ChangeUserName(string userName) => _gameData.SetUserName(userName);
        public void SetMusic(bool enableMusic) => _gameSettings.SetMusic(enableMusic);
        public void SetEffects(bool enableEffects) => _gameSettings.SetEffects(enableEffects);
        public AbstractGameSettings GetGameSettings() => _gameSettings;

#endregion

#region Logic
        private IEnumerator CUpdate()
        {
            yield return new WaitForSeconds(Validations.TIME_TO_GET_STARTED);
            _playerController.Activate();

            while (true)
            {
                yield return new WaitForSeconds(1f);
                _gameData.SetScore(_gameData.Score + 1);
            }
        }

        private void AppendScore(int value) => _gameData.SetScore(_gameData.Score + value);

#endregion

#region Listeners
        private void OnEnable() 
        {
            _levelController.OnAppendScore += AppendScore;
        }

        private void OnDisable() 
        {
            _levelController.OnAppendScore -= AppendScore;
        }
#endregion
    }
}
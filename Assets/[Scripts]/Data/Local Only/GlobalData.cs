using Cube.Controllers;
using Cube.Gameplay;
using Cube.UI.View;
using UnityEngine;

namespace Cube.Data
{
    /// <summary>
    ///     Container for data
    /// </summary>
    public class GlobalData : MonoBehaviour
    {
        public AbstractGameData GameData { get { return _gameData; } }
        public AbstractGameSettings GameSettings { get { return _gameSettings; } }

        [Header("References")]
        [SerializeField]
        private ViewController _viewController;
        [SerializeField]
        private NetworkController _networkController;
        [SerializeField]
        private LevelController _levelController;

        private GameData _gameData;
        private GameSettings _gameSettings;

        #region MonoBehaviour
        private void Awake()
        {
            // PlayerPrefs requirement
            _gameData = new();
            _gameSettings = new();
        }

        private void Start()
        {
            GameHandler.Instance.OnGameStart += ClearScore;
            GameHandler.Instance.OnGameStop += SendData;

            _levelController.OnStageChanged += StageChanged;

            SettingsView settingsView = _viewController.GetView<SettingsView>();
            settingsView.OnUserNameChanged += UsernameChanged;
            settingsView.OnMusicOnChanged += MusicOnChanged;
            settingsView.OnEffectsOnChanged += EffectsOnChanged;
            _viewController.GetView<GameOverlayView>().OnAppendScore += AppendScore;
        }

        private void OnDestroy()
        {
            GameHandler.Instance.OnGameStart -= ClearScore;
            GameHandler.Instance.OnGameStop -= SendData;

            _levelController.OnStageChanged -= StageChanged;

            SettingsView settingsView = _viewController.GetView<SettingsView>();
            settingsView.OnUserNameChanged -= UsernameChanged;
            settingsView.OnMusicOnChanged -= MusicOnChanged;
            settingsView.OnEffectsOnChanged -= EffectsOnChanged;
            _viewController.GetView<GameOverlayView>().OnAppendScore += AppendScore;
        }
        #endregion

        private void ClearScore() => _gameData.SetScore(0);
        private void SendData() => _networkController.SendScore(_gameData);
        private void StageChanged(int value) => _gameData.SetLevelStage(value);
        private void UsernameChanged(string userName) => _gameData.SetUserName(userName);
        private void MusicOnChanged(bool enable) => _gameSettings.SetMusic(enable);
        private void EffectsOnChanged(bool enable) => _gameSettings.SetEffects(enable);
        private void AppendScore() => _gameData.SetScore(_gameData.Score + _gameData.LevelStage);
    }
}
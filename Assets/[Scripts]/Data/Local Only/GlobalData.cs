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

            _viewController.GetView<SettingsView>().UserNameChanged += OnUsernameChanged;
            _viewController.GetView<SettingsView>().MusicOnChanged += OnMusicOnChanged;
            _viewController.GetView<SettingsView>().EffectsOnChanged += OnEffectsOnChanged;
        }

        private void OnDestroy() 
        {
            GameHandler.Instance.OnGameStart -= ClearScore;
            GameHandler.Instance.OnGameStop -= SendData;

            _viewController.GetView<SettingsView>().UserNameChanged -= OnUsernameChanged;
            _viewController.GetView<SettingsView>().MusicOnChanged -= OnMusicOnChanged;
            _viewController.GetView<SettingsView>().EffectsOnChanged -= OnEffectsOnChanged;
        }
        #endregion

        private void ClearScore() => _gameData.SetScore(0);
        private void SendData() => _networkController.SendScore(_gameData);
        private void OnUsernameChanged(string userName) => _gameData.SetUserName(userName);
        private void OnMusicOnChanged(bool enable) => _gameSettings.SetMusic(enable);
        private void OnEffectsOnChanged(bool enable) => _gameSettings.SetEffects(enable);
    }
}
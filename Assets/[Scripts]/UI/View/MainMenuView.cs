using Cube.Controllers;
using Cube.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI.View
{
    public class MainMenuView : View
    {
        [SerializeField]
        Button _startGameBtn, _settingsBtn, _highScoreBtn, _workshopBtn, _achievementsBtn;

        private void OnEnable() 
        {
            _startGameBtn.onClick.AddListener(OnStartGame);
            _settingsBtn.onClick.AddListener(OnSettings);
            _highScoreBtn.onClick.AddListener(OnHighScore);
            _workshopBtn.onClick.AddListener(OnWorkshop);
            _achievementsBtn.onClick.AddListener(OnAchievements);
        }

        private void OnDisable() 
        {
            _startGameBtn.onClick.RemoveAllListeners();
            _settingsBtn.onClick.RemoveAllListeners();
            _highScoreBtn.onClick.RemoveAllListeners();
            _workshopBtn.onClick.RemoveAllListeners();
            _achievementsBtn.onClick.RemoveAllListeners();
        }

        private void OnStartGame() => GameHandler.Instance.SetStatus(GameStatus.StartGame);
        private void OnSettings() => GameHandler.Instance.SetStatus(GameStatus.Settings);
        private void OnHighScore() => GameHandler.Instance.SetStatus(GameStatus.HighScore);
        private void OnWorkshop() => GameHandler.Instance.SetStatus(GameStatus.Workshop);
        private void OnAchievements() => GameHandler.Instance.SetStatus(GameStatus.Achievements);

        #region Override
        public override ViewType ViewType => ViewType.MainMenuView;
        #endregion
    }
}
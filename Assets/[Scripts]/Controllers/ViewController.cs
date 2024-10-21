using System.Collections.Generic;
using Cube.Data;
using Cube.Gameplay;
using Cube.UI.View;
using UnityEngine;

namespace Cube.Controllers
{
    public class ViewController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private List<View> _views;
        [SerializeField]
        private GlobalData _globalData;
        [SerializeField]
        private NetworkController _networkController;

        #region MonoBehaviour
        /// <summary>
        ///     All views should be disabled at start
        /// </summary>
        private void Awake() 
        {
            foreach (var item in _views)
                item.gameObject.SetActive(false);
        }

        private void OnEnable() 
        {
            GameHandler.Instance.OnGameStart += GameStart;
            GameHandler.Instance.OnGameStop += GameStop;
            GameHandler.Instance.OnNextLevel += NextLevel;
            GameHandler.Instance.OnMainMenu += MainMenu;
            GameHandler.Instance.OnSettings += Settings;
            GameHandler.Instance.OnHighScore += HighScore;
            GameHandler.Instance.OnWorkshop += Workshop;
            GameHandler.Instance.OnAchievements += Achievements;
        }

        private void OnDisable() 
        {
            GameHandler.Instance.OnGameStart -= GameStart;
            GameHandler.Instance.OnGameStop -= GameStop;
            GameHandler.Instance.OnNextLevel -= NextLevel;
            GameHandler.Instance.OnMainMenu -= MainMenu;
            GameHandler.Instance.OnSettings -= Settings;
            GameHandler.Instance.OnHighScore -= HighScore;
            GameHandler.Instance.OnWorkshop -= Workshop;
            GameHandler.Instance.OnAchievements -= Achievements;
        }
        #endregion

        #region Redirections
        private void GameStart() => Active(ViewType.GameOverlayView, _globalData);
        private void GameStop() => Active(ViewType.KilledView, _globalData);
        private void NextLevel() => Active(ViewType.GameOverlayView, _globalData);
        private void MainMenu() => Active(ViewType.MainMenuView, _globalData);
        private void Settings() => Active(ViewType.Settings, _globalData);
        private void Workshop() => Active(ViewType.Workshop, _globalData);
        private void Achievements() => Active(ViewType.Achievements, _globalData);
        private void HighScore()
        {
            var item = Active(ViewType.HighScore, _globalData);
            _networkController.GetTopScores ( result => 
            {
                GetView<HighScoreView>().Inject(result);
            }, Validations.TOP_SCORE_COUNT );
        }
        #endregion

        public T GetView<T>() where T : View
        {
            for (int i = 0; i < _views.Count; i++)
            {
                if (_views[i] is T)
                    return (T)_views[i];
            }
            return null;
        }

        private View Active(ViewType viewType, GlobalData data)
        {
            DeactivateAll();

            if (GetView(viewType, out View item))
                item.Active(data);

            return item;
        }

        private void DeactivateAll()
        {
            foreach (var view in _views)
                if (view.IsActive) view.Deactivate();
        }

        private bool GetView(ViewType viewType, out View item)
        {
            item = _views.Find(view => view.ViewType == viewType);
            if (item == null)
            {
                Debug.LogError($"{viewType} does not exsit.");
                return false;
            }
            return true;
        }
    }

    public enum ViewType
    {
        None,
        KilledView,
        GameOverlayView,
        MainMenuView,
        Settings,
        HighScore,
        Workshop,
        Achievements
    }
}
using Cube.Controllers;
using Cube.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI.View
{
    public class AchievementsView : View
    {
        [SerializeField]
        private Button _backBtn;

        #region MonoBehaviour
        private void OnEnable()
        {
            _backBtn.onClick.AddListener(OnBack);
        }

        private void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();
        }
        #endregion

        private void OnBack()
        {
            GameHandler.Instance.SetStatus(GameStatus.MainMenu);
        }

        #region Override
        public override ViewType ViewType => ViewType.Achievements;
        #endregion
    }
}
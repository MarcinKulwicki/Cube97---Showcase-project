using Cube.Controllers;
using Cube.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI.View
{
    public class KilledView : View
    {
        [SerializeField]
        private Button _resetBtn;

        private void OnReset()
        {
            GameHandler.Instance.SetStatus(GameStatus.StartGame);
        }

        private void OnEnable() 
        {
            _resetBtn.onClick.AddListener(OnReset);
        }

        private void OnDisable() 
        {
            _resetBtn.onClick.RemoveAllListeners();
        }

        #region Override
        public override ViewType ViewType => ViewType.KilledView;
        #endregion

    }
}
using Cube.Controllers;
using Cube.Data;
using Cube.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI.View
{
    public class KilledView : View<INjectable>
    {
        [SerializeField]
        Button _resetBtn;

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

        public override void Inject(INjectable[] data) => throw new System.NotImplementedException();

        #endregion

    }
}
using System.Collections;
using Cube.Controllers;
using Cube.Data;
using TMPro;
using UnityEngine;

namespace Cube.UI.View
{
    public class GameOverlayView : View<INjectable>
    {
        [SerializeField]
        private TextMeshProUGUI _scoreValue, _timerValue, _stageValue;
        [SerializeField]
        private GameObject _viewBlocker, _successNextStage;

        private float _timer;

        #region MonoBehaviour
        private void OnEnable() 
        {
            _data.GameData.OnScoreChanged += OnScoreChanged;
            OnScoreChanged(_data.GameData.Score);
            
            CoroutineContainer.Create(TimerLoop());

            var stage = _data.GameData.LevelStage;
            _stageValue.text = stage.ToString();
            _successNextStage.SetActive(stage > 1);
            _viewBlocker.SetActive(true);
        }

        private void OnDisable() 
        {
            _data.GameData.OnScoreChanged -= OnScoreChanged;
        }
        #endregion

        private IEnumerator TimerLoop()
        {
            _timer = 0f;
            _timerValue.gameObject.SetActive(true);

            while (_timer % 60 < Validations.TIME_TO_GET_STARTED)
            {
                yield return new WaitForEndOfFrame();

                _timer += Time.deltaTime;
                _timerValue.text = Mathf.CeilToInt(Validations.TIME_TO_GET_STARTED - _timer % 60).ToString();
            }

            _timerValue.gameObject.SetActive(false);
            _viewBlocker.SetActive(false);
            _successNextStage.SetActive(false);
        }

        private void OnScoreChanged(int value) => _scoreValue.text = value.ToString();
        
#region Override

        public override ViewType ViewType => ViewType.GameOverlayView;

        public override void Inject(INjectable[] data) => throw new System.NotImplementedException();

#endregion
    }
}
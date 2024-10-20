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
        TextMeshProUGUI _scoreValue, _timerValue, _stageValue;

        [SerializeField]
        GameObject _viewBlocker, _successNextStage;

        float _timer;

        private void OnEnable() 
        {
            _gameData.OnScoreChanged += OnScoreChanged;
            OnScoreChanged(_gameData.Score);
            
            CoroutineContainer.Create(CTimer());

            var stage = _controller.GetLevelStage();
            _stageValue.text = stage.ToString();
            _successNextStage.SetActive(stage > 1);
            _viewBlocker.SetActive(true);
        }

        private void OnDisable() 
        {
            _gameData.OnScoreChanged -= OnScoreChanged;
        }

        IEnumerator CTimer()
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
using System.Collections;
using Cube.Controllers;
using Cube.Data;
using TMPro;
using UnityEngine;

namespace Cube.UI.View
{
    public class GameOverlayView : View
    {
        [SerializeField]
        private TextMeshProUGUI _scoreValue, _timerValue, _stageValue;
        [SerializeField]
        private GameObject _viewBlocker, _successNextStage;

        private float _timer;

        #region MonoBehaviour
        private void OnEnable() 
        {
            _data.GameData.OnScoreChanged += ScoreChanged;
            _data.GameData.OnLevelStageChanged += LevelStageChanged;
            
            CoroutineContainer.Create(TimerLoop());

            _successNextStage.SetActive(_data.GameData.LevelStage > 0);
            _viewBlocker.SetActive(true);
        }

        private void OnDisable() 
        {
            _data.GameData.OnScoreChanged -= ScoreChanged;
            _data.GameData.OnLevelStageChanged -= LevelStageChanged;
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

        private void ScoreChanged(int value) => _scoreValue.text = value.ToString();
        private void LevelStageChanged(int value) => _stageValue.text = value.ToString();
        
        #region Override
        public override ViewType ViewType => ViewType.GameOverlayView;
        #endregion
    }
}
using System;
using System.Collections;
using Cube.Controllers;
using Cube.Data;
using TMPro;
using UnityEngine;

namespace Cube.UI.View
{
    public class GameOverlayView : View
    {
        public Action OnAppendScore;

        [Header("References")]
        [SerializeField]
        private TextMeshProUGUI _scoreValue, _timerValue, _stageValue;
        [SerializeField]
        private GameObject _viewBlocker, _successNextStage;

        private CoroutineContainer _coroutine;
        private float _timer;

        #region MonoBehaviour
        private void OnEnable()
        {
            _data.GameData.OnScoreChanged += ScoreChanged;
            _data.GameData.OnLevelStageChanged += LevelStageChanged;

            _coroutine = CoroutineContainer.Create(TimerLoop());

            _successNextStage.SetActive(_data.GameData.LevelStage > 0);
            _viewBlocker.SetActive(true);
        }

        private void OnDisable()
        {
            _coroutine.Interrupt();

            _data.GameData.OnScoreChanged -= ScoreChanged;
            _data.GameData.OnLevelStageChanged -= LevelStageChanged;
        }
        #endregion

        private IEnumerator TimerLoop()
        {
            _timer = 0f;
            _timerValue.gameObject.SetActive(true);

            while (_timer % 60 < Global.TIME_TO_GET_STARTED)
            {
                yield return new WaitForEndOfFrame();

                _timer += Time.deltaTime;
                _timerValue.text = Mathf.CeilToInt(Global.TIME_TO_GET_STARTED - _timer % 60).ToString();
            }

            _timerValue.gameObject.SetActive(false);
            _viewBlocker.SetActive(false);
            _successNextStage.SetActive(false);

            while (true)
            {
                yield return new WaitForSeconds(1f);
                OnAppendScore?.Invoke();
            }
        }

        private void ScoreChanged(int value) => _scoreValue.text = value.ToString();
        private void LevelStageChanged(int value) => _stageValue.text = value.ToString();

        #region Override
        public override ViewType ViewType => ViewType.GameOverlayView;
        #endregion
    }
}
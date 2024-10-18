using Cube.Controllers;
using TMPro;
using UnityEngine;

namespace Cube.UI.View
{
    public class GameOverlayView : View
    {
        [SerializeField]
        TextMeshProUGUI _scoreValue;

        private void OnEnable() 
        {
            _gameData.OnScoreChanged += OnScoreChanged;
            OnScoreChanged(_gameData.Score);
        }

        private void OnDisable() 
        {
            _gameData.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int value) => _scoreValue.text = value.ToString();
        
#region Override
        public override ViewType ViewType => ViewType.GameOverlayView;
#endregion
    }
}
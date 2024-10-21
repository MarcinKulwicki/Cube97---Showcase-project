using Cube.Gameplay;
using UnityEngine;

namespace Cube.Data
{
    /// <summary>
    ///     Container for data
    /// </summary>
    public class GlobalData : MonoBehaviour
    {
        public GameData GameData { get; private set; }
        public GameSettings GameSettings  { get; private set; }

        #region MonoBehaviour
        private void Awake() 
        {
            // It must be in Awake mathod because in constructor PlayerPrefs are used
            GameData = new();
            GameSettings = new();
        }

        private void Start() 
        {
            GameHandler.Instance.OnGameStart += ClearScore;
        }

        private void OnDestroy() 
        {
            GameHandler.Instance.OnGameStart -= ClearScore;
        }
        #endregion

        private void ClearScore() => GameData.SetScore(0);
    }
}
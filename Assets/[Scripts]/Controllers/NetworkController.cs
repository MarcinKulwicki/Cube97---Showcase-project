using System;
using Cube.Data;
using Cube.Gameplay;
using UnityEngine;

namespace Cube.Controllers
{
    public class NetworkController : MonoBehaviour
    {
        public static bool IsOnline { get; private set; } = false; //TODO move it to NetworkConfig when will be ScriptableObject
        
        public TopScoreData TopScoreData { get; private set; } = new();

        [SerializeField]
        private GlobalData _globalData;

        #region MonoBehaviour
        private void Start() 
        {
            GameHandler.Instance.OnGameStop += StopGame;
        }

        private void OnDestroy() 
        {
            GameHandler.Instance.OnGameStop -= StopGame;
        }
        #endregion

        public void GetTopScores(Action<TopScoreItemData[]> OnSuccess, int limit)
        {
            TopScoreData.Get(result => 
            {   
                OnSuccess?.Invoke
                (
                    TopScoreData.Filter(result, TopScoreFilterType.Top, limit)
                );
            }, 
            error =>  Debug.Log(error));
        }

        private void StopGame() => SendScore(_globalData.GameData);

        private void SendScore(GameData data)
        {
            TopScoreData.Post
            (
                new TopScoreItemData(data.UserName, data.Score),
                result => Debug.Log(result),
                OnFail
            );
        }

        private void OnFail(string message) => Debug.LogWarning(message);
    }
}

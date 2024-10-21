using System;
using Cube.Data;
using UnityEngine;

namespace Cube.Controllers
{
    public class NetworkController : MonoBehaviour
    {
        public static bool IsOnline { get; private set; } = false; //TODO move it to NetworkConfig when will be ScriptableObject
        
        public TopScoreData TopScoreData { get; private set; } = new();

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

        public void SendScore(GameData data)
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

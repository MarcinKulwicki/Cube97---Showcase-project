using System;
using Cube.Data;
using UnityEngine;

namespace Cube.Controllers
{
    // TODO IsOnline should be moved in some sort of config and then there is no reason to keep that class as Singleton
    public class NetworkController : Singleton<NetworkController>
    {
        [field: SerializeField]
        public bool IsOnline { get; private set; } = false;

        public TopScoreData TopScoreData { get; private set; } = new();

        public void SendScore(GameData data)
        {
            TopScoreData.Post
            (
                new TopScoreItemData(data.UserName, data.Score),
                result => Debug.Log(result),
                OnFail
            );
        }

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

        private void OnFail(string message) => Debug.LogWarning(message);
    }
}

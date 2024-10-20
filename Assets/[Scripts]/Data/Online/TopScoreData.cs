using System;
using System.Linq;
using Cube.Network;

namespace Cube.Data
{
    public class TopScoreData : NetworkService<TopScoreItemData> 
    {
        protected override string OnlineEndPoint => NetworkConfig.TOP_SCORE;
        protected override string OfflineEndPoint => NetworkConfig.TOP_SCORE_OFFLINE;
        
        /// <summary>
        ///     Should be used only on local data. Data from BE should be filtered on BE.
        /// </summary>
        public TopScoreItemData[] Filter(TopScoreItemData[] items, TopScoreFilterType filterType, int limit = 0)
        {
            if (filterType == TopScoreFilterType.Top)
                return Top(items, limit);

            return items;
        }

        private TopScoreItemData[] Top(TopScoreItemData[] items, int limit)
        {
            if (items != null && items.Length > 0)
                items = items.ToList().OrderByDescending(item => item.Score).Take(limit).ToArray();

            return items;
        }
    }  

    public enum TopScoreFilterType
    {
        Top
    }

    [Serializable]
    public class TopScoreItemData : INjectable
    {
        public string UserName;
        public int Score;

        public TopScoreItemData(string userName, int score)
        {
            UserName = userName;
            Score = score;
        }
    }
}

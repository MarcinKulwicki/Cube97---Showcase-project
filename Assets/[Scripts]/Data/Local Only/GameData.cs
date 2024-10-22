using UnityEngine;

namespace Cube.Data
{
    public class GameData : AbstractGameData
    {
        private static readonly string USER_NAME_KEY = "UserName";

        public GameData()
        {
            UserName = PlayerPrefs.GetString(USER_NAME_KEY, "Default Name");
        }

        public void SetUserName(string userName)
        {
            PlayerPrefs.SetString(USER_NAME_KEY, userName);
            PlayerPrefs.Save();

            UserName = userName;
        }

        public void SetScore(int score) => Score = score;

        public void SetLevelStage(int stage) => LevelStage = stage;
    }
}

using UnityEngine;

namespace Cube.Data
{
    public class GameSettings : AbstractGameSettings
    {
        private static readonly string MUSIC_ON_NAME_KEY = "MusicOn";
        private static readonly string EFFECTS_ON_NAME_KEY = "EffectsOn";

        public void SetMusic(bool enable) 
        {
            PlayerPrefs.SetInt(MUSIC_ON_NAME_KEY, enable ? 1 : 0);
            PlayerPrefs.Save();

            MusicOn = enable;
        }
        
        public void SetEffects(bool enable) 
        {
            PlayerPrefs.SetInt(EFFECTS_ON_NAME_KEY, enable ? 1 : 0);
            PlayerPrefs.Save();
            
            EffectsOn = enable;
        }

        public GameSettings()
        {
            MusicOn = PlayerPrefs.GetInt(MUSIC_ON_NAME_KEY, 1) > 0;
            EffectsOn = PlayerPrefs.GetInt(EFFECTS_ON_NAME_KEY, 1) > 0;
        }
    }
}
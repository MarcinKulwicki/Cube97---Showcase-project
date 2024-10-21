using System;
using Cube.Controllers;
using Cube.Data;
using Cube.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI.View
{
    public class SettingsView : View
    {
        public Action<string> UserNameChanged;
        public Action<bool> MusicOnChanged;
        public Action<bool> EffectsOnChanged;

        [Header("References")]
        [SerializeField]
        private Button _backBtn;
        [SerializeField]
        private Toggle _music, _effects;
        [SerializeField]
        private TMP_InputField _userName;

        #region MonoBehaviour
        private void OnEnable() 
        {
            _backBtn.onClick.AddListener(OnBack);
            _music.onValueChanged.AddListener(OnMusicChanged);
            _effects.onValueChanged.AddListener(OnEffectsChanged);
        }

        private void OnDisable() 
        {
            _backBtn.onClick.RemoveAllListeners();
            _music.onValueChanged.RemoveAllListeners();
            _effects.onValueChanged.RemoveAllListeners();
        }
        #endregion

        public override void Active(GlobalData data)
        {
            base.Active(data);

            _music.isOn = _data.GameSettings.MusicOn;
            _effects.isOn = _data.GameSettings.EffectsOn;
            _userName.text = _data.GameData.UserName;
        }

        private void OnBack()
        {
            if (_userName.text.Length >= Validations.MIN_LETTERS_USER_NAME)
                UserNameChanged?.Invoke(_userName.text);

            GameHandler.Instance.SetStatus(GameStatus.MainMenu);
        }

        private void OnMusicChanged(bool enable) => MusicOnChanged?.Invoke(enable);
        private void OnEffectsChanged(bool enable) => EffectsOnChanged?.Invoke(enable);
        
        #region Override
        public override ViewType ViewType => ViewType.Settings;
        #endregion
    }
}
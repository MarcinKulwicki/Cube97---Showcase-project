using Cube.Controllers;
using Cube.Data;
using Cube.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI.View
{
    public class SettingsView : View<INjectable>
    {
        [SerializeField]
        Button _backBtn;

        [SerializeField]
        Toggle _music, _effects;

        [SerializeField]
        TMP_InputField _userName;

        public override void Active(AbstractGameData data, IGameController controller)
        {
            base.Active(data, controller);

            _music.isOn = _controller.GetGameSettings().MusicOn;
            _effects.isOn = _controller.GetGameSettings().EffectsOn;
            _userName.text = _gameData.UserName;
        }

        private void OnBack()
        {
            if (_userName.text.Length >= Validations.MIN_LETTERS_USER_NAME)
                _controller.ChangeUserName(_userName.text);

            GameHandler.Instance.SetStatus(GameStatus.MainMenu);
        }

        private void OnMusicChanged(bool enable) => _controller.SetMusic(enable);
        private void OnEffectsChanged(bool enable) => _controller.SetEffects(enable);

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
        
#region Override

        public override ViewType ViewType => ViewType.Settings;

        public override void Inject(INjectable[] data) => throw new System.NotImplementedException();

#endregion
    }
}
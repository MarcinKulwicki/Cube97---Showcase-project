using System.Collections.Generic;
using Cube.Controllers;
using Cube.Data;
using Cube.Gameplay;
using Cube.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI.View
{
    public class HighScoreView : View<INjectable>
    {
        [SerializeField]
        Button _backBtn;

        [SerializeField]
        TopScoreItem _topScoreItem;

        [SerializeField]
        Transform _topScoreParent;

        List<TopScoreItem> _items = new();

        private void OnBack()
        {
            GameHandler.Instance.SetStatus(GameStatus.MainMenu);
        }

        private void OnEnable() 
        {
            _backBtn.onClick.AddListener(OnBack);
        }

        private void OnDisable() 
        {
            _backBtn.onClick.RemoveAllListeners();

            foreach(var item in _items)
                item.gameObject.SetActive(false);
        }

#region Override

        public override ViewType ViewType => ViewType.HighScore;

        public override void Inject(INjectable[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                if (_items.Count <= i)
                    _items.Add(Instantiate(_topScoreItem, _topScoreParent));
                 
                TopScoreItemData item = (TopScoreItemData) data[i];
                
                _items[i].gameObject.SetActive(true);
                _items[i].Set(i + 1, item.UserName, item.Score);
            }
        }

        #endregion
    }
}
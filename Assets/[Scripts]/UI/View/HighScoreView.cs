using System.Collections.Generic;
using Cube.Controllers;
using Cube.Data;
using Cube.Gameplay;
using Cube.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI.View
{
    public class HighScoreView : View
    {
        [Header("References")]
        [SerializeField]
        private Button _backBtn;
        [SerializeField]
        private TopScoreItem _topScoreItem;
        [SerializeField]
        private Transform _topScoreParent;

        private List<TopScoreItem> _items = new();

        #region MonoBehaviour
        private void OnEnable()
        {
            _backBtn.onClick.AddListener(OnBack);
        }

        private void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();

            foreach (var item in _items)
                item.gameObject.SetActive(false);
        }
        #endregion

        public void Inject(TopScoreItemData[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (_items.Count <= i)
                    _items.Add(Instantiate(_topScoreItem, _topScoreParent));

                _items[i].gameObject.SetActive(true);
                _items[i].Set(i + 1, data[i].UserName, data[i].Score);
            }
        }

        private void OnBack()
        {
            GameHandler.Instance.SetStatus(GameStatus.MainMenu);
        }

        #region Override
        public override ViewType ViewType => ViewType.HighScore;
        #endregion
    }
}
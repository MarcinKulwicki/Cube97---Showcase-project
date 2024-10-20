using System.Collections.Generic;
using Cube.Data;
using Cube.UI.View;
using UnityEngine;

namespace Cube.Controllers
{
    public class ViewController : MonoBehaviour
    {
        [SerializeField]
        List<View<INjectable>> _views;

        /// <summary>
        ///     All views should be disabled at start
        /// </summary>
        private void Awake() 
        {
            foreach (var item in _views)
                item.gameObject.SetActive(false);
        }

        public View<INjectable> Active(ViewType viewType, AbstractGameData data, IGameController controller)
        {
            DeactivateAll();

            if (GetView(viewType, out View<INjectable> item))
                item.Active(data, controller);

            return item;
        }

        public void Deactivate(ViewType viewType)
        {
            if (GetView(viewType, out View<INjectable> item) && item.IsActive)
                item.Deactivate();
        }

        public void DeactivateAll()
        {
            foreach (var view in _views)
                if (view.IsActive) view.Deactivate();
        }

        private bool GetView(ViewType viewType, out View<INjectable> item)
        {
            item = _views.Find(view => view.ViewType == viewType);
            if (item == null)
            {
                Debug.LogError($"{viewType} does not exsit.");
                return false;
            }
            return true;
        }
    }

    public enum ViewType
    {
        None,
        KilledView,
        GameOverlayView,
        MainMenuView,
        Settings,
        HighScore,
        Workshop,
        Achievements
    }
}
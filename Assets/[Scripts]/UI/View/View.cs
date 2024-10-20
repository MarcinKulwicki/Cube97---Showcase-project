using Cube.Controllers;
using Cube.Data;
using UnityEngine;

namespace Cube.UI.View
{
    public abstract class View<INjectable> : MonoBehaviour
    {
        public bool IsActive { get; private set;}
        
        protected AbstractGameData _gameData;
        protected IGameController _controller;
        public virtual ViewType ViewType { get; private set;} = ViewType.None;
        public virtual void Active(AbstractGameData data, IGameController controller)
        {
            _gameData = data;
            _controller = controller;
            IsActive = true;
            gameObject.SetActive(IsActive);
        }
        public virtual void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(IsActive);
            _gameData = null;
        }

        public abstract void Inject(INjectable[] data);
    }
}
using Cube.Controllers;
using Cube.Data;
using UnityEngine;

namespace Cube.UI.View
{
    public abstract class View : MonoBehaviour
    {
        public virtual ViewType ViewType { get; private set; } = ViewType.None;
        public bool IsActive { get; private set; }

        protected GlobalData _data;

        public virtual void Active(GlobalData data)
        {
            _data = data;
            IsActive = true;
            gameObject.SetActive(IsActive);
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(IsActive);
            _data = null;
        }
    }
}
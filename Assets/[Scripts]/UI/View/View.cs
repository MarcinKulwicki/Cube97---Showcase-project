using Cube.Controllers;
using Cube.Data;
using UnityEngine;

namespace Cube.UI.View
{
    public abstract class View<INjectable> : MonoBehaviour
    {
        public bool IsActive { get; private set;}
        public virtual ViewType ViewType { get; private set;} = ViewType.None;
        
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

        public abstract void Inject(INjectable[] data);
    }
}
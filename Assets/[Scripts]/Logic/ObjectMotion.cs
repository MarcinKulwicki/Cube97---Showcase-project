using System.Collections;
using UnityEngine;

namespace Cube.Logic
{
    public abstract class ObjectMotion : MonoBehaviour
    {
        public bool IsActive 
        { 
            get => _isActive;
            protected set
            {
                if (_renderer != null)  _renderer.enabled = value;
                if (_collider != null)  _collider.enabled = value;
                _isActive = value;
            } 
        }
        private CoroutineContainer _coroutine;

        private MeshRenderer _renderer;
        private Collider _collider;

        private bool _isActive;

        protected virtual void Awake() 
        {
            _renderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();

            IsActive = false;
        }

        public virtual void Activate()
        {
            IsActive = true;
            _coroutine = CoroutineContainer.Create(Process());
        }

        public virtual void Deactivate()
        {
            IsActive = false;

            if (_coroutine != null)
                _coroutine.Interrupt();

            _coroutine = null;
        }

        protected abstract IEnumerator Process();
    }
}

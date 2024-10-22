using System.Collections;
using UnityEngine;

namespace Cube.Logic
{
    public class Rotate : ObjectMotion
    {
        [SerializeField]
        private Vector3 _rotateVector = new(0, 1, 0);
        [SerializeField]
        private float _turnSpeed = 60f;

        private Quaternion _startRotation;

        protected override void Awake()
        {
            _startRotation = transform.rotation;
            base.Awake();
        }

        public override void Activate()
        {
            base.Activate();
            transform.rotation = _startRotation;
        }

        public override void Deactivate()
        {
            transform.rotation = _startRotation;
            base.Deactivate();
        }

        protected override IEnumerator Process()
        {
            while (IsActive)
            {
                yield return new WaitForFixedUpdate();
                transform.Rotate(Time.fixedDeltaTime * _turnSpeed * _rotateVector);
            }
        }
    }
}
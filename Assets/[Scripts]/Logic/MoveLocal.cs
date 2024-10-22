using System.Collections;
using UnityEngine;

namespace Cube.Logic
{
    public class MoveLocal : ObjectMotion
    {
        [SerializeField]
        private Vector3 _moveVector = new(-1, 0, -1);
        [SerializeField]
        private float _moveSpeed = 0.1f;

        private Vector3 _startPos;

        protected override void Awake()
        {
            _startPos = transform.localPosition;
            base.Awake();
        }

        public override void Activate()
        {
            transform.localPosition = _startPos;
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            transform.localPosition = _startPos;
        }

        protected override IEnumerator Process()
        {
            while (IsActive)
            {
                yield return new WaitForFixedUpdate();
                transform.localPosition += _moveSpeed * Time.deltaTime * _moveVector;
            }
        }
    }
}
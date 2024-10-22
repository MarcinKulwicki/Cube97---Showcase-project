using System.Collections;
using UnityEngine;

namespace Cube.Gameplay
{
    public class CameraControl : Singleton<CameraControl>
    {
        [Header("Properties")]
        [SerializeField]
        private AnimationCurve _animation;

        private Vector3 _shift;
        private CoroutineContainer _coroutine;

        protected override void Awake()
        {
            base.Awake();
            _shift = transform.position;
        }

        public void Move(Vector3 targetPos)
        {
            if (_coroutine != null)
                _coroutine.Interrupt();

            _coroutine = CoroutineContainer.Create(CMove(targetPos));
        }

        private IEnumerator CMove(Vector3 targetPos)
        {
            var startVec = transform.position;
            var endVec = _shift + targetPos;

            for (float i = 0; i <= 1; i += Time.fixedDeltaTime)
            {
                transform.position = Vector3.Lerp(startVec, endVec, Mathf.Clamp01(_animation.Evaluate(i)));
                yield return new WaitForFixedUpdate();
            }

            _coroutine = null;
        }
    }
}

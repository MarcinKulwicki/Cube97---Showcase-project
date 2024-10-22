using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cube.Gameplay.Player
{
    public class PlayerControl : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField]
        private AnimationCurve _animation;

        private PlayerInputActions _inputActions;
        private CoroutineContainer _moving;
        private int _moveStep = 15;

        private void Awake()
        {
            _inputActions = new();
            _inputActions.Player.Enable();

            _inputActions.Player.Movement.performed += Move;
            _inputActions.Player.Back.performed += Back;
        }

        public void Interrupt()
        {
            if (_moving != null)
                _moving.Interrupt();

            _moving = null;
        }

        public void SetMoveStep(int step) => _moveStep = step;

        private void Move(InputAction.CallbackContext context)
        {
            if (_moving != null)
                return;

            Vector2 inputVector = context.ReadValue<Vector2>();

            float angle = (Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg) - CameraControl.Instance.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

            _moving = CoroutineContainer.Create(Moving(inputVector));
        }

        private void Back(InputAction.CallbackContext context) { }

        private IEnumerator Moving(Vector2 inputVector)
        {
            var startVec = transform.position;
            var endVec = transform.position + _moveStep * new Vector3(inputVector.x, 0, inputVector.y);

            CameraControl.Instance.Move(endVec);

            for (float i = 0; i <= 1; i += Time.fixedDeltaTime)
            {
                transform.position = Vector3.Lerp(startVec, endVec, Mathf.Clamp01(_animation.Evaluate(i)));
                yield return new WaitForFixedUpdate();
            }

            _moving = null;
        }
    }
}
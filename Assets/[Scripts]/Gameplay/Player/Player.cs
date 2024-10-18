using UnityEngine;

namespace Cube.Gameplay.Player
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(PlayerVisual))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        PlayerControl _controls;

        PlayerVisual _visual;
        Collider _collider;

        private void Awake() 
        {
            _collider = GetComponent<Collider>();
            _visual = GetComponent<PlayerVisual>();
        }

        public void Kill(Collider obstacle)
        {
            Debug.Log($"Player killed by : {obstacle.transform.parent.parent}/{obstacle.transform.parent}/{obstacle.gameObject}");

            GameHandler.Instance.SetStatus(GameStatus.StopGame);

            _controls.Interrupt();            
            _collider.enabled = false;
            _visual.SetStatus(VisualStatus.Killed);
        }

        public void Respawn(Vector3 pos, Quaternion rot)
        {
            _controls.Interrupt();
            
            _visual.SetStatus(VisualStatus.Alive);
            transform.SetPositionAndRotation(pos, rot);
            _collider.enabled = false;
        }

        public void Activate() => _collider.enabled = true;
    }
}
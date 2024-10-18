using UnityEngine;

namespace Cube.Gameplay.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] Material _alive, _killed;

        public void SetStatus(VisualStatus visualStatus)
        {
            _renderer.material = visualStatus == VisualStatus.Alive ? _alive : _killed;
        }
    }

    public enum VisualStatus
    {
        Killed,
        Alive
    }
}
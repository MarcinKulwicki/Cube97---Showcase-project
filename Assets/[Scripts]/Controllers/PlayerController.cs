using Cube.Gameplay.Player;
using UnityEngine;

namespace Cube.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Player _player;
        [SerializeField] PlayerControl _control;

        public void Respawn(LevelInitData levelInitData)
        {
            _control.SetMoveStep(levelInitData.Step);
            _player.Respawn(levelInitData.StartPos, levelInitData.StartRot);
        }
        
    }
}
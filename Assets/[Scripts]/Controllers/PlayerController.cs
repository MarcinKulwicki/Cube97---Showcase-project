using System.Collections;
using Cube.Data;
using Cube.Gameplay.Player;
using UnityEngine;

namespace Cube.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Player _player;
        [SerializeField] PlayerControl _control;

        public void Respawn(LevelInitData levelInitData)
        {
            _control.SetMoveStep(levelInitData.Step);
            _player.Respawn(levelInitData.StartPos, levelInitData.StartRot);

            CoroutineContainer.Create(Activate());
        }
        
        private IEnumerator Activate() 
        {
            yield return new WaitForSeconds(Validations.TIME_TO_GET_STARTED);
            _player.Activate();
        }
    }
}
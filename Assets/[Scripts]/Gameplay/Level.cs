using System.Collections.Generic;
using Cube.Logic;
using UnityEngine;

namespace Cube.Gameplay
{
    public class Level : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        List<ObjectMotion> _elements;

        public void Activate()
        {
            foreach (var item in _elements)
                item.Activate();
        }

        public void Deactivate()
        {
            foreach (var item in _elements)
                item.Deactivate();
        }
    }
}

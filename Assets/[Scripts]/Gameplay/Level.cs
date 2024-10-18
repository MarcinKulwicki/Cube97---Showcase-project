using System;
using System.Collections.Generic;
using Cube.Logic;
using UnityEngine;

namespace Cube.Gameplay
{
    public class Level : MonoBehaviour
    {
        public Action OnNextAreaReached;

        [SerializeField]
        List<ObjectMotion> _elements;

        public void Activate() 
        {
            OnNextAreaReached?.Invoke();

            foreach(var item in _elements)
                item.Activate();
        }

        public void Deactivate() 
        {
            foreach(var item in _elements)
                item.Deactivate();
        }
    }
}

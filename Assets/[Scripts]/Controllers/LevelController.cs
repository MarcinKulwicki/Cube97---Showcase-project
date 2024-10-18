using System;
using Cube.Data;
using Cube.Gameplay;
using UnityEngine;

namespace Cube.Controllers
{
    public class LevelController : MonoBehaviour
    {
        public Action<int> OnAppendScore;
        public LevelInitData InitData { get; private set; }    

        [SerializeField]
        LevelGenerator _levelGenerator;

        [SerializeField]
        int _dimension = 3, _step = 15;
        [SerializeField]
        Difficulty _difficulty = Difficulty.Easy;

        Level[] _levels;
        private int _moveCounter = 0;

        public void Generate()
        {
            Clear();

            InitData = new LevelInitData(_step, _dimension, _difficulty);
            _levels = _levelGenerator.Make(InitData);

            foreach (var item in _levels)
                item.OnNextAreaReached += AppendScore;
        }

        private void Clear()
        {
            if (_levels != null)
            {
                foreach (var item in _levels)
                {
                    item.OnNextAreaReached -= AppendScore;
                    Destroy(item.gameObject);
                }
            }
        }

        public void Reset()
        {
            _moveCounter = 0;
        }

        public void DeactivateAll()
        {
            foreach (var level in _levels)
                level.Deactivate();
        }

        private void AppendScore()
        {
            if (_moveCounter > 0)
                OnAppendScore?.Invoke(10);
            
            _moveCounter++;
        }
    }


    public class LevelInitData
    {
        public Vector3 StartPos;
        public Quaternion StartRot;
        public int Step;
        public int Dimension;
        public Difficulty Difficulty;

        public LevelInitData(int step, int dimension, Difficulty difficulty)
        {
            StartPos = new Vector3
            ( 
                UnityEngine.Random.Range(0, dimension) * step,
                0, 
                UnityEngine.Random.Range(0, dimension) * step
            ); 
            StartRot = Quaternion.identity;
            Step = step;
            Dimension = dimension;
            Difficulty = difficulty;
        }
    }
}

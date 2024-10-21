using System;
using Cube.Data;
using Cube.Gameplay;
using UnityEngine;

namespace Cube.Controllers
{
    public class LevelController : MonoBehaviour
    {
        #region Variables
        public Action<int> OnStageChanged;

        public LevelInitData InitData { get; private set; }   
        
        [Header("References")]
        [SerializeField]
        private LevelGenerator _levelGenerator;
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private GlobalData _globalData;

        [Header("Properties")]
        [SerializeField]
        private Difficulty _difficulty = Difficulty.Easy;
        [SerializeField]
        private int _step = 15;
        
        private Level[] _levels;
        private int _stage = 0;
        #endregion

        #region MonoBehaviour
        private void OnEnable() 
        {
            GameHandler.Instance.OnGameStart += GameStart;
            GameHandler.Instance.OnNextLevel += NextLevel;
            GameHandler.Instance.OnGameStop += GameStop;
        }

        private void OnDisable() 
        {
            GameHandler.Instance.OnGameStart -= GameStart;
            GameHandler.Instance.OnNextLevel -= NextLevel;
            GameHandler.Instance.OnGameStop -= GameStop;
        }
        #endregion

        private void GameStart()
        {
            TriggerLayer.Reset?.Invoke(); // Clear all collision information
            Generate(1);
            _playerController.Respawn(InitData);
            CameraControl.Instance.Move(InitData.StartPos);
        }

        private void NextLevel()
        {
            DeactivateAll();
            
            TriggerLayer.Reset?.Invoke(); // Clear all collision information
            Generate();
            _playerController.Respawn(InitData);
            CameraControl.Instance.Move(InitData.StartPos);
        }

        private void GameStop()
        {
            _stage = 0;
            OnStageChanged?.Invoke(_stage);

            DeactivateAll();
            CameraControl.Instance.Move(InitData.StartPos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stage">Pass positive number to generate specific Stage, otherwise will be next</param>
        private void Generate(int stage = -1)
        {
            if (stage < 0)
                _stage++;
            else
                _stage = stage;
            OnStageChanged?.Invoke(_stage);
            
            Clear();
            
            InitData = new LevelInitData(_step, _stage, _difficulty);
            _levels = _levelGenerator.Make(InitData);
        }

        private void DeactivateAll()
        {
            foreach (var level in _levels)
                level.Deactivate();
        }

        private void Clear()
        {
            if (_levels != null)
                foreach (var item in _levels)
                    Destroy(item.gameObject);
        }
    }

    public class LevelInitData
    {
        public Difficulty Difficulty;
        public Vector3 StartPos;
        public Quaternion StartRot;
        public int Step;
        public int Dimension;

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

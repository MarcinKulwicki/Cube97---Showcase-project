using System.Collections;
using Cube.Data;
using Cube.Gameplay;
using UnityEngine;

namespace Cube.Controllers
{
    public class LevelController : MonoBehaviour
    {
        #region Variables
        public LevelInitData InitData { get; private set; }   
        
        public int Stage 
        { 
            get { return _stage; }
            private set 
            {
                _globalData.GameData.SetLevelStage(value);
                _stage = value;
            }
        }

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
        private CoroutineContainer _gameLoop;
        private int _moveCounter = 0;
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

        //TODO It should be calculated somewhere else. Score should be increased only when player can move
        private IEnumerator GameLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                _globalData.GameData.AddScore(Stage);
            }
        }

        private void GameStart()
        {
            _moveCounter = 0;

            TriggerLayer.Reset?.Invoke(); // Clear all collision information
            Generate(1);
            _playerController.Respawn(InitData);
            CameraControl.Instance.Move(InitData.StartPos);

            _gameLoop = CoroutineContainer.Create(GameLoop());
        }

        private void NextLevel()
        {
            DeactivateAll();
            _moveCounter = 0;
            
            TriggerLayer.Reset?.Invoke(); // Clear all collision information
            Generate();
            _playerController.Respawn(InitData);
            CameraControl.Instance.Move(InitData.StartPos);
        }

        private void GameStop()
        {
            Stage = 0;

            DeactivateAll();
            CameraControl.Instance.Move(InitData.StartPos);

            _gameLoop.Interrupt();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stage">Pass positive number to generate specific Stage, otherwise will be next</param>
        private void Generate(int stage = -1)
        {
            if (stage < 0)
                Stage++;
            else
                Stage = stage;
            
            Clear();
            
            InitData = new LevelInitData(_step, Stage, _difficulty);
            _levels = _levelGenerator.Make(InitData);

            foreach (var item in _levels)
                item.OnNextAreaReached += AppendScore;
        }

        private void DeactivateAll()
        {
            foreach (var level in _levels)
                level.Deactivate();
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

        private void AppendScore()
        {
            if (_moveCounter > 0)
                _globalData.GameData.AddScore(10);
            
            _moveCounter++;
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
                Random.Range(0, dimension) * step,
                0, 
                Random.Range(0, dimension) * step
            ); 
            StartRot = Quaternion.identity;
            Step = step;
            Dimension = dimension;
            Difficulty = difficulty;
        }
    }
}

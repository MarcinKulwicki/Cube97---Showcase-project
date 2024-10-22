using System;
using System.Collections.Generic;
using Cube.Controllers;
using Cube.Data;
using UnityEngine;

namespace Cube.Gameplay
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private LevelDatabase[] _datas;
        [SerializeField]
        private Transform _lvlField;

        private Dictionary<Difficulty, LevelDatabase> _mappedLevels;

        private void Awake()
        {
            _mappedLevels = new Dictionary<Difficulty, LevelDatabase>();

            for (int i = 0; i < _datas.Length; i++)
                _mappedLevels.Add(_datas[i].Difficulty, _datas[i]);
        }

        public void OnPlayerExitField()
        {
            GameHandler.Instance.SetStatus(GameStatus.NextLevel);
        }

        public Level[] Make(LevelInitData data)
        {
            Level[] levels = new Level[data.Dimension * data.Dimension];

            int counter = 0;
            for (int i = 0; i < data.Dimension; i++)
                for (int j = 0; j < data.Dimension; j++)
                    levels[counter++] = Instantiate(GetRandom(data.Difficulty), new Vector3(i * data.Step, 0, j * data.Step), Quaternion.Euler(0, 45, 0), transform);

            float scale = data.Dimension * data.Step;
            float movePos = data.Step / 2f * (data.Dimension - 1);
            _lvlField.transform.localScale = new Vector3(scale, 0, scale);
            _lvlField.transform.position = new Vector3(movePos, 0, movePos);

            return levels;
        }

        private Level GetRandom(Difficulty difficulty) => _mappedLevels[difficulty].Prefabs[UnityEngine.Random.Range(0, _mappedLevels[difficulty].Prefabs.Length - 1)];
    }
}
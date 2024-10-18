using Cube.Gameplay;
using UnityEngine;

namespace Cube.Data
{
    [CreateAssetMenu(fileName = "LevelDatabase", menuName = "ScriptableObjects/Data/LevelDatabase_", order = 1)]
    public class LevelDatabase : ScriptableObject
    {
        [field: SerializeField] public Difficulty Difficulty { get; private set; }
        [field: SerializeField] public Level[] Prefabs { get; private set; }
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}



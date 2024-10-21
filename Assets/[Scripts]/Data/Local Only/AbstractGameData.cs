using System;

namespace Cube.Data
{
    public abstract class AbstractGameData
    {
        public Action<int> OnScoreChanged;
        
        public string UserName { get; protected set; }
        public int LevelStage { get; protected set; }

        public int Score
        {
            get { return _score; }
            protected set 
            {
                _score = value;
                OnScoreChanged?.Invoke(value);
            }
        }

        private int _score;
    }
}

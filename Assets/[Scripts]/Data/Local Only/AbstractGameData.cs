using System;

namespace Cube.Data
{
    public abstract class AbstractGameData
    {
        public Action<int> OnScoreChanged;
        public Action<int> OnLevelStageChanged;
        
        public string UserName { get; protected set; }

        public int LevelStage 
        { 
            get { return _levelStage; }
            protected set
            {
                _levelStage = value;
                OnLevelStageChanged?.Invoke(value);
            }
        }
        public int Score
        {
            get { return _score; }
            protected set 
            {
                _score = value;
                OnScoreChanged?.Invoke(value);
            }
        }

        private int _levelStage;
        private int _score;
    }
}

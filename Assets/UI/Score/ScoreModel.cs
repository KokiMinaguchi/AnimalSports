using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class ScoreModel : MonoBehaviour
    {
        public readonly int _maxScore = 999;
        private readonly SerializableReactiveProperty<int> _score = new(0);
        private readonly SerializableReactiveProperty<int> _preScore = new(0);
        public ReadOnlyReactiveProperty<int> Score => _score;
        public ReadOnlyReactiveProperty<int> PreScore => _preScore;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        public void AddScore(int value)
        {
            _preScore.Value = _score.Value;
            _score.Value = Mathf.Clamp(_score.Value + value, 0, _maxScore);
        }

        public void ResetScore()
        {
            _preScore.Value = 0;
            _score.Value = 0;
        }
    }
}

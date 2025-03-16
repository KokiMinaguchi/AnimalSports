using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// スコア処理
    /// </summary>
    public class ScoreModel : MonoBehaviour
    {
        public readonly int _maxScore = 999;
        // スコアのカウントアップアニメーションを実装するために現在のスコアと直前のスコアを管理
        private readonly SerializableReactiveProperty<int> _score = new(0);
        private readonly SerializableReactiveProperty<int> _preScore = new(0);
        public ReadOnlyReactiveProperty<int> Score => _score;
        public ReadOnlyReactiveProperty<int> PreScore => _preScore;

        /// <summary>
        /// スコア加算（減点するときはマイナスの値を入れる）
        /// </summary>
        /// <param name="value">得点</param>
        public void AddScore(int value)
        {
            _preScore.Value = _score.Value;
            _score.Value = Mathf.Clamp(_score.Value + value, 0, _maxScore);
        }
        /// <summary>
        /// スコアリセット
        /// </summary>
        public void ResetScore()
        {
            _preScore.Value = 0;
            _score.Value = 0;
        }
    }
}

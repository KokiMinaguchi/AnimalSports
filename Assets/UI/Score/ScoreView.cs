using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.UI;

namespace SleepingAnimals
{
    /// <summary>
    /// カウントアップアニメーション実装のためのカスタムトゥイーン
    /// </summary>
    public static class DOTweenEx
    {
        public static Tweener DOTextInt(this TextMeshProUGUI text, int initialValue, int finalValue, float duration, Func<int, string> convertor)
        {
            return DOTween.To(
                 () => initialValue,
                 it => text.text = convertor(it),
                 finalValue,
                 duration
             );
        }

        public static Tweener DOTextInt(this TextMeshProUGUI text, int initialValue, int finalValue, float duration)
        {
            return DOTextInt(text, initialValue, finalValue, duration, it => it.ToString());
        }
    }
    
    /// <summary>
    /// スコア表示
    /// </summary>
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        [Header("スコアテキスト")]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        [Range(0.1f, 5.0f)]
        [Header("カウントアップアニメーションのスピード")]
        private float _countUpAnimDuration;

        /// <summary>
        /// スコアをテキスト文字列にして表示
        /// </summary>
        /// <param name="currentScore">現在のスコア</param>
        /// <param name="value">加算後のスコア</param>
        public void SetText(int currentScore, int value)
        {
            _scoreText.DOTextInt(currentScore, value, _countUpAnimDuration);
        }
    }
}

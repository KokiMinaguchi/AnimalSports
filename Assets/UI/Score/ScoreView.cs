using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.UI;

namespace SleepingAnimals
{
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
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        [Range(0.1f, 5.0f)]
        private float _countUpAnimDuration;

        public void SetText(int currentScore, int value)
        {
            _scoreText.DOTextInt(currentScore, value, _countUpAnimDuration);
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// アイリスイン/アウトをコントロールする
    /// </summary>
    public class IrisShotController : MonoBehaviour
    {
        [SerializeField]
        [Header("マスクのトランスフォーム")]
        private RectTransform _unmask;

        readonly Vector2 IRIS_IN_SCALE = new Vector2(15, 15);
        readonly Vector2 IRIS_MID_SCALE1 = new Vector2(0.8f, 0.8f);
        readonly Vector2 IRIS_MID_SCALE2 = new Vector2(1.2f, 1.2f);

        public void IrisIn()
        {
            _unmask.DOScale(IRIS_MID_SCALE2, 0.4f).SetEase(Ease.InCubic); ;
            _unmask.DOScale(IRIS_MID_SCALE1, 0.2f).SetDelay(0.4f).SetEase(Ease.OutCubic);
            _unmask.DOScale(IRIS_IN_SCALE, 0.2f).SetDelay(0.6f).SetEase(Ease.InCubic);
        }

        public void IrisOut()
        {
            _unmask.DOScale(IRIS_MID_SCALE1, 0.2f).SetEase(Ease.InCubic);
            _unmask.DOScale(IRIS_MID_SCALE2, 0.2f).SetDelay(0.2f).SetEase(Ease.OutCubic);
            _unmask.DOScale(new Vector2(0, 0), 0.4f).SetDelay(0.4f).SetEase(Ease.InCubic);
        }
    }
}

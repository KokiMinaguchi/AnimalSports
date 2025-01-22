using CommonViewParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using DG.Tweening;
//using DG.Tweening;

[RequireComponent(typeof(CustomButton))]
public class BlinkButtonView : MonoBehaviour
{
    private CustomButton _button;
    [SerializeField]
    string _sceneName;

    public CanvasGroup _canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<CustomButton>();

        _button.OnButtonPressed.AsObservable().
            Subscribe(_ =>
            {
                transform.DOScale(0.95f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(0.8f, 0.24f).SetEase(Ease.OutCubic);
                // ƒV[ƒ“‘JˆÚ
                //SceneManagement.LoadScene(_sceneName);
                //this.transform.parent.gameObject.SetActive(false);
            })
            .AddTo(this);

        _button.OnButtonReleased.AsObservable()
            .Subscribe(_ =>
            {
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
            })
            .AddTo(this);
    }
}

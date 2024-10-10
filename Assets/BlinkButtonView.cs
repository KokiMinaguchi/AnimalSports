using CommonViewParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
//using DG.Tweening;

[RequireComponent(typeof(CustomButton))]
public class BlinkButtonView : MonoBehaviour
{
    private CustomButton _button;
    [SerializeField]
    string _sceneName;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<CustomButton>();

        _button.OnButtonClicked.AsObservable().
            Subscribe(_ =>
            {
                Debug.Log("Click!");
                // ÉVÅ[ÉìëJà⁄
                //SceneManagement.LoadScene(_sceneName);
                this.transform.parent.gameObject.SetActive(false);
            }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

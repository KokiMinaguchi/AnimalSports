using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// パネルがいくつ点灯しているかカウントする
    /// </summary>
    public class CheckLightUpPanelCount : MonoBehaviour
    {
        [SerializeField]
        [Header("ステージ")]
        private GameObject _stage;

        private LightUpPanel[] _lightUpPanels;

        [SerializeField]
        [Header("クリア演出キャンバス")]
        private GameObject _resultCanvas;

        // Start is called before the first frame update
        void Start()
        {
            // ステージパネルのスクリプトをすべて取得
            _lightUpPanels = new LightUpPanel[_stage.transform.childCount];
            for (int panelCnt = 0; panelCnt < _stage.transform.childCount; ++panelCnt)
            {
                // ステージパネルの子オブジェクトにスクリプトがついているので２回GetChild()している
                _lightUpPanels[panelCnt] = _stage.transform.GetChild(panelCnt).GetChild(0).GetComponent<LightUpPanel>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            var panelCnt = _lightUpPanels.Length;
            Debug.Log(panelCnt);
            
            // すべてのパネルが点灯していたらクリア
            // TODO UniRxで実装
            foreach (var panel in _lightUpPanels)
            {
                Debug.Log(panel);
                if(panel.IsLightUp)
                {
                    panelCnt--;
                    Debug.Log(panel.IsLightUp);
                }
            }
            if(panelCnt <= 0)
            {
                _resultCanvas.SetActive(true);
                Debug.Log("Clear");
            }
        }
    }
}

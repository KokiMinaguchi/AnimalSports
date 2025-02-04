using SleepingAnimals.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// ステージパネルを点灯、消灯する
    /// </summary>
    public class LightUpPanel : MonoBehaviour
    {
        [SerializeField]
        private Tag _tag;

        [SerializeField]
        [Header("点灯前のマテリアル")]
        private Color _normalColor = new Color(0.47f, 0.47f, 0.47f, 1.0f);

        [SerializeField]
        [Header("点灯後のマテリアル")]
        private Color _lightUpColor;

        // パネルが点灯しているか
        private bool _isLightUp = false;

        public bool IsLightUp { get { return _isLightUp; } }

        //private void Start()
        //{
        //    // debug
        //    this.GetComponent<MeshRenderer>().material.color = _lightUpColor;
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_tag.Name))
            {
                Material material = this.GetComponent<MeshRenderer>().material;
                // パネルを踏んだらマテリアルを変更
                if (_isLightUp)
                {
                    // パネルを消灯
                    material.color = _normalColor;
                    material.SetFloat("_Metallic", 0.0f);
                    Debug.Log("lightnormal");
                    _isLightUp = false;
                }
                else
                {
                    // パネルを点灯
                    material.color = _lightUpColor;
                    //material.SetFloat("_Metallic", 1.0f);
                    _isLightUp = true;
                }
            }
        }
    }
}

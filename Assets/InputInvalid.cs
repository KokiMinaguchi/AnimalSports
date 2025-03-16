using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// 入力を無効にする処理実装
    /// </summary>
    public class InputInvalid : MonoBehaviour
    {
        [SerializeField]
        [Header("プレイヤーのリジッドボディ")]
        private Rigidbody _rb;

        [SerializeField]
        private GameObject _eventSystem;

        /// <summary>
        /// 入力を無効にしてプレイヤーが移動しないようにする
        /// </summary>
        public void Invalid()
        {
            // プレイヤーのフリーズポジションをON、イベントシステムを非表示にして入力を無効にする
            _rb.constraints = RigidbodyConstraints.FreezeRotation  //Rotationを全てオン
            | RigidbodyConstraints.FreezePosition;  //Positionを全てオン
            _eventSystem.SetActive(false);
        }
    }
}

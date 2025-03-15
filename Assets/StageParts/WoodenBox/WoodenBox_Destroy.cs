using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace SleepingAnimals
{
    public class WoodenBox_Destroy : MonoBehaviour
    {
        [SerializeField]
        [Header("木の破片をフェードして消すまでの時間")]
        [Range(1.0f,5.0f)]
        float _destroyTime;
        private void OnEnable()
        {
            // アクティブ時に破壊
            destroyObject();
        }

        public void destroyObject()
        {
            var random = new System.Random();
            var min = -5;
            var max = 5;
            // 木の破片オブジェクトに破壊の物理挙動をさせる
            gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
                r.isKinematic = false;
                r.transform.SetParent(null);
                var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));
                r.AddForce(vect, ForceMode.Impulse);
                //r.AddExplosionForce(10f, transform.position, 10f);
                r.AddTorque(vect, ForceMode.Impulse);
                // 破片をフェードさせてから消す
                r.gameObject.GetComponent<Renderer>().material.DOFade(0.0f, _destroyTime);
                Destroy(r.gameObject, _destroyTime);
            });
            Destroy(gameObject);
        }
    }
}

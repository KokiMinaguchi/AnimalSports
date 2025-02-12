using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;
using UnityEngine.VFX;
using SleepingAnimals.Core;

namespace SleepingAnimals
{
    public class PopEffect : MonoBehaviour
    {
        [SerializeField]
        private Tag _tag;

        private VisualEffect _effect;

        // Start is called before the first frame update
        void Start()
        {
            _effect = GetComponent<VisualEffect>();
            _effect.Stop();
            this.OnTriggerEnterAsObservable()
                .Where(value => value.gameObject.CompareTag(_tag.Name))
                .Subscribe(_ =>
                {
                    _effect.Reinit();
                    _effect.Play();
                })
                .AddTo(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    [RequireComponent(typeof(CustomButton))]
    public class BaseButton : MonoBehaviour
    {
        protected CustomButton _button;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            _button = GetComponent<CustomButton>();
        }
    }
}
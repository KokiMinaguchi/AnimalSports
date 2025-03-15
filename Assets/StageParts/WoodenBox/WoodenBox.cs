using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class WoodenBox : MonoBehaviour
    {
        [SerializeField]
        private ItemInfo.ItemType _item;
        // Start is called before the first frame update
        void Start()
        {
            _item = ItemInfo.ItemType.None;
        }

        public ItemInfo.ItemType Item { set => _item = value; get => _item; }
    }
}

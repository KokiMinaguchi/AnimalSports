using SleepingAnimals.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class LightUpPanel : MonoBehaviour
    {
        [SerializeField]
        private Tag _tag;

        [SerializeField]
        private Material _normalMaterial;

        [SerializeField]
        Material _lightUpMaterial;

        private bool _isLightUp = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.gameObject.tag);
            if (other.gameObject.CompareTag(_tag.Name))
            {
                //Material material = other.gameObject.GetComponent<MeshRenderer>().material;
                //Debug.Log(material.ToString());
                //Debug.Log(other.gameObject.GetComponent<MeshRenderer>().material.name);
                //Debug.Log(_tag.Name);
                if (_isLightUp)
                {
                    this.gameObject.GetComponent<MeshRenderer>().material = _normalMaterial;
                    //other.gameObject.GetComponent<MeshRenderer>().material = _normalMaterial;
                    Debug.Log("lightnormal");
                    _isLightUp = false;
                }
                else
                {
                    this.gameObject.GetComponent<MeshRenderer>().material = _lightUpMaterial;
                    //other.gameObject.GetComponent<MeshRenderer>().material = _lightUpMaterial;
                    _isLightUp = true;
                }
            }
        }
    }
}

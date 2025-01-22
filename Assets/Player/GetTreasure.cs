using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SleepingAnimals.Core;

public class GetTreasure : MonoBehaviour
{
    [SerializeField]
    private Tag _tag;
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
        if (other.gameObject.CompareTag(_tag.Name))
        {
            Destroy(other.gameObject);
            Debug.Log("Get:"+other.gameObject.name);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDropItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("DropItem"))
            {
                Destroy(collision.gameObject);
                Debug.Log("Getitem:" + collision.gameObject.name);
            }
        }
    }
}

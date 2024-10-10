using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                Destroy(this.gameObject);
                // ゴール演出などの処理
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puck : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        //this.GetComponent<Collider>().isTrigger = false;
        rb.AddForce((transform.position - collision.transform.position).normalized * 5f, ForceMode.Impulse);
    }
}

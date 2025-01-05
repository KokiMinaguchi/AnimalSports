using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

[RequireComponent(typeof(PlayerInputProvider))]
public class playerMoveTest : MonoBehaviour
{
    PlayerInputProvider input;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInputProvider>();
        input.Move.Where(x => x.magnitude > 0.5f).Subscribe(_ => Debug.Log("MOVE!"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

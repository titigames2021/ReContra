using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public Transform playerTrans;
    public float cameraspeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTrans.position.x >= transform.position.x)
        {
            transform.Translate(Vector3.right*Time.deltaTime*cameraspeed);
        }
    }
}

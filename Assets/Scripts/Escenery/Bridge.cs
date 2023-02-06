using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Transform playerTrans;
    public GameObject _bridge;
    public GameObject falseBridge;
  
   
    // Update is called once per frame
    void Update()
    {
        if (playerTrans.position.x >= transform.position.x - 2.0f)
        {
            _bridge.SetActive(true);
            falseBridge.SetActive(false);
        }

    }
}

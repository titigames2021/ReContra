using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    //esta clase esta pensada para instanciar las balas repetidamente "de una"
    public float fireTime = .05f;
    void Start()
    {
        InvokeRepeating("Fire", fireTime, fireTime);

       
    }
    void Fire()
    {
        GameObject obj = ObjectPoolerScript.current.GetPooledObject();
        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }
}

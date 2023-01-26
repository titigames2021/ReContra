using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    //cuando se active el objeto activa la funcincion destroy que desactiva el objeto
     void OnEnable()
    {
        Invoke("Destroy", 2f);
    }

     void Destroy()
    {
        gameObject.SetActive(false);
    }
    //para evitar que haga invoke un objeto desactivado
    void OnDisable()
    {
        CancelInvoke();
    }







}

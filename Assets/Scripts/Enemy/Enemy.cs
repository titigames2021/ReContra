using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float life;
    public float speed;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "bullet")
        {
            life--;
        }



        if (life <= 0)
        {
            gameObject.SetActive(false);
        }



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float life;
    public float speed;
    Collider enemyCollider;

    private void Start()
    {
        enemyCollider= GetComponent<Collider>();
    }


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

        if (collision.gameObject.tag == "player")
        {
            enemyCollider.isTrigger= true;
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            enemyCollider.isTrigger = false;
        }
    }
}

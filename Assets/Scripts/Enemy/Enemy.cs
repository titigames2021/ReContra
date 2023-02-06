using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float life;
    public float speed;
    public Collider enemyCollider;



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "bullet")
        {
            life--;
        }
        if (collision.gameObject.tag == "bullet2")
        {
            life = life - 2;
        }



        if (life <= 0)
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "r" || collision.gameObject.tag == "s" || collision.gameObject.tag == "b" || collision.gameObject.tag == "f" || collision.gameObject.tag == "l" || collision.gameObject.tag == "m")
        {
            enemyCollider.isTrigger= true;
            
        }


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "r" || other.gameObject.tag == "s" || other.gameObject.tag == "b" || other.gameObject.tag == "f" || other.gameObject.tag == "l" || other.gameObject.tag == "m")
        {
            enemyCollider.isTrigger = false;
        }
        
       
    }
}

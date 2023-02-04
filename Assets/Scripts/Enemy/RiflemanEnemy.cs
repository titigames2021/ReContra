using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RiflemanEnemy : Enemy
{
    public Transform playerTransform;
    public Sprite pointRight;
    public SpriteRenderer enemySprite;
    public Sprite pointUp;
    public List<Transform> throwpoints;
    public Transform throwpoint;
    public GameObject riflemanBullet;
    public float timeShoot;
    public ObjectPoolerScript pool;
    public float shootWaitTime;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        enemySprite=GetComponent<SpriteRenderer>();
        
        enemyCollider=GetComponent<Collider>();
        coroutine = Shoot(shootWaitTime);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        

        if(playerTransform.position.x>= transform.position.x)
        {


            enemySprite.flipX= true;

            
        }
        else
        {
            enemySprite.flipX= false;
            
        }


        if (playerTransform.position.y > transform.position.y)
        {

            

            enemySprite.sprite = pointUp;

            if (enemySprite.flipX)
            {
                throwpoint = throwpoints[1];
            }
            else
            {
                throwpoint = throwpoints[0];
            }
              
            
          
        }
        else
        {
            
            
            enemySprite.sprite = pointRight;

            if (enemySprite.flipX)
            {
                throwpoint = throwpoints[3];
            }
            else
            {
                throwpoint = throwpoints[2];
            }

        }

        timeShoot += Time.deltaTime;



       

        



    }

    private IEnumerator Shoot(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
           
            GameObject obj = pool.GetPooledObject();
            //if (obj == null) return;

            Debug.Log("RILESHOOT");

            obj.transform.position = throwpoint.position;
            obj.transform.rotation = throwpoint.rotation;
            obj.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    public Transform throwpoint;
    public ObjectPoolerScript pool;
    private IEnumerator coroutine;
    public float shootwaitTime;
    public Transform playerTransform;
    public float activationDistance;
    Animator animator;
    public bool _playerIsClose;
    bool isCoroutineStarted;
    public List<Transform> throwpoints;
    public float shootUpRange;
    SpriteRenderer enemySpriteTurret;
    public List<Sprite> sprites;
    public Vector3 Vrange = Vector3.zero;
    public Vector3 Hrange = Vector3.zero;
    public float maxH;
    public float minH;
    public float plus;
    public bool lookUp;
    public bool looKUpL;
    public bool lookUpR;


    // Start is called before the first frame update
    void Start()
    {

        coroutine = Shoot(shootwaitTime);
       
        

        enemySpriteTurret = GetComponent<SpriteRenderer>();



        enemyCollider = GetComponent<Collider>();


    }

    // Update is called once per frame
    private void Update()
    {
        if(Vector3.Distance(playerTransform.position, transform.position) < activationDistance)
        {
           
           
            if (!isCoroutineStarted)
            {
                StartCoroutine(coroutine);
            }





        }
        else
        {
            if (isCoroutineStarted)
            {
                StopCoroutine(coroutine);
                isCoroutineStarted = false;
            }
        }


        if (playerTransform.position.y > transform.position.y + plus)
        {

            if (playerTransform.position.x < transform.position.x - plus)
            {
                enemySpriteTurret.sprite = sprites[1];
                throwpoint = throwpoints[1];
            }
            else if (playerTransform.position.x > transform.position.x + plus)
            {

                enemySpriteTurret.sprite = sprites[3];
                throwpoint = throwpoints[3];

            }
            else
            {
                enemySpriteTurret.sprite = sprites[2];
                throwpoint = throwpoints[2];
            }


        }else if (playerTransform.position.y < transform.position.y - plus)
        {
            if (playerTransform.position.x < transform.position.x - plus)
            {
                enemySpriteTurret.sprite = sprites[9];
                throwpoint = throwpoints[9];
            }
            else if (playerTransform.position.x > transform.position.x + plus)
            {

                enemySpriteTurret.sprite = sprites[7];
                throwpoint = throwpoints[7];


            }
            else
            {
                enemySpriteTurret.sprite = sprites[8];
                throwpoint = throwpoints[8];
            }
        }
        else 
        {
            if (playerTransform.position.x < transform.position.x - plus)
            {
                enemySpriteTurret.sprite = sprites[11];
                throwpoint = throwpoints[11];
            }

            if (playerTransform.position.x > transform.position.x + plus)
            {
                enemySpriteTurret.sprite = sprites[5];
                throwpoint = throwpoints[5];
            }
            
            
           
        }
        



        
        











    }

    
   
    private IEnumerator Shoot(float waitTime)
    {
        isCoroutineStarted = true;
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

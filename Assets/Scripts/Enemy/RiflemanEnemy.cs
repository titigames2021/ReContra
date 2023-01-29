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
    // Start is called before the first frame update
    void Start()
    {
        enemySprite=GetComponent<SpriteRenderer>();
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

              
            
          
        }
        else
        {
            enemySprite.sprite = pointRight;
           
        }






    }
}

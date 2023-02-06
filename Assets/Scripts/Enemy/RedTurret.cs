using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTurret :Enemy
{
    public ObjectPoolerScript pool;
    private IEnumerator coroutine;
    public Transform playerT;
    public float firstlimit;
    public float secondlimit;
    public SpriteRenderer enemySpriteRedTurret;
    public Sprite onelimit;
    public Sprite twolimit;
    public Sprite left;
    Transform throwpoint;
    public List<Transform> throwpoints;
    public float shootwaitTime;
    public bool isCoroutineStarted;
    // Start is called before the first frame update
    void Start()
    {
        enemySpriteRedTurret = GetComponent<SpriteRenderer>();
        coroutine = Shoot(shootwaitTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCoroutineStarted)
        {
            StartCoroutine(coroutine);
        }

        if (playerT.position.x >= transform.position.x-firstlimit)
        {
            

            enemySpriteRedTurret.sprite = onelimit;
            throwpoint = throwpoints[1];

            if (playerT.position.x >= transform.position.x - secondlimit)
            {
                enemySpriteRedTurret.sprite = twolimit;
                throwpoint = throwpoints[2];
            }
        }
        else
        {
            enemySpriteRedTurret.sprite = left;
            throwpoint = throwpoints[0];
        }




    }



    private IEnumerator Shoot(float waitTime)
    {
        isCoroutineStarted = true;
        while (true)
        {

            yield return new WaitForSeconds(waitTime);

            GameObject obj = pool.GetPooledObject();
            

            obj.transform.position = throwpoint.position;
            obj.transform.rotation = throwpoint.rotation;
            obj.SetActive(true);


        }
    }
}

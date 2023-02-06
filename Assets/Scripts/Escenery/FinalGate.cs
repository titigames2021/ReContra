using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGate : MonoBehaviour
{
    private IEnumerator coroutine;
    public bool isCoroutineStarted;
    public float shootwaitTime;
    public float activationDistance;
    public Transform playerTransform;
    public Transform throwpoint;
    public Transform throwpoint2;
    public ObjectPoolerScript pool;
    public float impulse;
    public float impulse2;
    public float life;
    GameObject obj;
    GameObject obj2;
    public bool objMove;
    public GameObject gateBreaked;
    Collider col;


    // Start is called before the first frame update
    void Start()
    {
        coroutine = Shoot(shootwaitTime);
        col=GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) < activationDistance)
        {


            if (!isCoroutineStarted)
            {
                StartCoroutine(coroutine);
            }


        }

        if (life <= 0)
        {

            Debug.Log("Opengate");
            gateBreaked.SetActive(true);
            col.isTrigger= true;
            StopCoroutine(coroutine);
        }

      

    }

    private IEnumerator Shoot(float waitTime)
    {
        isCoroutineStarted = true;
        while (true)
        {

            yield return new WaitForSeconds(waitTime);

             obj = pool.GetPooledObject();
             
            //if (obj == null) return;

           

            obj.transform.position = throwpoint.position;
            obj.transform.rotation = throwpoint.rotation;
            obj.SetActive(true);





            obj2 = pool.GetPooledObject();
            obj2.transform.position = throwpoint2.position;
            obj2.transform.rotation = throwpoint2.rotation;
            obj2.SetActive(true);

            objMove = true;





        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        life--;
    }

}

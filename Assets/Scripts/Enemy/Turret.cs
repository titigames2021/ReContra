using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    public Transform throwpoint;
    public ObjectPoolerScript pool;
    private IEnumerator coroutine;
    public float shootwaitTime;
    public Transform playerTrasnform;
    public float activationDistance;
    Animator animator;
    public bool _playerIsClose;
    public Animation animation;
    public float rayCastLength;
    public float stophplayer;
    // Start is called before the first frame update
    void Start()
    {

        coroutine = Shoot(shootwaitTime);
        StartCoroutine(coroutine);
        animator = GetComponent<Animator>();
       

    }

    // Update is called once per frame
    private void Update()
    {
        if(Vector3.Distance(playerTrasnform.position, transform.position) < activationDistance)
        {
            Debug.Log("playerCerca");
            animator.SetBool("playerIsClose", _playerIsClose);
            _playerIsClose = true;
            
        }

        if(playerTrasnform.position.y>stophplayer|| playerTrasnform.position.y < stophplayer)
        {
            animator.speed = 1.0f;
        }


        RaycastHit hit;
        int layerMask = 1 << 3;


        if (Physics.Raycast(throwpoint.position, Vector3.up, out hit, rayCastLength, layerMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            
            animator.speed = -1.0f;
            stophplayer = playerTrasnform.position.y;
        }


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

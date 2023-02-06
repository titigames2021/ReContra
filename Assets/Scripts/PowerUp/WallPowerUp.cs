using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WallPowerUp : DropPowerUp
{
    GameObject pu;
    private IEnumerator coroutine;
    public float impulse;
    public bool dropped;
    public bool stopMove;
    GameObject finalpu;
    Rigidbody rb;
    Collider col;
   
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(powerUps.Capacity);
         pu = powerUps[Random.Range(0,powerUps.Capacity)];
        coroutine = Fall(2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0 && dropped==false)
        {

            
            
             finalpu = Instantiate(pu, transform.position, Quaternion.identity);
             rb = finalpu.GetComponent<Rigidbody>();
            col = finalpu.GetComponent<Collider>();
            rb.AddForce(0.0f, impulse, 0.0f, ForceMode.Impulse);
            rb.AddForce( impulse-1.5f, 0.0f, 0.0f, ForceMode.Impulse);


            dropped = true;
        }

        if (dropped)
        {
           
            if (finalpu.transform.position.y >= 2.5f)
            {
                col.isTrigger= false;
                rb.useGravity= true;  
                Debug.Log("entra");
                impulse = 0.0f;
            }

        }






    }
    private void OnCollisionEnter(Collision collision)
    {
        life--;
    }
    private IEnumerator Fall(float waitTime)
    {
        
        while (true)
        {

            yield return new WaitForSeconds(waitTime);

           


        }
    }



}

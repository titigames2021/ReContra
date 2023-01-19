using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float translation;
    public Rigidbody rb;
    public float gravityForce;
    public float jumpForce;
    Collider fielcol;
    Collider playerCol;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        playerCol=GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        //Movimiento Horizontal mediane el uso de un Input Axis que indica la direccion del movimiento

        translation = Input.GetAxis("Horizontal") * speed*Time.deltaTime;

        transform.Translate(translation,0,0);

        gravityForce = 2.0f;


        //Salto 
         //IMPULSO

        if(Input.GetKeyDown("q"))
        {
            Debug.Log("jump");
            rb.AddForce(0.0f, jumpForce, 0.0f, ForceMode.Impulse);
            gravityForce = 0.0f;
        }
        if (Input.GetKeyDown("e"))
        {

            playerCol.isTrigger = true;
        }

       
       
         //G
        rb.AddForce(0.0f, -gravityForce, 0.0f, ForceMode.Force);


    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entro");
    }



    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Salio");
        other.isTrigger = false;
        fielcol = other;

    }
}

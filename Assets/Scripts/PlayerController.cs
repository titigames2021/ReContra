using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float translation;
    public Rigidbody rb;
     float gravityForce;
    public float setgravity;
    public float jumpForce;
    Collider fielcol;
    Collider playerCol;
    private LayerMask mask;
    public bool fallen;
    public float currentvelocity;
    public bool nomove;
    public bool raydownOff;
    public float rayDTime;
    public bool canJump;
   
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        playerCol=GetComponent<Collider>();
         mask = LayerMask.GetMask("platform");
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal mov
        translation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(translation, 0, 0);
        RaycastHit hit;
       
        Debug.Log(rb.velocity.y);



        //Jump
          //Cuando pulsas espacio, el jugador salta y envia un raycast hascia arriba haciendo el el objeto que recibe el rayo se vuelva trigger puediendo atravesar las plataformas 
         //Cuando terminas de atravesar una plataforma salta en OntriggerExit que devuelve la plataforma a isTRIGGER=false y asi el jugador no atraviesa la platraforma cuando cae 
         //Cuando pulsamos f hacemos que el jugador sea trigger por milesimas de segundo para poder atravesar y bajar de la plataforma

        if (Input.GetKeyDown(KeyCode.Space)& canJump)
        {
            canJump = false;
            rb.AddForce(0.0f, jumpForce, 0.0f, ForceMode.Impulse);
            if (Physics.Raycast(transform.position, Vector3.up, out hit, 10.0f))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);

                hit.collider.isTrigger = true;
                
            }


        }

        

        if (raydownOff){

            rayDTime += Time.deltaTime;

            if (rayDTime > 0.2f)
            {
                raydownOff = false;
                rayDTime = 0.0f;
                playerCol.isTrigger = false;
            }
        
        }

       
       


        if (Input.GetKeyDown("f"))
        {
            raydownOff = true;
            
            playerCol.isTrigger = true;
           
        }

        
        





        //Gravity
        rb.AddForce(0.0f, -gravityForce, 0.0f, ForceMode.Force);

        gravityForce = setgravity;



    }

    

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.isTrigger = false;
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float translation;
    public Rigidbody rb;
     public float gravityForce;
    public float setgravity;
    public float jumpForce;
    Collider fielcol;
    Collider playerCol;
    private LayerMask mask;
    public bool fallen;
    public float currentvelocity;
    public bool nomove;
    public bool raydownOff;
    public float triggerTime;
    public bool canJump;
    public Transform throwpoint;
    SpriteRenderer playerSprite;
    public Sprite spriteUP;
    public Sprite spriteUpRight;
    public Sprite spriteDown;
    public Sprite spriteDownRight;
    public Sprite spriteRight;
    public float lookAt;
    public bool canMove;
    public Collider downCollider;

    public bool lookDown;
    public bool lookUp;
    public bool lookRight;

    public List<Transform> throwpoints;

    public bool onAir;

    private PlayerInput _input;

    private Vector2 move_input_data;

    public float raycastlength;
    public Vector2 Vrange = Vector2.zero;
    public Vector2 Hrange = Vector2.zero;
    public Transform edgepoint;
    public float cameraspeed;
    public Transform camperaTrans;
    public GameObject falseBridge;

    public GameObject bridge;

    GameObject objpool;
    GameObject objpool2;
    GameObject objpool3;
    GameObject objpool4;
    SpriteRenderer objpoolSr;
    public Sprite spriteBigBullet;
    int pooln;

    public List<ObjectPoolerScript> pool;
    Rigidbody rbPool;
    public float impulseBullet;
    

    private void Awake()
    {
        pooln = 0;

        _input=new PlayerInput();


        _input.GeneralMovement.Move.performed += move_performed =>
        {

            move_input_data = move_performed.ReadValue<Vector2>();
            Debug.Log("Guapisimo");
        };



    }
   
    private void move_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Guapisimo");
        throw new NotImplementedException();
    }



    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        playerCol=GetComponent<Collider>();
         mask = LayerMask.GetMask("platform");
        playerSprite= GetComponent<SpriteRenderer>();
        canMove = true;
    }

    private void FixedUpdate()
    {
        //Gravity
        rb.AddForce(0.0f, -gravityForce, 0.0f, ForceMode.Force);
    }


    private void LateUpdate()
    {
        //limits of the player's area of movement
        gameObject.transform.position = new Vector3(

            Mathf.Clamp(transform.position.x,edgepoint.transform.position.x, Camera.main.transform.position.x+2.5f),
        Mathf.Clamp(transform.position.y, Vrange.x, Vrange.y),
            transform.position.z





            );
    }


    // Update is called once per frame
    void Update()
    {


        
        




        if (transform.position.x >= Camera.main.transform.position.x)
        {
            Camera.main.transform.Translate(Vector3.right * Time.deltaTime * cameraspeed);
        }




        //Horizontal Movement
        translation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        
            transform.Translate(translation, 0, 0);


        //El eje vertical hace que el jugador mire hacia arriba o hacia abajo y cambie el sprite a la situación

        lookAt = Input.GetAxis("Vertical");

        //Si el input horizontal es negativo apunta a la izq 
        if (translation < 0)
        {
            playerSprite.flipX = true;
            
           
        }
        
        if(translation> 0)
        {
            playerSprite.flipX = false;
            
        }

       

       //Si el input vertical es 1 el sprite cambiara a uno que este mirando hacia arriba y el throwpoint tomara la posicion que le corresponde y asi con el resto de movimientos
       //Tambien cambia el collider cuando el jugador toma la posicion de "tumbado"

        if (lookAt == 1)
        {
            playerSprite.sprite = spriteUP;
            
            downCollider.enabled = false;
            playerCol.enabled = true;
            lookUp = true;
            lookDown = false;
            if (playerSprite.flipX)
            {
                throwpoint = throwpoints[5];
            }
            else
            {
                throwpoint = throwpoints[4];
            }
            
        }
        else if (lookAt == -1)
        {
            lookUp = false;
            lookDown = true;
            canJump=false;
            playerSprite.sprite = spriteDown;
           
            playerCol.enabled = false;
            downCollider.enabled = true;
            
            if (playerSprite.flipX)
            {
                throwpoint = throwpoints[3];
            }
            else
            {
                throwpoint = throwpoints[2];
            }
            
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {


                downCollider.isTrigger = true;

            }

        }
        else
        {
            lookUp = false;
            lookDown = false;
            playerSprite.sprite = spriteRight;
            
            downCollider.enabled = false;
            playerCol.enabled = true;
            throwpoint = throwpoints[0];

            if (playerSprite.flipX)
            {
                throwpoint = throwpoints[1];
            }

        }

        
        if( lookDown && translation!=0)
        {
            playerSprite.sprite = spriteDownRight;
            if(translation < 0)
            {
                throwpoint = throwpoints[7];
            }

            if (translation > 0)
            {
                throwpoint = throwpoints[6];
            }
            
        }

        if (lookUp && translation != 0)
        {
            playerSprite.sprite = spriteUpRight;
            
            if (translation < 0)
            {
                throwpoint = throwpoints[9];
            }

            if (translation > 0)
            {
                throwpoint = throwpoints[8];
            }

        }

        //El jugador solo puede disparar hacia abajo en linea recta si esta saltando 
        if(lookDown && onAir)
        {
            throwpoint = throwpoints[10];
        }




        // Jump

        //Cuando pulsas espacio, el jugador salta y envia un raycast hascia arriba haciendo el el objeto que recibe el rayo se vuelva trigger puediendo atravesar las plataformas 
        //Cuando terminas de atravesar una plataforma salta en OntriggerExit que devuelve la plataforma a isTRIGGER=false y asi el jugador no atraviesa la platraforma cuando cae 
        //Cuando pulsamos f hacemos que el jugador sea trigger por milesimas de segundo para poder atravesar y bajar de la plataforma

        if (Input.GetKeyDown(KeyCode.Joystick1Button0)&&canJump){





                onAir = true;
                RaycastHit hit;
                canJump = false;
                rb.AddForce(0.0f, jumpForce, 0.0f, ForceMode.Impulse);
                if (Physics.Raycast(transform.position, Vector3.up, out hit, raycastlength))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);

                    hit.collider.isTrigger = true;
                

                }


            
        }


        //Hacemos que el jugador sea trigger por milesimas de segundo para poder atravesar una plataforma y bajar de esta 


       


        if (downCollider.isTrigger){

            triggerTime += Time.deltaTime;

            if (triggerTime > 0.5f)
            {
                downCollider.isTrigger= false;
                triggerTime = 0.0f;
                
            }
        
        }


        //Shoot


         //Cuando el jugador pulsa el boton de disparar llama a la funcion de la clase ObjectPooler la cual le retorna una bala inactiva para colocarla en el disparador y activarla 
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {

            
            if (pooln ==2 )
            {
                objpool2 = pool[1].GetPooledObject();
                if (objpool2 == null) return;
                objpool2.transform.position = throwpoint.position;
                objpool2.transform.rotation =throwpoint.rotation *Quaternion.Euler(0.0f, 0.0f, 30.0f);
                objpool2.SetActive(true);
                //
                objpool3 = pool[1].GetPooledObject();
                if (objpool3 == null) return;
                objpool3.transform.position = throwpoint.position;
                objpool3.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f) * throwpoint.rotation;
                objpool3.SetActive(true);
                //
                objpool3 = pool[1].GetPooledObject();
                if (objpool3 == null) return;
                objpool3.transform.position = throwpoint.position;
                objpool3.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -30.0f) * throwpoint.rotation;
                objpool3.SetActive(true);
                //
                objpool4 = pool[1].GetPooledObject();
                if (objpool4 == null) return;
                objpool4.transform.position = throwpoint.position;
                objpool4.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -60.0f) * throwpoint.rotation;
                objpool4.SetActive(true);
            }
            else
            {
                objpool = pool[pooln].GetPooledObject();
               
                if (objpool == null) return;

                objpool.transform.position = throwpoint.position;

                objpool.transform.rotation = throwpoint.rotation;

                

                objpool.SetActive(true);
               
            }


        }



    }

  


    //Solo puede saltar despues de pisar una plataforma
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            canJump = true;
            onAir = false;
        }

        

       
        switch (collision.gameObject.tag)
        {


            case "r":

                Debug.Log("r");

                pooln = 1;
                collision.gameObject.SetActive(false);

                break;
            case "s":

                Debug.Log("s");
                pooln = 2;
                collision.gameObject.SetActive(false);

                break;
            
        }




    }

    private void OnTriggerExit(Collider other)
    {
        other.isTrigger = false;
    }

}

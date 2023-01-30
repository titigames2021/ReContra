using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

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


    // Update is called once per frame
    void Update()
    {

        





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
                if (Physics.Raycast(transform.position, Vector3.up, out hit, 10.0f))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);

                    hit.collider.isTrigger = true;

                }


            
        }


        //Hacemos que el jugador sea trigger por milesimas de segundo para poder atravesar una plataforma y bajar de esta 


       


        if (downCollider.isTrigger){

            triggerTime += Time.deltaTime;

            if (triggerTime > 0.2f)
            {
                downCollider.isTrigger= false;
                triggerTime = 0.0f;
                
            }
        
        }


        //Shoot


         //Cuando el jugador pulsa el boton de disparar llama a la funcion de la clase ObjectPooler la cual le retorna una bala inactiva para colocarla en el disparador y activarla 
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {

            GameObject obj = ObjectPoolerScript.current.GetPooledObject();
            if (obj == null) return;

            obj.transform.position = throwpoint.position;
            obj.transform.rotation = throwpoint.rotation;
            obj.SetActive(true);


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

        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("EnemyCollision");

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        other.isTrigger = false;
    }

}

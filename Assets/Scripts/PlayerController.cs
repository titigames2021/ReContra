using UnityEngine;

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
    public float triggerTime;
    public bool canJump;
    public Transform throwpoint;
    SpriteRenderer sprite;
    public Sprite spriteUP;
    public Sprite spriteRigth;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        playerCol=GetComponent<Collider>();
         mask = LayerMask.GetMask("platform");
        sprite= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Gravity
        rb.AddForce(0.0f, -gravityForce, 0.0f, ForceMode.Force);

        gravityForce = setgravity;




        //Horizontal Movement
        translation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(translation, 0, 0);
        RaycastHit hit;
       
       



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



        //Hacemos que el jugador sea trigger por milesimas de segundo para poder atravesar una plataforma y bajar de esta 


        if (Input.GetKeyDown("f"))
        {


            playerCol.isTrigger = true;

        }


        if (playerCol.isTrigger){

            triggerTime += Time.deltaTime;

            if (triggerTime > 0.4f)
            {
                playerCol.isTrigger= false;
                triggerTime = 0.0f;
                playerCol.isTrigger = false;
            }
        
        }


        //Shoot


         //Cuando el jugador pulsa el boton de disparar llama a la funcion de la clase ObjectPooler la cual le retorna una bala inactiva para colocarla en el disparador y activarla 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            GameObject obj = ObjectPoolerScript.current.GetPooledObject();
            if (obj == null) return;

            obj.transform.position = throwpoint.position;
            obj.transform.rotation = throwpoint.rotation;
            obj.SetActive(true);


        }



        if (Input.GetKeyDown("p"))
        {
            sprite.sprite = spriteUP;
        }
        if (Input.GetKeyDown("l"))
        {
            sprite.sprite = spriteRigth;
        }

        









    }

    
    //Solo puede saltar despues de pisar una plataforma
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            canJump = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        other.isTrigger = false;
    }

}

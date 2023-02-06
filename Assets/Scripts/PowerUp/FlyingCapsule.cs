using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlyingCapsule : MonoBehaviour
{
    public float speed;
    public bool up;
    public bool down;
    public float limitU;
    public float limitD;
    public float life;
    public GameObject pu;
   
    public List<GameObject> powerUps;
    // Start is called before the first frame update
    void Start()
    {
        down = true;
        pu = powerUps[Random.Range(0, powerUps.Capacity)];
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right*speed*Time.deltaTime);
        if (up)
        {
            gameObject.transform.localPosition += new Vector3(0, 1) * speed * Time.deltaTime;
            gameObject.transform.position += new Vector3(1, 0) * speed * Time.deltaTime;
        }
        if (down)
        {
            gameObject.transform.localPosition += new Vector3(0, -1) * speed * Time.deltaTime;
            gameObject.transform.position += new Vector3(1, 0) * speed * Time.deltaTime;
        }

        if (gameObject.transform.localPosition.y >= limitU)
        {


            up = false;
            down = true;

        }

        if (gameObject.transform.localPosition.y <= limitD)
        {


            up = true;
            down = false;

        }
        if (life <= 0)
        {
            GameObject finalpu = Instantiate(pu, transform.position, Quaternion.identity);
           
            Collider col = finalpu.GetComponent<Collider>();
           
            col.isTrigger= false;

            Rigidbody rb = finalpu.GetComponent<Rigidbody>();

            rb.useGravity = true;

            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        life--;
    }
}
  

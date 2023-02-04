using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleHiddenMan : Enemy
{
    public bool toHide;
   
    public SpriteRenderer enemyVisibleSprite;
    public Collider enemyHideCollider;
    public float rateTime;
    public Transform throwpoint;
    public ObjectPoolerScript pool;
    private IEnumerator coroutine;
    public float shootwaitTime;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {


        InvokeRepeating("Visible", 0f, rateTime);

        coroutine = Shoot(shootwaitTime);
        col = GetComponent<Collider>();
       

    }

    //Metodo que desactiva o activa el sprite de "visible" y llama al metodo "Shoot" cuando el sprite "visible" este activado
    void Visible()
    {
       
        if(enemyVisibleSprite.enabled) {
            enemyVisibleSprite.enabled = false;
            StopCoroutine(coroutine);
            col.enabled = false;
        }
        else
        {
            enemyVisibleSprite.enabled = true;
            StartCoroutine(coroutine);
            col.enabled= true;



        }


    }
    //Metodo para disparar los proyectiles de una pool de objetos
    private IEnumerator Shoot(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
            GameObject obj = pool.GetPooledObject();
            //if (obj == null) return;



            obj.transform.position = throwpoint.position;
            obj.transform.rotation = throwpoint.rotation;
            obj.SetActive(true);
        }
    }
}

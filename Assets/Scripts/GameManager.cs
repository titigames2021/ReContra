using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public bool key;
    public bool gamepad;
    public int life;

    private void Awake()
    {
        
        if(instance!=null && instance != this)
        {
            Destroy(this);
            return;




        }
        instance= this;
         DontDestroyOnLoad(gameObject);



    }

    public void selectKeyBoard()
    {
        
        key = true;

        
    }

    public void selectGamepad()
    {

        gamepad= true;
    }


}

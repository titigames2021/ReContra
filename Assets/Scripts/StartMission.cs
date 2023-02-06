using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMission : MonoBehaviour
{
    // Start is called before the first frame update

    private void Update()
    {
        //presionar start
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }


}

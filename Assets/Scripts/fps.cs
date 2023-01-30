using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class fps : MonoBehaviour
{
    public TextMeshProUGUI tfps;
   
    public float time;
    public int frameCount;
    public float pollingTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            tfps.text = frameRate.ToString()+ "FPS";

        }
    }
}

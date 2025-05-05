using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenAdapter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        
            int screenWidth = Display.main.systemWidth;
            int screenHeight = Display.main.systemHeight;

            Screen.SetResolution(screenWidth, screenHeight, false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

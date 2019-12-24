using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nil_Sound_Maker : MonoBehaviour
{

    

    public bool isRunning;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;

        }
        else
        {
            isRunning = false;
        }





    }
}


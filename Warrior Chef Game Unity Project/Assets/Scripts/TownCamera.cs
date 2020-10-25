using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCamera : MonoBehaviour
{

    Camera mainCam;
    Camera townCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        townCam = GameObject.Find("Camera").GetComponent<Camera>();

        townCam.enabled = true;
        mainCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

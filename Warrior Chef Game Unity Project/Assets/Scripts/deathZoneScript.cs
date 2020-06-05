using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZoneScript : MonoBehaviour
{
    public playerController playerc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        Debug.Log(hitInfo);
    playerController player = hitInfo.GetComponent<playerController>();
        
        
            playerc.currentHealth = 0;
        
    }
}

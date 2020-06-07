using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meatDropScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //FindObjectOfType<playerController>().GainMeatStock();
    //Destroy(gameObject);
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
         playerController player = hitInfo.GetComponent<playerController>();
        if (player != null)
        {
            player.GainMeatStock();
            Destroy(gameObject);
        }
    }
}

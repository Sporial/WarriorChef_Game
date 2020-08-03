using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meatDropScript : MonoBehaviour
{

    //FindObjectOfType<playerController>().GainMeatStock();
    //Destroy(gameObject);

    // script for the meat drops from enemies
    //if the player picks up the object it adds to the meatstock counter on the playerController and destroys itself
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

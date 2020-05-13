using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public int damage = 10;
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
         enemyController enemy = hitInfo.GetComponent<enemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
     }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public int baseDamage = 1;
    static public int upgradeDamage = 0;
    public int totalDamage;
    // Start is called before the first frame update
    void Start()
    {
        UpdateDamage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDamage()
    {
        totalDamage = baseDamage + upgradeDamage;
    }

    public void UpgradeDamageBy(int upgrade)
    {
        upgradeDamage = upgradeDamage + upgrade;
        UpdateDamage();
    }

     void OnTriggerEnter2D(Collider2D hitInfo)
     {
         enemyController enemy = hitInfo.GetComponent<enemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(totalDamage);
        }
     }
}

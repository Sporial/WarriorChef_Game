using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public int health = 20;

    private Rigidbody2D rb;

    

    //what to do when I take damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //tell player to get meatstock
            FindObjectOfType<playerController>().GainMeatStock();

            Destroy(gameObject);

        }
    }
}

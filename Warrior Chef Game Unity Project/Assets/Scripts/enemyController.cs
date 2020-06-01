using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public int health = 20;

    public float speed = 10f;

    private Rigidbody2D rb;

    public Animator animator;

    public Transform target;
    public float maxDistance = 1;
    public float minDistance = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Vector2 vel = rb.velocity;
        //float xVel = vel.x;
        //animator.SetFloat("Speed", Mathf.Abs(xVel));
        transform.LookAt(target.position);
         transform.Rotate(new Vector3(0,90,0),Space.Self);
        //move towards the player
         if (Vector3.Distance(transform.position, target.position) >= minDistance)
         {  
            transform.Translate(new Vector3(-1 * speed * Time.deltaTime,0,0) );
            animator.SetFloat("Speed", Mathf.Abs(speed * Time.deltaTime));
         }    
            //shoots toward player every so often
            if (Vector3.Distance(transform.position, target.position) <= maxDistance)
            {  
                Attack();
            }
    }

     public void Attack()
   {
       animator.SetTrigger("Attack");
   }

    

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

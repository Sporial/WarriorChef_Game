using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public int health = 4;

    public int damage = 1;

    //basically to test whether the enemy is alive to attack vs wanting to play a death anim etc.
    private bool alive;

    public float baseSpeed = 1f;
    public float currentSpeed;

    private Rigidbody2D rb;

    public Animator animator;

    //used for dropping the meat the player picks up
    public GameObject meatDrop;
    private Vector3 dropPos;

    //all to for finding the player and moving/attacking toward them
    public playerController player;
    public Transform target;
    public float maxDistance = 1f;
    public float minDistance = 1f;
    public float attackRange = 1f;

    //allows for attack cooldowns, basically so they can't spam their attacks
    public float attackDelay = 2f;
    private float attackCooldown;
    

    void Start()
    {
        attackCooldown = attackDelay;
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
        alive = true;
        //for meatdrop
        dropPos = new Vector3(0f, 0.7f, 0f);
    }

    void FixedUpdate()
    {
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }
        //Vector2 vel = rb.velocity;
        //float xVel = vel.x;
        //animator.SetFloat("Speed", Mathf.Abs(xVel));
        transform.LookAt(target.position);
         transform.Rotate(new Vector3(0,90,0),Space.Self);
        //move towards the player
         if (Vector3.Distance(transform.position, target.position) >= minDistance && alive)
         {  
            transform.Translate(new Vector3(-1 * currentSpeed * Time.deltaTime,0,0) );
            animator.SetFloat("Speed", Mathf.Abs(currentSpeed * Time.deltaTime));
         }    
            //attack toward player every so often
            if (Vector3.Distance(transform.position, target.position) <= maxDistance && attackCooldown <= 0f && alive)
            {  
                Attack();
                attackCooldown = attackDelay;
            }
    }

     public void Attack()
   {
       currentSpeed = 0f;
       animator.SetTrigger("Attack");
       if(Vector3.Distance(transform.position, target.position) < attackRange)
       {
           player.TakeDamage(damage);
       }
       StartCoroutine(AttackWait());
       
   }

    //all for waiting between other events
    IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(1f);
        currentSpeed = baseSpeed;
    }
    
    IEnumerator DamagedWait()
    {
        yield return new WaitForSeconds(0.5f);
        currentSpeed = baseSpeed;
    }
    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(meatDrop, transform.position + dropPos, transform.rotation);
        Destroy(gameObject);
    }
    

    //what to do when I take damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && alive)
        {
            //tell player to get meatstock
            //FindObjectOfType<playerController>().GainMeatStock(); -depreciated, now drops meat which when touched does the same thing.
            animator.SetTrigger("Death");
            alive = false;
            //waits to die, no longer alive, so only death aniamtion plays until destroyed
            StartCoroutine(DeathWait());
        }
        //allows the player to 'stun' the enemy, returns to normal behaviour after DamagedWait()
        currentSpeed = 0f;
        animator.SetTrigger("Damaged");
        StartCoroutine(DamagedWait());
    }
}

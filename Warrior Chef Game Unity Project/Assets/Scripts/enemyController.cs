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

    public Transform groundDetection;
    private Vector3 groundDirection;
    public float groundDetectionDistance = 0.25f;
    public bool isTouchingGround;

    //all to for finding the player and moving/attacking toward them
    public playerController player;
    public Transform target;
    public bool isFlipped = false;
    private bool facingRight = false;
    public int facingRightNum = -1;

    public float maxDistance = 1f;
    public float minDistance = 1f;
    public float attackRange = 1f;

    public float losDistance = 3f;
    private int patrolWait;

    public bool isFlyer;
    public bool canLookDown;
    public bool startOnRoof;
    public bool isFrog;
    public float jumpStrength = 1;
    //public float jumpWait = 1f;
    

    //allows for attack cooldowns, basically so they can't spam their attacks
    public float attackDelay = 2f;
    private float attackCooldown;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Physics2D.queriesStartInColliders = false;

        attackCooldown = attackDelay;
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
        alive = true;
        //for meatdrop
        dropPos = new Vector3(0f, 0.7f, 0f);
        //groundDetection = new Vector3(-0.25f, 0.1f, 0f);
        groundDirection = new Vector3(-0.25f, 0f, 0f);
    }

    void FixedUpdate()
    {
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }

        //they ray cast to detect if the player is within line of sight of the enemy
        if (canLookDown == false)
        {
        RaycastHit2D losInfo = Physics2D.Raycast(transform.position + dropPos, transform.right * facingRightNum, losDistance);
        if (losInfo.collider !=null)
        {
            

            if (losInfo.collider.CompareTag("Player"))
            {
                Debug.DrawLine(transform.position + dropPos, losInfo.point, Color.red);
                animator.SetBool("isAlerted", true);
            }
            else
            {
                Debug.DrawLine(transform.position + dropPos, losInfo.point, Color.blue);
            }

        }
        else
        {
            Debug.DrawLine(transform.position + dropPos, transform.position + dropPos + transform.right * losDistance * facingRightNum, Color.green);
        }
        }
        else
        {
            RaycastHit2D losInfo = Physics2D.Raycast(transform.position, transform.up * -1, losDistance);
        if (losInfo.collider !=null)
        {
            

            if (losInfo.collider.CompareTag("Player"))
            {
                Debug.DrawLine(transform.position, losInfo.point, Color.red);
                animator.SetBool("isAlerted", true);
            }
            else
            {
                Debug.DrawLine(transform.position, losInfo.point, Color.blue);
            }

        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.up * losDistance * -1, Color.green);
        }
        }

        //rays to detect whether there is ground to walk on, and if there is an enemy or wall in fron of them *make sure to tage tiles "Ground" and enemies "Enemy"
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, transform.up * -1, groundDetectionDistance);
        Debug.DrawLine(groundDetection.position, groundDetection.position + transform.up * -1 * groundDetectionDistance, Color.yellow);
        RaycastHit2D infrontInfo = Physics2D.Raycast(groundDetection.position + transform.up * 0.3f, transform.up * 0.3f + transform.right * facingRightNum, groundDetectionDistance * 2f);
        Debug.DrawLine(groundDetection.position + transform.up * 0.3f, groundDetection.position + transform.up * 0.3f + transform.right * facingRightNum * groundDetectionDistance * 2f, Color.yellow);
        if (groundInfo.collider == false && isFlyer == false && isFrog == false)
        {
            Flip();
        }
        if (groundInfo.collider == false)
        {
            isTouchingGround = false;
        }
        else
        {
            isTouchingGround = true;
        }
        if (infrontInfo.collider != null)
        {
            if (infrontInfo.collider.CompareTag("Enemy") || infrontInfo.collider.CompareTag("Ground"))
            {
                Flip();
            }
        }
        //Vector2 vel = rb.velocity;
        //float xVel = vel.x;
        //animator.SetFloat("Speed", Mathf.Abs(xVel));
        
        
        //transform.LookAt(target.position);
         //transform.Rotate(new Vector3(0,90,0),Space.Self);
        //move towards the player
         //if (Vector3.Distance(transform.position, target.position) >= minDistance && alive)
         //{  
           // transform.Translate(new Vector3(-1 * currentSpeed * Time.deltaTime,0,0) );
            //animator.SetFloat("Speed", Mathf.Abs(currentSpeed * Time.deltaTime));
         //}    
            //attack toward player every so often
            if (Vector3.Distance(transform.position, target.position) <= maxDistance && attackCooldown <= 0f && alive && isTouchingGround)
            {  
                Attack();
                attackCooldown = attackDelay;
            }
    }

    //using a similar script to the playerController -most of the work is done by the 'followBehaviour' statemachine
    public void LookAtPlayer()
    {
        //Vector3 flipped = transform.localScale;
        //flipped.z *= -1f;

        if (transform.position.x > target.position.x && facingRight)
        {
            Flip();
        }
        else if (transform.position.x < target.position.x && !facingRight)
        {
            Flip();
        }


    }

    public void Flip()
    {
        //to flip sprite to face movement
        facingRight = !facingRight;
        facingRightNum *= -1;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
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
    
    //these are so that the statmachines can run the coroutines to swap between idling and wandering, as state machines apparently can't StartCoroutine()s
    public void RunPatrolWait()
    {
        StartCoroutine(PatrolWait());
    }
    public void RunIdleWait()
    {
        StartCoroutine(IdleWait());
    }
    public void RunFlipWait()
    {
        StartCoroutine(FlipWait());
    }
    //public void RunJumpWait()
    //{
    //    StartCoroutine(JumpWait());
    //}

    //these are basically just a random duration for the AI to perform each of their behaviours.
    IEnumerator PatrolWait()
    {
        patrolWait = Random.Range(5,15);
        yield return new WaitForSeconds(patrolWait);
        animator.SetBool("Idle", true);
    } 
    IEnumerator IdleWait()
    {
        patrolWait = Random.Range(2,5);
        yield return new WaitForSeconds(patrolWait);
        if (isFrog == false)
        {
        animator.SetBool("Idle", false);
        }
        else
        {
            Jump();
        }
    }
    IEnumerator FlipWait()
    {
        patrolWait = Random.Range(2,15);
        yield return new WaitForSeconds(patrolWait);
        Flip();
    }
    //IEnumerator JumpWait()
    //{
    //    yield return new WaitForSeconds(jumpWait);
    //    Jump();
    //}

    public void SetGravity(float gravityNum)
    {
        rb.gravityScale = gravityNum;
    }


    public void Jump()
    {
        animator.SetTrigger("Jump");
        rb.velocity = (Vector2.up + (Vector2.right * facingRightNum)) * jumpStrength;
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public int health = 50;

    public int bossDamage = 1;
    
    private bool alive;
    public float baseSpeed = 1f;
    public float currentSpeed;

    private Rigidbody2D rb;

    public Animator animator;

    public GameObject meatDrop;
    private Vector3 dropPos;

    public GameObject corpsePrefab;

    public playerController player;
    public Transform target;

    private playerController playerHit;

    public bool isFlipped = false;
    private bool facingRight = false;
    public int facingRightNum = -1;

    public float maxDistance = 1f;
    public float minDistance = 1f;
    //public float attackRange = 10f;

    public float jumpStrength = 1;

    public float losDistance = 20f;

    public int atkNum;

    public bool isAttacking = false;

    public float attackDelay = 2f;
    private float attackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Physics2D.queriesStartInColliders = false;

        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
        alive = true;

        dropPos = new Vector3(0f, 1f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }

        RaycastHit2D losInfo = Physics2D.Raycast(transform.position + dropPos, transform.right * facingRightNum, losDistance);
        if (losInfo.collider !=null)
        {
            

            if (losInfo.collider.CompareTag("Player") || losInfo.collider.CompareTag("Player"))
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

        if (Vector3.Distance(transform.position, target.position) <= maxDistance && attackCooldown <= 0f && alive)
        {  
                Attack();
                attackCooldown = attackDelay;
        }
        else if (Vector3.Distance(transform.position, target.position) >= (maxDistance * 1.5f) && attackCooldown <= 0f && alive)
        {  
                animator.SetTrigger("Roll");
                attackCooldown = attackDelay;
        }
    }

    public void LookAtPlayer()
    {
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
       isAttacking = true;
       atkNum = Random.Range(1,6);
       animator.SetInteger("attackNum", atkNum);
       /*if(Vector3.Distance(transform.position, target.position) < attackRange)
       {
           player.TakeDamage(damage);
       }
       */
       StartCoroutine(AttackWait());
       
    }

    public void OnCollisionEnter2D(Collider2D hitInfo)
    {
        playerController playerHit = hitInfo.GetComponent<playerController>();
        if (playerHit != null && isAttacking == true)
        {
            playerHit.TakeDamage(bossDamage);
        }
    }

    IEnumerator AttackWait()
    {
        if (animator.GetBool("isEnraged") == true)
        {
            yield return new WaitForSeconds(3f);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
        currentSpeed = baseSpeed;
        atkNum = 0;
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
        Instantiate(meatDrop, transform.position + dropPos, transform.rotation);
        Instantiate(meatDrop, transform.position + dropPos, transform.rotation);
        Instantiate(meatDrop, transform.position + dropPos, transform.rotation);
        Instantiate(meatDrop, transform.position + dropPos, transform.rotation);
        if (corpsePrefab != null)
        {
            GameObject myCorpse = Instantiate(corpsePrefab, transform.position, transform.rotation);
            if (facingRightNum == -1)
            {
                Vector3 Scaler = myCorpse.transform.localScale;
                Scaler.x *= -1;
                myCorpse.transform.localScale = Scaler;
            }
        }
        Destroy(gameObject);
    }

    public void SetGravity(float gravityNum)
    {
        rb.gravityScale = gravityNum;
    }
    
    public void Jump()
    {
        animator.SetTrigger("Jump");
        rb.velocity = (Vector2.up + (Vector2.right * facingRightNum)) * jumpStrength;
    }
    public void AnimJump(float strength)
    {
        rb.velocity = (Vector2.up + (Vector2.right * facingRightNum)) * strength;
    }
    public void AnimDrop(float strength)
    {
        //rb.velocity = ((Vector2.up * -1) + (Vector2.right * facingRightNum)) * strength;
        //rb.velocity = ((Vector2.up * -1) + target) * strength;
        rb.velocity = (Vector2.up * -1) * strength;
    }
    public void AnimDash(float strength)
    {
        rb.velocity = (Vector2.right * facingRightNum) * strength;
    }
    public void AnimPlayerKnockback (float strength)
    {
        player.KnockedBackPlayer(facingRightNum, strength);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 10 && alive)
        {
            animator.SetBool("isEnraged", true);
            bossDamage += 1;
        }

        if (health <= 0 && alive)
        {
            //tell player to get meatstock
            //FindObjectOfType<playerController>().GainMeatStock(); -depreciated, now drops meat which when touched does the same thing.
            animator.SetTrigger("Death");
            SetGravity(1f);
            alive = false;
            //waits to die, no longer alive, so only death aniamtion plays until destroyed
            StartCoroutine(DeathWait());
        }
        //allows the player to 'stun' the enemy, returns to normal behaviour after DamagedWait()
        currentSpeed = 0f;
        animator.SetTrigger("Damaged");
        isAttacking = false;
        rb.velocity = (Vector2.up + (Vector2.right * facingRightNum * -2 ));
        StartCoroutine(DamagedWait());
    }

}

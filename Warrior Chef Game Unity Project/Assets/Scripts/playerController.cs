using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public int baseHealth = 3;
    static public int upgradeHealth = 0;
    public int maxHealth;
    public int currentHealth;

    public float speed = 1;

    public float jumpStrength = 1;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue = 1;

    private Rigidbody2D rb;

    private float moveInput;

    public meatStockUI meatStockCounter;

    public Animator animator;

    public healthScript hearts;

    public GameObject deathMenuUI;

    public int meatStock = 0;
    public int upgradeToken = 0;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        UpdateHealth();
        currentHealth = maxHealth;
        hearts.SetMaxHealth(maxHealth);
        meatStockCounter.SetMeatStock(meatStock);
        deathMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
       {
           Attack();
       }
       if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
        if(Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
        {
            Jump();
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.F) && maxHealth > currentHealth && meatStock > 0)
        {
            ConsumeMeatStock();
        }
       
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        
        
        
        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        
    }
    void Flip()
    {
        //to flip sprite to face movement
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void Attack()
   {
       animator.SetTrigger("Attack");
   }

   public void Jump()
   {
       rb.velocity = Vector2.up * jumpStrength;
       animator.SetTrigger("Jump");
   }


    public void GainMeatStock()
    {
        meatStock ++;

        meatStockCounter.SetMeatStock(meatStock);
    }

    public void LoseMeatStock()
    {
        meatStock --;

        meatStockCounter.SetMeatStock(meatStock);
    }

    public void ConsumeMeatStock()
    {
        LoseMeatStock();

        currentHealth ++;
        
        hearts.SetHealth(currentHealth);
    }

    public void GainToken()
    {
        upgradeToken ++;
    }

    public void UpdateHealth()
    {
        maxHealth = baseHealth + upgradeHealth;
    }

    public void UpgradeHealthBy(int upgrade)
    {
        upgradeHealth = upgradeHealth + upgrade;
        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        hearts.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            //pauses game and activates failstate/death ui
            deathMenuUI.SetActive(true);
            Time.timeScale = 0f;
            //Destroy(gameObject);
        }
    }

}

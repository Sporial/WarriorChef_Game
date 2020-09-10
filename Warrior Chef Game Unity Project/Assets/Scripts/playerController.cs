using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    static playerController instance;
    //multiple health variables for base health and maintaining additional upgrade health
    public int baseHealth = 3;
    static public int upgradeHealth = 0;
    public int maxHealth;
    public int currentHealth;
    

    public float speed = 1;

    public float jumpStrength = 1;

    //all for checking if the player is on the ground eg. double jump status or airial attack status
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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance!= this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       hearts = GameObject.Find("Hearts").GetComponent<healthScript>();
       meatStockCounter = GameObject.Find("MeatStock Counter").GetComponent<meatStockUI>();

        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        UpdateHealth();
        currentHealth = maxHealth;
        hearts.SetMaxHealth(maxHealth);
        //meatStockCounter.SetMeatStock(meatStock);
        deathMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        meatStockCounter.SetMeatStock(meatStock);

        if (Input.GetMouseButtonDown(0))
       {
           Attack();
       }

       //detects whether the player is touching the ground, if they are, they have their double jump refreshed
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

        //allows the player to consume meat to regain health
        if(Input.GetKeyDown(KeyCode.F) && maxHealth > currentHealth && meatStock > 0)
        {
            ConsumeMeatStock();
        }
       
    }

    void FixedUpdate()
    {
        //checking if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //controls player movements and movement animations
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        
        
        //flips player based on direction of movement
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

    //all updates to UI and/or storing new values
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

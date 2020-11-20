using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    static playerController instance;
    //multiple health variables for base health and maintaining additional upgrade health
    public int baseHealth = 3;
    static public int upgradeHealth = 0;
    public int maxHealth =3;
    public int currentHealth;
    public int curLevelUnlocked=0;

    public float speed = 1;

    public float jumpStrength = 1;

    //all for checking if the player is on the ground eg. double jump status or airial attack status
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue = 1;

    private Rigidbody2D rb;

    public float moveInput;

    public meatStockUI meatStockCounter;

    public Animator animator;
    bool isCrouching = false;

    public healthScript hearts;

    public GameObject deathMenuUI;

    public int meatStock = 0;
    public int upgradeToken = 0;

    public float timer = 0.0f;
    public float heavyHoldTiming = 1.0f;

    private bool facingRight = true;

    public AudioSource itemPickup;
    public AudioSource deathAudio;
    public AudioSource animalAttack;

    //trail FX for sword
    //public ParticleSystem pSys;

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
        AudioSource[] audios = GetComponents<AudioSource>();
        itemPickup = audios[0];
        animalAttack = audios[1];
        deathAudio = audios[2];
        
        //pSys.Stop();
       hearts = GameObject.Find("HealthBar").GetComponent<healthScript>();
       meatStockCounter = GameObject.Find("MeatStock Counter").GetComponent<meatStockUI>();

        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        //UpdateHealth();
        currentHealth = maxHealth;
        //hearts.SetMaxHealth(maxHealth);
        //meatStockCounter.SetMeatStock(meatStock);
        deathMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        meatStockCounter.SetMeatStock(meatStock);
        hearts.SetHealth(currentHealth);

       /* if (Input.GetMouseButton(0))
       {
            timer += Time.deltaTime;
       }
       if (Input.GetMouseButtonUp(0) && timer <= heavyHoldTiming)
        {
            Attack();
            timer = 0.0f;
        }
        else if (Input.GetMouseButtonUp(0) && timer > heavyHoldTiming)
        {
            HeavyAttack();
            timer = 0.0f;
        }
       
       if (Input.GetMouseButtonDown(1))
       {
           LiftAttack();
           rb.velocity = Vector2.up * jumpStrength;
       }
        */
        if (Input.GetMouseButtonDown(0) && isGrounded == true)
        {
            Attack();
        }
        if (Input.GetMouseButtonDown(0) && isGrounded == false)
        {
            LiftAttack();
        }
        if (Input.GetMouseButtonDown(1))
        {
            HeavyAttack();
        }
       //detects whether the player is touching the ground, if they are, they have their double jump refreshed
       if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
        if((Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Space))) && extraJumps > 0)
        {
            Jump();
            extraJumps--;
        }
        else if((Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Space))) && extraJumps == 0 && isGrounded == true)
        {
            Jump();
        }

        //allows the player to consume meat to regain health
        if(Input.GetKeyDown(KeyCode.F) && maxHealth > currentHealth && meatStock > 0)
        {
            ConsumeMeatStock();
        }

        if(Input.GetButton("SButton") && isGrounded == true)
        {
            isCrouching = true;
            Crouch();
        }
        else
        {
            isCrouching = false;
            Crouch();
        }
        /*
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5)
         {
            Transform spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
            if (spawnLocation != null && transform.position.y < -200f)
            {
                transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, 0);
            }
         }
       */
    }

    void FixedUpdate()
    {
        //checking if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //controls player movements and movement animations
        moveInput = Input.GetAxisRaw("Horizontal");
        if(isCrouching == false)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        
        if (Camera.main != null)
        {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint( new Vector2(Input.mousePosition.x,  Input.mousePosition.y));
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);

        if ((facingRight == true && cursorPos.x < myPos.x) || (facingRight == false && cursorPos.x > myPos.x))
        {
            Flip();
        }
        }
        //flips player based on direction of movement
        /*if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }*/
        
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
       // pSys.Play();
        //StartCoroutine(StopParticle());
    }

    //public IEnumerator StopParticle()
   // {
    //    yield return new WaitForSeconds(1);
    //    pSys.Stop();
    //}
    public void HeavyAttack()
    {
       animator.SetTrigger("HeavyAttack");
    }
    public void LiftAttack()
    {
        animator.SetTrigger("LiftAttack");
    }

   public void Jump()
   {
       rb.velocity = Vector2.up * jumpStrength;
       animator.SetTrigger("Jump");
   }

   public void Crouch()
   {
       if (isCrouching == true)
        {
            animator.SetBool("isCrouching", true);
        }
       else
       {
           animator.SetBool("isCrouching", false);
       }
   }

   public void KnockedBackPlayer(int knockBackDir, float strength)
   {
       rb.velocity = (Vector2.right * knockBackDir) * strength;
   }

   public void LiftGravity()
   {
       rb.gravityScale = 0;
   }

   public void ResetGravity()
   {
       rb.gravityScale = 1;
   }

    //all updates to UI and/or storing new values
    public void GainMeatStock()
    {
        itemPickup.Play();
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
        ResetHP();
        hearts.SetHealth(currentHealth);
    }

    public void GainToken()
    {
        upgradeToken ++;
    }

    public void UpdateHealth()
    {
        maxHealth = baseHealth + upgradeHealth;
        hearts.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
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
            deathAudio.Play();
            //pauses game and activates failstate/death ui
            deathMenuUI.SetActive(true);
            Time.timeScale = 0f;
            //Destroy(gameObject); 
        }
    }
    public void ResetHP()
    {
        hearts.ResetHealth();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public int health = 100;

    public float speed = 1;

    public float jumpStrength = 1;

    private Rigidbody2D rb;

    private float moveInput;

    public int meatStock = 0;

    //extra jumps? best options for upgrades?

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpStrength;
        }
    }

    public void GainMeatStock()
    {
        meatStock ++;
    }

    //public void TakeDamage(int damage)
    //{
    //  health -= damage;
    //  if (health <= 0)
    //    {
    //      deahmenu or something
    //    }
    //}
}

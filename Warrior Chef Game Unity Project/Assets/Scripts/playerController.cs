﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public int health = 100;

    public float speed = 1;

    public float jumpStrength = 1;

    private Rigidbody2D rb;

    private float moveInput;

    //public MeatStockCounter meatStockCounter;

    public Animator animator;

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
        if (Input.GetMouseButtonDown(0))
       {
           Attack();
       }
       
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
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

        //meatStockCounter.SetMeatStock(meatStock);
    }

    //void consume meatstock() -meatstock +health

    //public void TakeDamage(int damage)
    //{
    //  health -= damage;
    //  if (health <= 0)
    //    {
    //      deahmenu or something
    //    }
    //}
}

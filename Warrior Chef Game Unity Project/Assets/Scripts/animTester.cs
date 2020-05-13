using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTester : MonoBehaviour
{
   public Animator animator;

   void Update()
   {
       if (Input.GetMouseButtonDown(0))
       {
           Attack();
       }
       if (Input.GetKeyDown(KeyCode.Space))
       {
           Run();
       }
   }

   public void Attack()
   {
       animator.SetTrigger("Attack");
   }

   public void Run()
   {
       animator.SetTrigger("Run");
   }

}

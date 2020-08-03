using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingTargetScript : MonoBehaviour
{
    //test code for success/failure state
      public void CookUpgrade()
    {
        Debug.Log("success");
    }

    public void CookFail()
    {
        Debug.Log("epic fail");
    }
}

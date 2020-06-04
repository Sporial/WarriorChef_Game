using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingDetectorScript : MonoBehaviour
{
    public cookingTargetScript cookingTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //if cookingTarget touches cookingTargetDetector, give upgrade token, subtract meat, else, just subtract meat.
            
        
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
     {
        cookingTargetScript food = hitInfo.GetComponent<cookingTargetScript>();
        if(Input.GetKeyDown(KeyCode.C))
        {
            cookingTarget.CookUpgrade();
        }
     }
}

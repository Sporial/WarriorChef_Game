using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cookingMinigameController : MonoBehaviour
{

    public GameObject cookingMinigame;

    public GameObject upgradeMenu;

    public GameObject postGameMenu;

    public playerController player;

    public Slider slider;

    public GameObject cookingIdle;
    public GameObject cookingAction;
    public GameObject cookingFail;
    public GameObject cookingSuccess;

    //public Transform cookingTargetDetector;
    public cookingTargetScript cookingTarget;

    public float xFrequency = 1;
    public float xMagnitude = 1;
    
    public static bool isCooking = false;


    


    // Start is called before the first frame update
    void Start()
    {
        cookingMinigame.SetActive(false);
        upgradeMenu.SetActive(false);
        postGameMenu.SetActive(false);
        isCooking = false;
        cookingIdle.SetActive(true);
        cookingAction.SetActive(false);
        cookingFail.SetActive(false);
        cookingSuccess.SetActive(false);
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (isCooking)
            {
                UpgradeChef();
            }
            else
            {
                YesChef();
                cookingIdle.SetActive(true);
                cookingAction.SetActive(false);
                cookingFail.SetActive(false);
                cookingSuccess.SetActive(false);
                StartCoroutine(Wait());
            }
            
        }
    
        if (Input.GetKeyDown(KeyCode.C) && player.meatStock > 0 && isCooking == true)
        {
            CookMeal();
            player.LoseMeatStock();
            cookingIdle.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        slider.value = Mathf.Sin(Time.time * xFrequency) * xMagnitude;
    }

    public void CookMeal()
    {
        //this is scuffed as, but it does the job for now -- collisions on the sliders seemed borked :(
        if(slider.value > -0.3 && slider.value < 0.3)
        {
            cookingFail.SetActive(false);
            cookingAction.SetActive(false);
            cookingSuccess.SetActive(true);
            cookingTarget.CookUpgrade();
            StartCoroutine(Wait());
            player.GainToken();
        }
        else
        {
            cookingSuccess.SetActive(false);
            cookingAction.SetActive(false);
            cookingFail.SetActive(true);
            cookingTarget.CookFail();
            StartCoroutine(Wait());
        }
    }
    
    //run to wait before swapping character
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        CookingDefault();
    }

    void CookingDefault()
    {
        cookingIdle.SetActive(false);
        cookingAction.SetActive(true);
        cookingFail.SetActive(false);
        cookingSuccess.SetActive(false);
    }

    public void YesChef()
    {
        
        cookingMinigame.SetActive(true);
        upgradeMenu.SetActive(false);
        postGameMenu.SetActive(false);
        isCooking = true;
    }

    public void UpgradeChef()
    {
        upgradeMenu.SetActive(true);
        cookingMinigame.SetActive(false);
        postGameMenu.SetActive(false);
        isCooking = false;
    }

    public void ContinueMenu()
    {
        postGameMenu.SetActive(true);
        upgradeMenu.SetActive(false);
        cookingMinigame.SetActive(false);
        isCooking = false;
    }
}

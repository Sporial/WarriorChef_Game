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

    public Slider slider;

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
            }
            
        }
    }

    void FixedUpdate()
    {
        slider.value = Mathf.Sin(Time.time * xFrequency) * xMagnitude;
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

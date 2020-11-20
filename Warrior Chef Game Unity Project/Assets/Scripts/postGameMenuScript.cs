using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class postGameMenuScript : MonoBehaviour
{
    public playerController player;
    public SaveController saveCont;

    public void ReplayLevel()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        saveCont = GameObject.FindGameObjectWithTag("Manager").GetComponent<SaveController>();
        player.deathMenuUI.SetActive(false);
        player.currentHealth = player.maxHealth;
        player.meatStock = saveCont.startMeat;
        player.ResetHP();
        Time.timeScale = 1f;
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(SceneManager.GetActiveScene().name);
    }
}

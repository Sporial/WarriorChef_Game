using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class postGameMenuScript : MonoBehaviour
{
    public playerController player;
    void Start()
    {
        
    }

    public void ReplayLevel()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        player.deathMenuUI.SetActive(false);
        player.currentHealth = player.maxHealth;
        Time.timeScale = 1f;
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }
}

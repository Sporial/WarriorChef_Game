using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    GameObject player;
    public void NewGame()
    {
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(1);
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody2D>().angularVelocity = 0;

        if (SceneManager.GetActiveScene().buildIndex == 1)
            //Level 1
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Transform spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
            playerTransform.transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, 0);
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        //Level2
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Transform spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
            playerTransform.transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, 0);
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        //Level3
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Transform spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
            playerTransform.transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, 0);
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        //Town
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Transform spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
            playerTransform.transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, 0);
            SavePlayer();
        }
    }
    public void SavePlayer()
    {
        GameObject pCon = GameObject.FindGameObjectWithTag("Player");
        SaveSystem.SavePlayer(pCon.GetComponent<playerController>());
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        playerController pCon = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        GameObject gO = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(data.meatStock);
        pCon.meatStock = data.meatStock;
        pCon.upgradeToken = data.upgradeToken;
        pCon.maxHealth = data.maxHealth;
        pCon.curLevelUnlocked = data.mapLevel;
    }
}

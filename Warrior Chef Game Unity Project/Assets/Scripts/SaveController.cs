using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    public GameObject player;
    public int startMeat;
    public GameObject saveText;
    public Camera mainCam;
    public AudioSource clickSound;
    public void NewGame()
    {
        clickSound = GetComponent<AudioSource>();
        clickSound.Play();
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<playerController>().ResetHP();
        player.GetComponent<playerController>().meatStock = 0;
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel("Level1");
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
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        mainCam.enabled = true;
        //player.GetComponent<playerController>().pSys.Stop();

        //I'm like 99.99% sure I can combine all of these into 1 if statement, but I dont want to risk it in case stuff messes up

        if (SceneManager.GetActiveScene().name.Equals("Level1"))
            //Level 1
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Transform spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
            playerTransform.transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, 0);
            startMeat = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().meatStock;
        }

        if (SceneManager.GetActiveScene().name.Equals("Level2"))
        //Level2
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Transform spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
            playerTransform.transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, 0);
            startMeat = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().meatStock;
        }
        if (SceneManager.GetActiveScene().name.Equals("Level3"))
        //Level3
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Transform spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
            playerTransform.transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, 0);
            startMeat = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().meatStock;
        }

        if (SceneManager.GetActiveScene().name.Equals("Town"))
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
        StartCoroutine(ShowSaveText());
    }
    public IEnumerator ShowSaveText()
    {
        saveText.SetActive(true);
        yield return new WaitForSeconds(2);
        saveText.SetActive(false);
    }

    public void LoadPlayer()
    {
        clickSound = GetComponent<AudioSource>();
        clickSound.Play();
        PlayerData data = SaveSystem.LoadPlayer();
        playerController pCon = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        GameObject gO = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(data.meatStock);
        pCon.meatStock = data.meatStock;
        pCon.upgradeToken = data.upgradeToken;
        pCon.maxHealth = data.maxHealth;
        pCon.curLevelUnlocked = data.mapLevel;
        pCon.ResetHP();

        healthScript hScript = GameObject.Find("HealthBar").GetComponent<healthScript>();
        hScript.SetMaxHealth(data.maxHealth);
    }
}

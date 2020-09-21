using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuUIScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public playerController player;

    // Start is called before the first frame update
    void Start()
    {
        //makes sure the ui isn't active and the game is not paused on game start (important if reloading)
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    { //press escape to do the opposite, if paused, unpause etc.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }
    }
 //removes ui and unpauses the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
//activates ui and pauses the game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    //returns to menu
    public void LoadMenu()
    {
        //needs to be fixed to account for more levels - worsk for prototype
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        player.deathMenuUI.SetActive(false);
        player.currentHealth = player.maxHealth;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit.");
        Application.Quit();
    }
}

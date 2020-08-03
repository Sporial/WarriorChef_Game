using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject confirmationMenu;

    void Start()
    {
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        confirmationMenu.SetActive(false);
    }

    public void NewGame()
    {
        //just loads the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //may want a 'ContinueGame()' option, otherwise LoadGame() may suffice.

    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
    }

    public void Return()
    {
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        confirmationMenu.SetActive(false);
    }

    public void ConfirmQuit()
    {
        //Quits the application, also prints 'Quit' so you can tell while editing
        Debug.Log("Quit.");
        Application.Quit();
    }

    //public void LoadGame()
    //needs to load all player data, as well as what scene they need etc. specific buildIndex??

    public void QuitGame()
    {
        confirmationMenu.SetActive(true);
    }
}

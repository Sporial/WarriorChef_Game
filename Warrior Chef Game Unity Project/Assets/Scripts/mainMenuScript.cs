using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    public void NewGame()
    {
        //just loads the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //public void LoadGame()

    public void QuitGame()
    {
        //Quits the application, also prints 'Quit' so you can tell while editing
        Debug.Log("Quit.");
        Application.Quit();
    }
}

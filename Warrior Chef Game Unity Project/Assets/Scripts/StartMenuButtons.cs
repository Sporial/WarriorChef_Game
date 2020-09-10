using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuButtons : MonoBehaviour
{
    SaveController saveControl;
    Button newGame;
    Button loadGame;
    void Start()
    {
        //This script has to be done because of returning to menu. Otherwise buttons would be unassigned.

        newGame = GameObject.Find("New Game Button").GetComponent<Button>();
        loadGame = GameObject.Find("Load Game Button").GetComponent<Button>();
        newGame.onClick.AddListener(NewGameClicked);
        loadGame.onClick.AddListener(LoadGameClicked);
    }

    void NewGameClicked()
    {
        saveControl = GameObject.Find("SceneManager").GetComponent<SaveController>();
        saveControl.NewGame();
    }
    void LoadGameClicked()
    {
        saveControl = GameObject.Find("SceneManager").GetComponent<SaveController>();
        saveControl.LoadPlayer();
    }
}

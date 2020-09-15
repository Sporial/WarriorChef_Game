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
    public GameObject noLoadAvailable;
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
        playerController pCon = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        Debug.Log(pCon.curLevelUnlocked);
        if (pCon.curLevelUnlocked < 1)
        {
            noLoadAvailable.SetActive(true);
            StartCoroutine(Wait());
        }
        else
        {
            GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(3);
        }

        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        noLoadAvailable.SetActive(false);
    }
}

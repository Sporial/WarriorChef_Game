using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapControl : MonoBehaviour
{

    public void Start()
    {
        playerController pCon = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        CheckLevelUnlocked(pCon.curLevelUnlocked);
    }
    public void CheckLevelUnlocked(int curLevel)
    {
        var icon1 = GameObject.Find("1");
        var icon2 = GameObject.Find("2");
        var icon3 = GameObject.Find("3");
        var connect1 = GameObject.Find("Connect1");
        var connect2 = GameObject.Find("Connect2");

        icon1.GetComponent<Button>().enabled = true;
        icon2.GetComponent<Button>().enabled = true;
        icon3.GetComponent<Button>().enabled = true;

        if (curLevel <=1)
        {
            icon3.GetComponent<Image>().color = new Color32(85, 85, 85, 255);
            connect2.GetComponent<Image>().color = new Color32(85, 85, 85, 255);
            icon3.GetComponent<Button>().enabled = false;
        }
        if (curLevel == 2)
        {
            icon3.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            connect2.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            icon3.GetComponent<Button>().enabled = true;
        }
    }

    public void TownButton()
    {
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(3);
        playerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        if(player.curLevelUnlocked!=1 && player.curLevelUnlocked < 2) { player.curLevelUnlocked = 1; }
        Debug.Log(player.curLevelUnlocked);
    }

    public void Level1Button()
    {
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(1);
    }
    public void Level2Button()
    {
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(4);
        playerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        player.curLevelUnlocked = 2;
    }

    public void Level3Button()
    {
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(5);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapControl : MonoBehaviour
{

    public void Start()
    {
        CheckLevelUnlocked(1);
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

        if (curLevel ==1)
        {
            icon3.GetComponent<Image>().color = new Color32(85, 85, 85, 255);
            connect2.GetComponent<Image>().color = new Color32(85, 85, 85, 255);
            icon3.GetComponent<Button>().enabled = false;
        }
    }

    public void TownButton()
    {
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(3);
    }

    public void Level1Button()
    {
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(1);
    }

}

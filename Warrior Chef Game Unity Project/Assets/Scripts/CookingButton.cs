using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CookingButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel("Map");

        //GameObject.Find("PlayerUI");
        //GameObject.Find("PlayerUI").GetComponent<cookingMinigameController>().YesChef();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class CookingButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject.Find("LevelController").GetComponent<FadeControls>().StartFade();

        GameObject.Find("PlayerUI");
        GameObject.Find("PlayerUI").GetComponent<cookingMinigameController>().YesChef();
    }
}

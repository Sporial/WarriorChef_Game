using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOWNToCookButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject.Find("PlayerUI");
        GameObject.Find("PlayerUI").GetComponent<cookingMinigameController>().YesChef();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TOWNToMapButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject.Find("FadeToBlack").GetComponent<FadeControls>().FadeToLevel(2);
    }
}

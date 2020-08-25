using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeControls : MonoBehaviour
{
    public Image black;
    public Animator anim;
    //define level to load on LevelController in scene
    public 
        int index;

    //Call with StartCoroutine(Fade())

    public void StartFade()
    {
        StartCoroutine(Fade());
    }
    public IEnumerator Fade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(index);
    }
}

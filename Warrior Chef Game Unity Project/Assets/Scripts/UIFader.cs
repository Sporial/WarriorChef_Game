using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFader : MonoBehaviour
{

    //Used in town screen to autosave

    private bool mFaded = true;
    public float Duration =1f;

    public void Start()
    {
        mFaded = true;
        var saveIcon = GameObject.Find("SaveIcon").GetComponent<CanvasGroup>();


        StartCoroutine(DoFadeIcon(saveIcon, saveIcon.alpha, mFaded ? 1 : 0));
        
    }

    public IEnumerator DoFadeIcon(CanvasGroup canvGroup, float start, float end)
    {
        var saveText = GameObject.Find("GameSaved").GetComponent<CanvasGroup>();
        float counter = 0f;
        
        if (mFaded)
        {
            while (counter < Duration)
            {
                counter += Time.deltaTime;
                canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
                yield return null;
            }
            
            StartCoroutine(DoFadeText(saveText, saveText.alpha, mFaded ? 1 : 0));
        }
        else
        {
            while (counter < Duration)
            {
                counter += Time.deltaTime;
                canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
                yield return null;
            }
            StartCoroutine(DoFadeText(saveText, saveText.alpha, mFaded ? 1 : 0));
        }

    }

    public IEnumerator DoFadeText(CanvasGroup canvGroup, float start, float end)
    {
        var saveIcon = GameObject.Find("SaveIcon").GetComponent<CanvasGroup>();
        yield return new WaitForSeconds(1);
        float counter = 0f;
        {
            while (counter < Duration)
            {
                counter += Time.deltaTime;
                canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
                yield return null;
            }
            counter = 0f;
            yield return new WaitForSeconds(1);
            while (counter < Duration)
            {
                counter += Time.deltaTime;
                canvGroup.alpha = Mathf.Lerp(end, start, counter / Duration);
                saveIcon.alpha = Mathf.Lerp(end, start, counter / Duration);
                yield return null;
            }
            mFaded = !mFaded;
           // StartCoroutine(DoFadeIcon(saveIcon, saveIcon.alpha, mFaded ? 1 : 0));
        }
        
    }

}

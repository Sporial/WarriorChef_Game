using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthScript : MonoBehaviour
{
    //current stand in for hearts system. hearts worked, but aren't infinitely scaleable for the upgrade system, considering a 'texture' that can tile infinitely to get around this problem (still a slider, but creates a 'heart' at every interval)
    public GameObject healthBar;
    public GameObject heart;

    //playerController does the heavy lifting, this just updates to UI to whatever it's told
    public void SetMaxHealth(int health)
    {
        healthBar = GameObject.Find("HealthBar");
        heart = GameObject.Find("Heart");
        while(healthBar.transform.childCount < health)
        {
            Instantiate(heart,healthBar.transform);
            Debug.Log("Success");
        }
        while(healthBar.transform.childCount > health)
        {
            Destroy(healthBar.transform.GetChild(healthBar.transform.childCount - 1).gameObject);
        }
    }
    public void ResetHealth()
    {
        healthBar = GameObject.Find("HealthBar");
        int maxHealth = healthBar.transform.childCount;
        Transform[] children = healthBar.GetComponentsInChildren<Transform>();
        List<GameObject> childrenObjects = new List<GameObject>();
        foreach (Transform child in children)
        {
            childrenObjects.Add(child.gameObject);
        }
        for (int i = 1; i < maxHealth+1; i++)
        {
            childrenObjects[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        }
    }
    public void SetHealth(int health)
    {       
        healthBar = GameObject.Find("HealthBar");
        int maxHealth = healthBar.transform.childCount;
        Transform[] children = healthBar.GetComponentsInChildren<Transform>();
        List<GameObject> childrenObjects = new List<GameObject>();
        foreach(Transform child in children)
        {
            childrenObjects.Add(child.gameObject);
        }
        for(int i = health+1; i < maxHealth+1; i++)
        {
            childrenObjects[i].GetComponent<Image>().color = new Color32(255,255,255,50);

        }
    }
}

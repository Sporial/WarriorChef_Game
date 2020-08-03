using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthScript : MonoBehaviour
{
    //current stand in for hearts system. hearts worked, but aren't infinitely scaleable for the upgrade system, considering a 'texture' that can tile infinitely to get around this problem (still a slider, but creates a 'heart' at every interval)
    public Slider slider;

    //playerController does the heavy lifting, this just updates to UI to whatever it's told
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}

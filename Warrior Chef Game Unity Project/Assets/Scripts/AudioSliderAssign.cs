using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderAssign : MonoBehaviour
{
    public Slider mainSlider;
    void Start()
    {
        mainSlider = this.gameObject.GetComponent<Slider>();
        mainSlider.onValueChanged.AddListener(delegate { GameObject.Find("PlayerUI").GetComponent<VolumeValue>().SetVolume(mainSlider.value); });
        
    }

}

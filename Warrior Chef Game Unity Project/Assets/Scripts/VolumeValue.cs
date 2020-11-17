using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    public AudioSource mainMusic;
    private float musicVolume = 1f;
    void Start()
    {
        mainMusic = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        mainMusic.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}

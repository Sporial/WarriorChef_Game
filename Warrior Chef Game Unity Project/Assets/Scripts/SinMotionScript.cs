using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMotionScript : MonoBehaviour
{
    //test code for moving an objects position back and forth around a curve, experiment for cooking slider to be a 'curve'

    public float xMovespeed = 1;
    public float yMovespeed = 1;
    public float xFrequency = 1;
    public float yFrequency = 1;
    public float xMagnitude = 1;
    public float yMagnitude = 1;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.up * Mathf.Abs(Mathf.Cos(Time.time * yFrequency)) * yMagnitude +
                             transform.right * Mathf.Sin(Time.time * xFrequency) * xMagnitude;
    }
}

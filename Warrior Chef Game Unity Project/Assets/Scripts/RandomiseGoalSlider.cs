using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomiseGoalSlider : MonoBehaviour
{
    public GameObject playArea;
    public GameObject randomLocal;

    // Update is called once per frame
    public void RandomiseLocation()
    {
        var areaSize = playArea.GetComponent<BoxCollider2D>();
        var randomPos = Random.Range(areaSize.bounds.min.x + 8, areaSize.bounds.max.x - 8);

        randomLocal.transform.position = new Vector3(randomPos, areaSize.bounds.center.y);
    }
}

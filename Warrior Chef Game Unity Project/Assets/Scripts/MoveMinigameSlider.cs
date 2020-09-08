using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMinigameSlider : MonoBehaviour
{

    public GameObject playArea;
    public GameObject meatSlider;
    public GameObject randomLocal;
    float randomSpeed = 200;


    void Update()  
    {
        var areaSize = playArea.GetComponent<BoxCollider2D>();

            meatSlider.transform.position = new Vector3(Mathf.PingPong(Time.time * randomSpeed, areaSize.bounds.max.x - areaSize.bounds.min.x) + areaSize.bounds.min.x, areaSize.bounds.center.y, transform.position.z);

    }
    public bool CheckIfIntersects()
    {
        ResetRandomLocal();

        if (meatSlider.GetComponent<Collider2D>().bounds.Intersects(randomLocal.GetComponent<Collider2D>().bounds))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetRandomLocal()
    {
        randomSpeed = Random.Range(200, 500);
        GameObject.Find("CookingPlayArea").GetComponent<RandomiseGoalSlider>().RandomiseLocation();
    }
}

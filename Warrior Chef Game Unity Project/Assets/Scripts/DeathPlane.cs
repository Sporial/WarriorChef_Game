using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //temporary for falling off of map
        if(collision.tag == "Player")
        {
            StartCoroutine(Wait());
        }
    }
    public IEnumerator Wait()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Transform spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
        
        yield return new WaitForSeconds(0.5f);
        playerTransform.transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, 0);
    }
}

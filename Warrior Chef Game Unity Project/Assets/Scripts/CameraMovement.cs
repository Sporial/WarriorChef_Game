using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    public float threshold;

    float Damping = 12.0f;


    void Update()
    {
        if (GameObject.Find("Moving_stance_2").GetComponent<playerController>().isGrounded && Input.GetButton("SButton"))
        {

            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = (player.position + mousePos) / 2f;

            targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
            targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

            // this.transform.position = targetPos;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * Damping);
        }
        else
        {
            this.transform.localPosition = new Vector3(0, 6, 0);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponShaderController : MonoBehaviour
{
    Material material;

    public playerController player;

    bool isChanging = false;
    float opacity = 1.2f;
    float outlineOpacity = 0f;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isAttacking == true)
        {
            opacity -= Time.deltaTime;
            outlineOpacity += Time.deltaTime;

            if (opacity <= 1f)
            {
                opacity = 1f;
            }

            if (outlineOpacity >= 3f)
            {
                outlineOpacity = 3f;
            }

            material.SetFloat("_Opacity", opacity);
            material.SetFloat("_OutlineOpacity", outlineOpacity);
        }
        else if (player.isAttacking == false)
        {
            opacity += Time.deltaTime;
            outlineOpacity -= Time.deltaTime;

            if (opacity >= 1.2f)
            {
                opacity = 1.2f;
            }

            if (outlineOpacity <= 0f)
            {
                outlineOpacity = 0f;
            }

            material.SetFloat("_Opacity", opacity);
            material.SetFloat("_OutlineOpacity", outlineOpacity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTown : MonoBehaviour
{
    // Start is called before the first frame update
    public void clicked()
    {
        SceneManager.LoadScene(3);
    }
}

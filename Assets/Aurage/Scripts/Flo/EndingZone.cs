using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingZone : MonoBehaviour
{
    public bool levelEnded = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            levelEnded = true;
    }

    private void Update()
    {
        if (levelEnded)
            SceneManager.LoadScene("WinningScreen");
        else
            return;
    }
}

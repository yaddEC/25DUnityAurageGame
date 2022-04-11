using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        KillZone.isDead = false;

        SceneManager.LoadScene("LD_Prototype");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

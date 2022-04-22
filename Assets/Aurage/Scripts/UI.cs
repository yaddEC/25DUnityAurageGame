using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void StartGame()
    {
        SceneSwitcher.GoToSelectedScene("LevelSelector");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

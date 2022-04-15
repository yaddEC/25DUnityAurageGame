using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static void GameOverScreen()
    {
        SceneManager.LoadScene("GameOverScene");   
    }

    public static void GameWinScreen()
    {
        SceneManager.LoadScene("GameWinScene");
    }

    public static void GameMenuScreen()
    {
        SceneManager.LoadScene("MenuScene");   
    }
}
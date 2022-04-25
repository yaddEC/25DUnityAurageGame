using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static void GameWinScreen()
    {
        SceneManager.LoadScene("GameWinScreen");
    }

    public static void GameMenuScreen()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public static void GameLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
        GameOver.StartGamePlay();
    }

    public static void GoToSelectedScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    public static void CloseApplication()
    {
        Application.Quit();
    }

    public static void RestartScene()
    {
        Debug.Log("Lol here");
        GameOver.StartGamePlay();
    }
}
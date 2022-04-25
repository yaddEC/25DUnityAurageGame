using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static void GameOverScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
        Debug.Log("Go To Game Over Scene");
    }

    public static void GameWinScreen()
    {
        SceneManager.LoadScene("GameWinScreen");
        Debug.Log("Go To Game Win Scene");
    }

    public static void GameMenuScreen()
    {
        SceneManager.LoadScene("MenuScreen");
        Debug.Log("Go To Menu Scene");
    }

    public static void GameLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
        Debug.Log("Go To Level Selector Scene");
    }

    public static void GoToSelectedScene(string s)
    {
        SceneManager.LoadScene(s);
        Debug.Log("Go To " + s  + " Scene");
    }

    public static void CloseApplication()
    {
        Application.Quit();
        Debug.Log("Quit Application");
    }

    public static void RestartScene()
    {
        var s = PlayerPrefs.GetInt("UnlockedLevel");
        SceneManager.LoadScene(s);
        Debug.Log("Restarted to " + s + " Scene");
    }
}
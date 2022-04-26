using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private static PauseMenu refPauseMenu;
    private void Awake()
    {
        refPauseMenu = GameObject.FindObjectOfType<PauseMenu>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelSelector"))
            Time.timeScale = 1;
    }

    public static void GameWinScreen()
    {
        refPauseMenu.ClosePause();
        SceneManager.LoadScene("GameWinScreen");
    }

    public static void GameMenuScreen()
    {
        refPauseMenu.ClosePause();
        SceneManager.LoadScene("MenuScreen");
    }

    public static void GameLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MenuScreen"))
            return;
        else
            refPauseMenu.ClosePause();

    }

    public static void GoToSelectedScene(string s)
    {
        //refPauseMenu.ClosePause();
        SceneManager.LoadScene(s);
    }

    public static void CloseApplication()
    {
        Application.Quit();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MenuScreen"))
            return;
        else
            refPauseMenu.ClosePause();
    }

    public static void RestartScene()
    {
        refPauseMenu.ClosePause();
        GameOver.StartGamePlay();
    }
}
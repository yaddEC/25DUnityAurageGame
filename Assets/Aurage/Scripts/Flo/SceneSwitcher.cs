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
        refPauseMenu.ClosePause(false);
        SceneManager.LoadScene("GameWinScreen");
    }

    public static void GameMenuScreen()
    {
        refPauseMenu.ClosePause(false);
        SceneManager.LoadScene("MenuScreen");
    }

    public static void GameLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MenuScreen"))
            return;
        else
            refPauseMenu.ClosePause(false);

    }

    public static void GoToSelectedScene(string s)
    {
        PowerManager.outOfPower = false;
        SceneManager.LoadScene(s);
    }

    public static void CloseApplication()
    {
        Application.Quit();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MenuScreen"))
            return;
        else
            refPauseMenu.ClosePause(false);
    }

    public static void RestartScene()
    {
        refPauseMenu.ClosePause(false);
        GameOver.StartGamePlay();
    }
}
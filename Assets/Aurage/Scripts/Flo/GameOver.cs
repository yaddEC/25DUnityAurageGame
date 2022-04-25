using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject overUI;
    public static GameObject gameOverUI;
    public static bool refreshGameplay = true;

    public static PowerManager refPowerManager;
    public static PlayerMotion refPlayerMotion;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();

        if(overUI != null)
            gameOverUI = overUI;
    }

    public static void StopGamePlay()
    {
        refreshGameplay = false;
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public static void StartGamePlay()
    {
        PowerManager.outOfPower = false;
        refreshGameplay = true;

        if(gameOverUI.gameObject.activeSelf) 
            gameOverUI.SetActive(false);

        Time.timeScale = 1;

        refPowerManager.currentPower = refPowerManager.maxPower;
    }
}

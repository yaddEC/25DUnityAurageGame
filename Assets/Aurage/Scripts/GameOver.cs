using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameObject gameOverUI;
    public static bool refreshGameplay = true;

    public static PowerManager refPowerManager;
    public static PlayerMotion refPlayerMotion;

    private void Awake()
    {
        gameOverUI = GameObject.FindGameObjectWithTag("GameOver");
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
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
        gameOverUI.SetActive(false);
        Time.timeScale = 1;

        refPowerManager.currentPower = refPowerManager.maxPower;

        if(refPowerManager.waypoint != null) 
            refPlayerMotion.transform.position = refPowerManager.waypoint.position;

    }
}

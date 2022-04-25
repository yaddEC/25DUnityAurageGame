using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject overUI;
    public static GameObject gameOverUI;

    public static PowerManager refPowerManager;
    public static PlayerMotion refPlayerMotion;
    public static PowerMeterStation refPowerMeter;
    public PowerMeterStation powerMetter;
    public static CameraClamp refCameraClamp;

    private void Awake()
    {
        refPowerMeter = powerMetter;
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
        refCameraClamp = GameObject.FindObjectOfType<CameraClamp>();

        if (overUI != null)
            gameOverUI = overUI;
    }

    public static void StopGamePlay()
    {
        Time.timeScale = 0;
        PlayerState.isPlaying = false;

        if (gameOverUI != null) 
            gameOverUI.SetActive(true);

    }

    public static void StartGamePlay()
    {
        Time.timeScale = 1;
        PlayerState.isPlaying = true;

        refPowerManager.currentPower = refPowerManager.maxPower;
        PowerManager.outOfPower = false;

        refPowerMeter.SetPositionOnSpawn();
        PlayerState.isInNodePath = false;
        refCameraClamp.ChangeZPos(refPlayerMotion.transform.position.z);

        if(gameOverUI != null && gameOverUI.gameObject.activeSelf) 
            gameOverUI.SetActive(false);
    }
}

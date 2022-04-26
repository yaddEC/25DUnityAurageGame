using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        gameOverUI.SetActive(false);
    }

    public static void StopGamePlay()
    {
        refPowerMeter.refSpawnPoint.SpawnPlayer(refPlayerMotion.transform);

        PlayerState.isPlaying = false;

        if (gameOverUI != null) 
            gameOverUI.SetActive(true);

        Time.timeScale = 0;
    }

    public static void StartGamePlay()
    {
        PowerManager.outOfPower = false;
        refPowerMeter.refSpawnPoint.SpawnPlayer(refPlayerMotion.transform);

        Time.timeScale = 1;
        PlayerState.isPlaying = true;

        refPowerManager.currentPower = refPowerManager.maxPower;

        PlayerState.isInNodePath = false;
        refCameraClamp.ChangeZPos(refPlayerMotion.transform.position.z);

        if(gameOverUI != null && gameOverUI.gameObject.activeSelf) 
            gameOverUI.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMeterStation : Station
{
    [Header("Property Info")]
    public bool isEntry;
    public bool canEnter = true;
    public SpawnPoint refSpawnPoint;
    //-------------------------------------------------------------
    private void Awake() 
    { 
        var t = "Disjoncteur"; tagToSearch = t;
        refSpawnPoint = GameObject.FindObjectOfType<SpawnPoint>();
    }
    private void Start() 
    { 
        RegisterReferences();
        refSpawnPoint.SpawnPlayer(refPlayerMotion.transform);
    }

    //-------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canEnter)
        {
            if (isEntry)
                refPowerManager.currentPower = refPowerManager.maxPower;
            else
            {
                if (PlayerPrefs.GetInt("UnlockedLevel") < 7)
                {
                    PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel") + 1);
                    SceneSwitcher.GoToSelectedScene("LevelSelector");
                }
                else
                    SceneSwitcher.GameWinScreen();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isEntry) 
            canEnter = false;
    }
}
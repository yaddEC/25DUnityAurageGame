using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMeterStation : Station
{
    [Header("Property Info")]
    public bool isEntry;
    public bool canEnter = true;
    //-------------------------------------------------------------
    private void Awake() { var t = "Disjoncteur"; tagToSearch = t; }
    private void Start() { RegisterReferences(); refPlayerMotion.transform.position = lockPosition.transform.position; }
    //-------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canEnter)
        {
            if (isEntry) refPowerManager.currentPower = refPowerManager.maxPower;
            else
            {
                if(LevelSelector.actualLevel == 6)
                {
                    SceneSwitcher.GoToSelectedScene("WinningScreen");
                    PlayerPrefs.SetInt("UnlockedLevel", 1);
                }
                else
                {
                    SceneSwitcher.GoToSelectedScene("LevelSelector");
                    PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel")+1);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isEntry) canEnter = false;
    }
}
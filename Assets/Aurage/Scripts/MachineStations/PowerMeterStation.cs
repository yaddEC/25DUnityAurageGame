using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMeterStation : Station
{
    [Header("Property Info")]
    public bool isEntry;
    public bool canEnter = true;
    public static string sceneToLoad;
    //-------------------------------------------------------------
    private void Awake() { var t = "Disjoncteur"; tagToSearch = t; }
    private void Start() { RegisterReferences(); refPlayerMotion.transform.position = lockPosition.transform.position; }
    //-------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canEnter)
        {
            if (isEntry) refPowerManager.currentPower = refPowerManager.maxPower;
            else SceneSwitcher.GoToSelectedScene(sceneToLoad);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isEntry)
            canEnter = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampStation : Station
{
    [Header("Property Info")]
    public float chargingPowerDelta;

    //-------------------------------------------------------------
    private void Awake() { var t = gameObject.name; tagToSearch = t; }
    private void Start() { RegisterReferences(); }
    private void Update()
    {
        CooldownHandler(false);
        ClampInMachine();
        if (doEvent && isUsable) RestorePower();
    }
    //-------------------------------------------------------------
    private void RestorePower()
    {
        refPowerManager.currentPower += chargingPowerDelta;
        isUsable = false;
    }
    //-------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0) EnterMachine();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0) StayMachine(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0) ExitMachine();
    }
}
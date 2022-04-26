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
        CooldownHandler(special = false);
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
        if (other.tag == "Player" && cooldown <= 0) EnterMachine(this.GetComponent<LampStation>());
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0) StayMachine(autoExec = true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0) ExitMachine();
    }
}
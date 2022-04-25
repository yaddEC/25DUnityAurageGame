using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorStation : Station
{
    [Header("Generator Stats")]
    public bool checkointActivated = false;
    public float chargingPowerDelta;

    //-------------------------------------------------------------
    private void Awake() { var t = gameObject.name; tagToSearch = t; }
    private void Start() { RegisterReferences(); }
    private void Update()
    {
        CooldownHandler(special = false);

        if (doEvent) StartCoroutine(RestorePower(chargingPowerDelta));
    }
    //-------------------------------------------------------------
    private IEnumerator RestorePower(float powerAmount)
    {
        refPowerManager.currentPower += powerAmount * 0.0001f;
        yield return new WaitForSecondsRealtime(2f);
    }
    private void CheckpointChecker()
    {
        if (!checkointActivated)
        {
            refPowerManager.waypoint = transform;
            checkointActivated = true;
        }
    }
    //-------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0)
        {
            EnterMachine(this.GetComponent<GeneratorStation>()); 
            CheckpointChecker();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0)
        {
            StayMachine(autoExec = false);
            if (isUsable && InputManager.performB) doEvent = true;
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0)
        {
            if (doEvent) isUsable = false;

            doEvent = false;
            ExitMachine();
        }
    }*/
}

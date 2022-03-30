using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    public Generator refGenerator;

    [Header("Charging Stats")]
    public float chargingPower;
    public bool canCharge = true;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canCharge)
        {
            refGenerator.isMachinUsed = true;
            refPowerManager.isCharging = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            refGenerator.isMachinUsed = false;
            refPowerManager.isCharging = false;
            canCharge = false;
        }
    }
}
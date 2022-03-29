using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;

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
            refPowerManager.isCharging = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            refPowerManager.isCharging = false;
            canCharge = false;
        }
    }
}

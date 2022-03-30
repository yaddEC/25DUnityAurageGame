using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    public InteractManager refGenerator;

    [Header("Charging UI/UX")]
    private MeshRenderer meshRenderer;

    [Header("Charging Stats")]
    public float chargingPower;
    public bool canCharge = true;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();

        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canCharge)
        {
            refGenerator.isMachinUsed = true;
            refPowerManager.isCharging = true;
            meshRenderer.material = refGenerator.machineMaterials[1];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            refGenerator.isMachinUsed = false;
            refPowerManager.isCharging = false;
            canCharge = false;
            meshRenderer.material = refGenerator.machineMaterials[0];
        }
    }
}
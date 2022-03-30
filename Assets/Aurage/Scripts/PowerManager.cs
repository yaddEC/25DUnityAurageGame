using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    private GameObject refPlayer;
    private ChargingStation refChargingStation;
    private PowerBar refPowerBar;

    [Header("Charging Stats")]
    public float unchargePower;
    public bool isCharging = false;

    [Header("Player Stats")]
    public float maxPower;
    public float currentPower;

    private void Awake()
    {
        refPlayer = GameObject.FindGameObjectWithTag("Player");
        refChargingStation = GameObject.FindObjectOfType<ChargingStation>();
        refPowerBar = GameObject.FindObjectOfType<PowerBar>();

        currentPower = maxPower;
    }

    private void Update()
    {
        if(isCharging)
            StartCoroutine(PluggedEvent(refChargingStation.chargingPower));
        else
            StartCoroutine(UnpluggedEvent(unchargePower));
    }

    private IEnumerator PluggedEvent(float powerAmount)
    {
        if (currentPower >= maxPower)
            isCharging = false;

        currentPower += powerAmount * 0.0001f;
        yield return new WaitForSecondsRealtime(2f);
    }

    private IEnumerator UnpluggedEvent(float powerAmount)
    {
        if (currentPower <= 0)
            refPlayer.SetActive(false);

        currentPower -= powerAmount * 0.0001f;
        refPowerBar.SetLife(currentPower);
        yield return new WaitForSecondsRealtime(2f);
    }
}

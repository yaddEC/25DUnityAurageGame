using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    private GameObject refPlayer;
    private GeneratorStation refGeneratorStation;
    [HideInInspector] public PowerBar refPowerBar;

    [Header("Charging Stats")]
    public float unchargePowerDelta;
    public bool isCharging = false;

    [Header("Player Stats")]
    public bool outOfPower;
    public float maxPower;
    public float currentPower;
    public Transform waypoint;


    private void Awake()
    {
        refPlayer = GameObject.FindGameObjectWithTag("Player");
        refGeneratorStation = GameObject.FindObjectOfType<GeneratorStation>();
        refPowerBar = GameObject.FindObjectOfType<PowerBar>();

        currentPower = maxPower;
    }

    private void Update()
    {
        if (outOfPower)
            StartCoroutine(OnOutOfPowerEvent());

        if (currentPower > maxPower)
            currentPower = maxPower;
    }

    private IEnumerator OnOutOfPowerEvent()
    {
        refPlayer.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSecondsRealtime(2f);

        refPlayer.GetComponent<MeshRenderer>().enabled = true;
        currentPower = maxPower;
        outOfPower = false;

        refPlayer.transform.position = waypoint.position;
        refGeneratorStation.RestoreGeneratorStateEvent();
    }
}

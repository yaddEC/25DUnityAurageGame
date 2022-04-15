using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerManager : MonoBehaviour
{
    [Header("Reference")]
    private GeneratorStation refGeneratorStation;
    private PowerBar refPowerBar;

    [Header("Charging Stats")]
    public float unchargePowerDelta;
    public static bool canLoosePower = true;

    [Header("Power Stats")]
    public static bool outOfPower;
    public float maxPower;
    public float currentPower;
    public Transform waypoint;

    private void Awake()
    {
        refPowerBar = GameObject.FindObjectOfType<PowerBar>();
        refGeneratorStation = GameObject.FindObjectOfType<GeneratorStation>();

        currentPower = maxPower;
    }

    private void Update()
    {
        PowerState();

        if (canLoosePower)
            StartCoroutine(ConsumePower(unchargePowerDelta));
    }

    private void PowerState()
    {
        if (outOfPower) OnOutOfPowerEvent();
        if (currentPower > maxPower) currentPower = maxPower;
    }

    private void OnOutOfPowerEvent()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    private IEnumerator ConsumePower(float powerAmount)
    {
        if (currentPower <= 0) outOfPower = true;

        currentPower -= powerAmount * 0.0001f;
        refPowerBar.SetLife(currentPower);
        yield return new WaitForSecondsRealtime(2f);
    }
}


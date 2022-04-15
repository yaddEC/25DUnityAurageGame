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
    public static bool  isInMachine = true;

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

        if (!isInMachine)
            StartCoroutine(ConsumePower(unchargePowerDelta));
        else
            PlayerMotion.canBeTargeted = false;
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
        PlayerMotion.canBeTargeted = true;
        yield return new WaitForSecondsRealtime(2f);
    }
}


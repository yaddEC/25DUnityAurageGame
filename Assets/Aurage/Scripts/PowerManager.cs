using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerManager : MonoBehaviour
{
    [Header("Reference")]
    private GeneratorStation refGeneratorStation;

    [Header("Charging Stats")]
    public float unchargePowerDelta;
    public static float unchargeCache;
    public static bool  isInMachine = true;

    [Header("Power Stats")]
    public static bool outOfPower;
    public float maxPower;
    public float currentPower;
    public Transform waypoint;

    public Material playerMaterial;

    private void Awake()
    {
        refGeneratorStation = GameObject.FindObjectOfType<GeneratorStation>();
        unchargeCache = unchargePowerDelta;
        currentPower = maxPower;
        isInMachine = false;
        playerMaterial = GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material;
    }

    private void Update()
    {
        PowerState();
        PlayerRender();

        if (!isInMachine) StartCoroutine(ConsumePower(unchargeCache));
        else PlayerMotion.canBeTargeted = false;
    }

    private void PowerState()
    {
        if (outOfPower) OnOutOfPowerEvent();
        if (currentPower > maxPower) currentPower = maxPower;
    }

    private void OnOutOfPowerEvent()
    {
        SceneSwitcher.GameOverScreen();
    }

    public IEnumerator ConsumePower(float powerAmount)
    {
        PlayerMotion.canBeTargeted = true;
        if (currentPower <= 0) outOfPower = true;

        currentPower -= powerAmount * 0.0001f;
        yield return new WaitForSecondsRealtime(2f);
    }

    private void PlayerRender()
    {
       playerMaterial.SetFloat("powerEmission", currentPower/10);
    }
}


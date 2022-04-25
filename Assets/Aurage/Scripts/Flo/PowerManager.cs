using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerManager : MonoBehaviour
{
    [Header("Charging Stats")]
    public float unchargePowerDelta;
    public static float unchargeCache;

    [Header("Power Stats")]
    public static bool outOfPower;
    public float maxPower;
    public float currentPower;
    public Transform waypoint;

    public Material playerMaterial;

    public static bool refresh = true;

    private void Awake()
    {
        unchargeCache = unchargePowerDelta;
        currentPower = maxPower;
        playerMaterial = GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material;

        GameOver.StartGamePlay();
    }

    private void Update()
    {
        PowerState();
        PlayerRender();

        if (!PlayerState.isInMachine && !PlayerState.isInNodePath) 
            StartCoroutine(ConsumePower(unchargeCache));
    }

    private void PowerState()
    {
        if (outOfPower) 
            OnOutOfPowerEvent();
        if (currentPower > maxPower) 
            currentPower = maxPower;
    }

    private void OnOutOfPowerEvent()
    {
        GameOver.StopGamePlay();
    }

    public IEnumerator ConsumePower(float powerAmount)
    {
        if (currentPower <= 0) 
            outOfPower = true;

        currentPower -= powerAmount * 0.0001f;
        yield return new WaitForSecondsRealtime(2f);
    }

    private void PlayerRender()
    {
       playerMaterial.SetFloat("powerEmission", currentPower/10);
    }
}


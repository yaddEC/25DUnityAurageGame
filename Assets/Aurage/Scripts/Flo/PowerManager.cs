using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerManager : MonoBehaviour
{
    [Header("Charging Stats")]
    public float unchargePowerDelta;
    public static float unchargeCache;

    public bool isInMenu = false;

    [Header("Power Stats")]
    public static bool outOfPower;
    public bool outOfPowerDebug;
    public float maxPower;
    public float currentPower;
    public static Transform waypoint;
    public Transform DebugWaypoint;

    public Material playerMaterial;

    public static bool refresh = true;

    private void Awake()
    {
        unchargeCache = unchargePowerDelta;
        currentPower = maxPower;
        playerMaterial = GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material;
    }

    private void Update()
    {
        outOfPowerDebug = outOfPower;
        DebugWaypoint = waypoint;

        PowerState();
        PlayerRender();

        if (!PlayerState.isInMachine && !PlayerState.isInNodePath)
        {
            if (isInMenu)
                currentPower = 100;
            else
                StartCoroutine(ConsumePower(unchargeCache));
        }
    }

    private void PowerState()
    {
        if (outOfPower) OnOutOfPowerEvent();
        else return;

        if (currentPower > maxPower) 
            currentPower = maxPower;
    }

    private void OnOutOfPowerEvent()
    {
        Debug.Log("Ici");
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


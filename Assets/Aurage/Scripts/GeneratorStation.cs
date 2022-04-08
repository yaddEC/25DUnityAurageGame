using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneratorStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    private PowerBar refPowerBar;
    private PlayerMotion refPlayerMotion;

    public Generator refGenerator;

    [Header("Generator UI/UX")]
    private MeshRenderer meshRenderer;

    [Header("Generator Stats")]
    public bool checkointActivated = false;
    public float chargingPowerDelta;
    public bool canCharge = true;
    private GameObject[] generatorsList;

    private bool isFreezed = false;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refPowerBar = GameObject.FindObjectOfType<PowerBar>();
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();

        generatorsList = GameObject.FindGameObjectsWithTag("Generator");

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = refGenerator.machineMaterials[1];
    }

    private void Update()
    {
        if (refPowerManager.currentPower >= refPowerManager.maxPower && refGenerator.isMachinUsed)
        {
            Debug.Log("ehere");

            canCharge = false;
            refPowerManager.currentPower = refPowerManager.maxPower;
        }

        if (isFreezed)
            FreezePlayer();

        if (refPowerManager.isCharging)
            StartCoroutine(PluggedEvent(chargingPowerDelta));
        else if (!refPowerManager.isCharging && !refPowerManager.outOfPower)
            StartCoroutine(UnpluggedEvent(refPowerManager.unchargePowerDelta));
    }

    private IEnumerator PluggedEvent(float powerAmount)
    {
        if (refPowerManager.currentPower >= refPowerManager.maxPower)
            refPowerManager.isCharging = false;

        refPowerManager.currentPower += powerAmount * 0.0001f;
        yield return new WaitForSecondsRealtime(2f);
    }

    private IEnumerator UnpluggedEvent(float powerAmount)
    {
        if (refPowerManager.currentPower <= 0)
            refPowerManager.outOfPower = true;

        refPowerManager.currentPower -= powerAmount * 0.0001f;
        refPowerBar.SetLife(refPowerManager.currentPower);
        yield return new WaitForSecondsRealtime(2f);
    }

    public void RestoreGeneratorStateEvent()
    {
        foreach (GameObject obj in generatorsList)
        {
            if (!obj.GetComponent<GeneratorStation>().checkointActivated)
            {
                obj.GetComponent<GeneratorStation>().canCharge = true;
                meshRenderer.material = refGenerator.machineMaterials[1];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        checkointActivated = true;
        refPowerManager.waypoint = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && canCharge && InputManager.performB)
        {
            refGenerator.isMachinUsed = true;
            refPowerManager.isCharging = true;
        }

        if (canCharge && !InputManager.performDash)
            isFreezed = true;
        else if (InputManager.performDash)
            isFreezed = false;
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

    private void FreezePlayer()
    {
        refPlayerMotion.transform.position = transform.position;
    }
}

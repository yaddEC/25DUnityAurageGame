using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    private PowerBar refPowerBar;
    private PlayerMotion refPlayerMotion;

    public Generator refGenerator;

    [Header("Generator UI/UX")]
    private MeshRenderer meshRenderer;
    private Text text;

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
        meshRenderer.material = refGenerator.machineMaterials[2];

        text = GetComponentInChildren<Text>();
        text.enabled = false;
    }

    private void Update()
    {
        if (refPowerManager.currentPower >= refPowerManager.maxPower && refGenerator.isMachinUsed)
        {
            canCharge = false;
            refPowerManager.currentPower = refPowerManager.maxPower;
        }

        FreezePlayer();

        if (refPowerManager.isCharging)
            StartCoroutine(PluggedEvent(chargingPowerDelta));
        else if (!refPowerManager.isCharging && !refPowerManager.outOfPower && PowerManager.canLoosePower)
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
                meshRenderer.material = refGenerator.machineMaterials[2];
            }
        }
    }
    private void FreezePlayer()
    {
        if (isFreezed)
        {
            refPlayerMotion.transform.position = transform.position;
            refPlayerMotion.rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
            refPlayerMotion.rb.constraints = RigidbodyConstraints.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMotion.isInMachine = true;
            text.enabled = true;
            isFreezed = true;

            if (!checkointActivated)
                refPowerManager.waypoint = transform;

            checkointActivated = true;
            meshRenderer.material = refGenerator.machineMaterials[3];
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(canCharge && InputManager.performB)
            {
                refPowerManager.isCharging = true;
                meshRenderer.material = refGenerator.machineMaterials[1];
            }

            if (InputManager.performA)
                isFreezed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMotion.isInMachine = false;
            refPowerManager.isCharging = false;
            canCharge = false;
            text.enabled = false;
            meshRenderer.material = refGenerator.machineMaterials[0];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    private PlayerMotion refPlayerMotion;

    [Header("Generator UI/UX")]
    private Text text;

    [Header("Generator Stats")]
    public bool checkointActivated = false;
    public float chargingPowerDelta;
    public bool canCharge = true;
    public bool isInMachine = false;
    public bool isCharging = false;
    private GameObject[] generatorsList;

    private bool isFreezed = false;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
        generatorsList = GameObject.FindGameObjectsWithTag("Generator");

        text = GetComponentInChildren<Text>();
        text.enabled = false;
    }

    private void Update()
    {
        ClampInMachine();

        if(isInMachine && canCharge)
            StartCoroutine(RestorePower(chargingPowerDelta));
    }

    private IEnumerator RestorePower(float powerAmount)
    {
        refPowerManager.currentPower += powerAmount * 0.0001f;
        yield return new WaitForSecondsRealtime(2f);
    }

    private void ClampInMachine()
    {
        if (isFreezed)
        {
            refPlayerMotion.transform.position = transform.position;
            refPlayerMotion.rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
            refPlayerMotion.rb.constraints = RigidbodyConstraints.None;
    }
    public void RestoreGenerators()
    {
        foreach (GameObject obj in generatorsList)
        {
            if (!obj.GetComponent<GeneratorStation>().checkointActivated)
                obj.GetComponent<GeneratorStation>().canCharge = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PowerManager.canLoosePower = false;
            isInMachine = true;
            isFreezed = true;
            text.enabled = true;

            if (!checkointActivated) refPowerManager.waypoint = transform;
            checkointActivated = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(canCharge && InputManager.performB) isCharging = true;
            if (InputManager.performA) isFreezed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PowerManager.canLoosePower = true;
            isInMachine = false;
            isCharging = false;
            text.enabled = false;
        }
    }
}

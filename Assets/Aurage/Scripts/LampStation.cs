using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    public Lamp refLamp;

    [Header("Lamp UI/UX")]
    private MeshRenderer meshRenderer;

    [Header("Lamp Stats")]
    public float restorePower;
    public bool canCharge = true;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = refLamp.machineMaterials[1];
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LightSwitchEvent());

        other.GetComponent<PlayerMotion>().isInLamp = true;

        other.GetComponent<PlayerMotion>().lockPosition = transform.position;
    }

    private IEnumerator LightSwitchEvent()
    {
        refLamp.isMachinUsed = true;
        RestorePowerEvent();

        yield return new WaitForSeconds(.5f);
        meshRenderer.material = refLamp.machineMaterials[0];
        canCharge = false;
    }

    private void RestorePowerEvent()
    {
        refPowerManager.currentPower += restorePower;
    }
}

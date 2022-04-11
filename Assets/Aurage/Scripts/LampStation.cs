using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    private PlayerMotion refPlayerMotion;
    public Lamp refLamp;

    [Header("Lamp UI/UX")]
    private MeshRenderer meshRenderer;

    [Header("Lamp Stats")]
    public float restorePower;
    public bool canCharge = true;

    private bool isFreezed = false;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = refLamp.machineMaterials[1];
    }

    private void Update()
    {
        if (isFreezed)
            FreezePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(canCharge)
            StartCoroutine(LightSwitchEvent());
    }

    private IEnumerator LightSwitchEvent()
    {
        isFreezed = true;
        refLamp.isMachinUsed = true;
        RestorePowerEvent();

        yield return new WaitForSeconds(.5f);
        meshRenderer.material = refLamp.machineMaterials[0];
        canCharge = false;
        isFreezed = false;
    }

    private void RestorePowerEvent()
    {
        refPowerManager.currentPower += restorePower;
    }
    private void FreezePlayer()
    {
        refPlayerMotion.transform.position = transform.position;
    }
}
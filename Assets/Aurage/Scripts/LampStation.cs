using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        FreezePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMotion.isInMachine = true;
            isFreezed = true;

            if (canCharge)
                RestorePowerEvent();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (InputManager.performA)
                isFreezed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMotion.isInMachine = false;
            canCharge = false;
            meshRenderer.material = refLamp.machineMaterials[0];
        }
    }

    private void RestorePowerEvent()
    {
        refPowerManager.currentPower += restorePower;
        canCharge = false;
        meshRenderer.material = refLamp.machineMaterials[0];
    }
    private void FreezePlayer()
    {
        if(isFreezed)
        {
            refPlayerMotion.transform.position = transform.position;
            refPlayerMotion.rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
            refPlayerMotion.rb.constraints = RigidbodyConstraints.None;
    }
}

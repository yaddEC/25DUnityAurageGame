using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    private PlayerMotion refPlayerMotion;

    [Header("Lamp Stats")]
    public float chargingPowerDelta;
    public bool canCharge = true;
    public bool isInMachine = false;

    private bool isFreezed = false;
    private Transform lockPosition;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
        lockPosition = UnityFinder.FindGameObjectInChildWithTag(this.gameObject, "LockPosition");
    }

    private void Update()
    {
        ClampInMachine();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.localScale = Vector3.zero;
            PowerManager.canLoosePower = false;
            isInMachine = true;
            isFreezed = true;

            if (canCharge) RestorePower();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && InputManager.performA) isFreezed = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.localScale = Vector3.one;
            PowerManager.canLoosePower = true;
            isInMachine = false;
            canCharge = false;
        }
    }

    private void RestorePower()
    {
        refPowerManager.currentPower += chargingPowerDelta;
        canCharge = false;
    }
    private void ClampInMachine()
    {
        if (isFreezed)
        {
            refPlayerMotion.transform.position = lockPosition.position;
            refPlayerMotion.rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
            refPlayerMotion.rb.constraints = RigidbodyConstraints.None;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Station : MonoBehaviour
{
    [Header("Reference")]
    public PowerManager refPowerManager;
    public PlayerMotion refPlayerMotion;

    [Header("Machine UI/UX")]
    public Text text;
    public Image image;
    public BoxCollider bC;
    public MeshRenderer mR;

    [Header("Machine Info")]
    public string tagToSearch;
    public bool isFreezed;
    public bool isInMachine;
    public bool isUsable;
    public bool doEvent;

    public float cooldown;
    public float cachedCooldown;

    public Transform lockPosition;
    public GameObject[] machineList;

    public virtual void RegisterReferences()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
        if(tagToSearch != null) machineList = GameObject.FindGameObjectsWithTag(tagToSearch);

        text = GetComponentInChildren<Text>(); text.enabled = false;
        image = GetComponentInChildren<Image>(); image.enabled = false;
        lockPosition = UnityFinder.FindTransformInChildWithTag(gameObject, "LockPosition");

        isUsable = true;
        cachedCooldown = cooldown;

        bC = GetComponent<BoxCollider>();
        mR = refPlayerMotion.GetComponent<MeshRenderer>();
    }

    public virtual void CooldownHandler(bool b)
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            if(!b) bC.isTrigger = false;
        }
        else if(!b && cooldown < 0)
            bC.isTrigger = true;
    }
    public virtual void ClampInMachine()
    {
        if (isFreezed)
        {
            refPlayerMotion.transform.position = lockPosition.position;
            refPlayerMotion.rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
            refPlayerMotion.rb.constraints = RigidbodyConstraints.None;
    }
    public virtual void RestoreMachines()
    {
        foreach (GameObject obj in machineList)
        {
            if (!obj.GetComponent<GeneratorStation>().checkointActivated)
                obj.GetComponent<GeneratorStation>().isUsable = true;
        }
    }

    public virtual void EnterMachine()
    {
        mR.enabled = false;
        PowerManager.isInMachine = true;
        isInMachine = true;
        isFreezed = true;
        text.enabled = true;
        image.enabled = true;
    }
    public virtual void StayMachine(bool autoExec)
    {
        if(autoExec) doEvent = true;
        if (InputManager.performX) isFreezed = false;
    }
    public virtual void ExitMachine()
    {
        mR.enabled = true;
        PowerManager.isInMachine = false;
        isInMachine = false;
        doEvent = false;
        text.enabled = false;
        image.enabled = false;
        cooldown = cachedCooldown;
    }
}
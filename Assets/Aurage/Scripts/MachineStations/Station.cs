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
    public bool autoExec = false;
    public bool special = false;
    public string tagToSearch;
    public bool isInStation;
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
    public virtual void CooldownHandler(bool special)
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;

        if(!special)
        {
            if (cooldown <= 0)
                bC.isTrigger = true;
            else
                bC.isTrigger = false;
        }
    }
    public virtual void RestoreMachines()
    {
        foreach (GameObject obj in machineList)
        {
            if (!obj.GetComponent<GeneratorStation>().checkointActivated)
                obj.GetComponent<GeneratorStation>().isUsable = true;
        }
    }

    public virtual void EnterMachine(Station station)
    {
        isInStation = true;
        PlayerState.isInMachine = isInStation;

        refPlayerMotion.refStation = station;
        PlayerState.FreezePlayer();

        mR.enabled = false;
        text.enabled = true;
        image.enabled = true;
    }
    public virtual void StayMachine(bool autoExec)
    {
        PlayerState.FreezePlayer();

        if (autoExec && isInStation) doEvent = true;
        if (isInStation && InputManager.performX && InputManager.inputAxis != Vector2.zero) ExitMachine();

    }
    public virtual void ExitMachine()
    {
        PlayerState.UnFreezePlayer();
        refPlayerMotion.DashMachine(InputManager.inputAxis, 1.1f, false);

        isInStation = false;
        PlayerState.isInMachine = isInStation;
        doEvent = false;

        mR.enabled = true;
        text.enabled = false;
        image.enabled = false;

        cooldown = cachedCooldown;
    }
}
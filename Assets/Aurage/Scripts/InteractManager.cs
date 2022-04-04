using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractManager : ScriptableObject
{
    public string machineName;
    public float powerToUse;
    public bool isMachinUsed;

    public Material[] machineMaterials;

    public PlayerMotion refPlayerMotion;

    public virtual void Interact()
    {

    }
    public virtual void lockInStation()
    {

    }

    public virtual void registerReference()
    {

    }
}
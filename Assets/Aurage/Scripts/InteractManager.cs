using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractManager : ScriptableObject
{
    public string machineName;
    public float powerToUse;
    public GameObject machineObject;
    public bool isMachinUsed;

    public Material[] machineMaterials;

    public virtual void Interact()
    {

    }
}
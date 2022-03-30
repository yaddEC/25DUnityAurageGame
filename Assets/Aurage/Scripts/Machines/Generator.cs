using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "Machine/Generator")]
public class Generator : InteractManager
{
    public Generator(string _machineName, bool _isMachinUsed, float _powerToUse, GameObject _machineObject)
    {
        machineName = _machineName;
        isMachinUsed = _isMachinUsed;
        powerToUse = _powerToUse;
        machineObject = _machineObject;
    }
}
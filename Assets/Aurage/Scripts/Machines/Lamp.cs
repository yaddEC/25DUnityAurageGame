using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lamp", menuName = "Machine/Lamp")]
public class Lamp : InteractManager
{
    public Lamp(string _machineName, bool _isMachinUsed, float _powerToUse, GameObject _machineObject)
    {
        machineName = _machineName;
        isMachinUsed = _isMachinUsed;
        powerToUse = _powerToUse;
        machineObject = _machineObject;
    }
}

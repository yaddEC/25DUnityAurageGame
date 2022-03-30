using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drone", menuName = "Machine/Drone")]
public class Drone : InteractManager
{
    public Drone(string _machineName, bool _isMachinUsed, float _powerToUse, GameObject _machineObject)
    {
        machineName = _machineName;
        isMachinUsed = _isMachinUsed;
        powerToUse = _powerToUse;
        machineObject = _machineObject;
    }
}

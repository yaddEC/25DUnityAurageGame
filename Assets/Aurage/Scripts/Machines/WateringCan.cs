using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WateringCan", menuName = "Machine/WateringCan")]
public class WateringCan : InteractManager
{
    public WateringCan(string _machineName, bool _isMachinUsed, float _powerToUse)
    {
        machineName = _machineName;
        isMachinUsed = _isMachinUsed;
        powerToUse = _powerToUse;
    }
}
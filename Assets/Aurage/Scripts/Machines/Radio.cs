using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Radio", menuName = "Machine/Radio")]
public class Radio : InteractManager
{
    public Radio(string _machineName, bool _isMachinUsed, float _powerToUse)
    {
        machineName = _machineName;
        isMachinUsed = _isMachinUsed;
        powerToUse = _powerToUse;
    }
}

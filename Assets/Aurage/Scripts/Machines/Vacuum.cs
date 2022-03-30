using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vacuum", menuName = "Machine/Vacuum")]
public class Vacuum : InteractManager
{
    public Vacuum(string _machineName, bool _isMachinUsed, float _powerToUse)
    {
        machineName = _machineName;
        isMachinUsed = _isMachinUsed;
        powerToUse = _powerToUse;
    }
}

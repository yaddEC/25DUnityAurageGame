using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lamp", menuName = "Machine/Lamp")]
public class Lamp : InteractManager
{
    public Lamp(string _machineName, bool _isMachinUsed, float _powerToUse, Material[] _machineMaterials)
    {
        machineName = _machineName;
        isMachinUsed = _isMachinUsed;
        powerToUse = _powerToUse;

        for (int i = 0; i < machineMaterials.Length; i++)
        {
            machineMaterials[i] = _machineMaterials[i];
        }
    }
}

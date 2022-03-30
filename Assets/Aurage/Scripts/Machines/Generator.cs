using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "Machine/Generator")]
public class Generator : InteractManager
{
    public Generator(string _machineName, bool _isMachinUsed, float _powerToUse, Material[] _machineMaterials)
    {
        machineName = _machineName;
        isMachinUsed = _isMachinUsed;
        powerToUse = _powerToUse;

        for (int i = 0; i < machineMaterials.Length; i++)
        {
            machineMaterials[i] = _machineMaterials[i];
        }
    }

    public override void Interact()
    {
        //do this
    }
}
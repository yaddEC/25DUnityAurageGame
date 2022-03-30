using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Socket", menuName = "Machine/Socket")]
public class Socket : InteractManager
{
    public Socket(string _machineName, bool _isMachinUsed, float _powerToUse)
    {
        machineName = _machineName;
        isMachinUsed = _isMachinUsed;
        powerToUse = _powerToUse;
    }
}

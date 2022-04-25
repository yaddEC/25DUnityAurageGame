using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CammeraSoundPlayer : MonoBehaviour
{
    public EnemyCamera CameraState;
    public PlayShortSound Sound;

    bool isActive = false;

    void Update()
    {
        if (CameraState.isSeeingPlayer && !isActive)
        {
            Sound.PlaySound();
            isActive = true;
        }
        if (!CameraState.isSeeingPlayer)
        {
            Sound.StopSound();
            isActive = false;
        }
    }
    
}

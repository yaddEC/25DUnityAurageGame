using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCableSoundPlay : MonoBehaviour
{
    public PlayerState playerState;
    public PlayLoopSoundWithBegin RunSound;
    public PlayLoopSound cableSound;
    public void PlaySoundWithState(InputAction.CallbackContext context)
    {
        
        if (playerState.InspectorIsInNodePath)
            cableSound.PlaySound(context);
        else
            RunSound.PlaySound(context);
        if (context.canceled)
        {
            cableSound.StopSound();
            RunSound.StopSound();
        }
    }
}

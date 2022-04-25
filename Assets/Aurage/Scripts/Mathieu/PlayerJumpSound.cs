using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpSound : MonoBehaviour
{
    public PlayerState playerState;
    public PlayShortSound Sound;

    public void PlaySoundWithState()
    {
        if (playerState.InspectorIsGrounded)
            Sound.PlaySound();
    }
}

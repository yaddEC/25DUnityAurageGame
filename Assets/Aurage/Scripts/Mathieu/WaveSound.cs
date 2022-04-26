using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSound : MonoBehaviour
{
    public PlayerState playerState;
    public Shockwave waveState;
    public PlayShortSound Sound;

    public void PlaySound()
    {
        if (waveState.refresh && !playerState.InspectorIsInMachine)
            Sound.PlaySound();
    }
}

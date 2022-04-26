using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSound : MonoBehaviour
{
    public Shockwave waveState;
    public PlayShortSound Sound;

    public void PlaySound()
    {
        if (waveState.refresh)
            Sound.PlaySound();
    }
}

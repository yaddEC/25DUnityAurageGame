using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hackMachineSoundPlayer : MonoBehaviour
{
    public ConsoleStation Station;
    public PlayShortSound Sound;

    private void Update()
    {
        if (Station.doEvent)
            Sound.PlaySound();
    }
}

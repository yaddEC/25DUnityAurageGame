using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingSoundPlqyer : MonoBehaviour
{
    public GeneratorStation Station;
    public PlayShortSound Sound;

    bool done;

    private void Update()
    {
        if (Station.isInStation && !done)
        {
            Sound.PlaySound();
            done = true;
        }
        else if(!Station.isInStation && done)
        {
            done = false;
        }
    }
}

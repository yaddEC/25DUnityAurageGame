using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingSoundPlqyer : MonoBehaviour
{
    public GeneratorStation Station;
    public PlayShortSound Sound;

    private void Update()
    {
        if (Station.isInStation)
            Sound.PlaySound();
    }
}
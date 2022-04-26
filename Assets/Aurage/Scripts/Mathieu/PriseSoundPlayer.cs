using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriseSoundPlayer : MonoBehaviour
{
    public SocketStation Station;
    public PlayShortSound Sound;

    private void Update()
    {
        if (Station.perfomed)
            Sound.PlaySound();
    }
}

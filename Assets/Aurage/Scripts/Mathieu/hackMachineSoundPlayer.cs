using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hackMachineSoundPlayer : MonoBehaviour
{
    public ConsoleStation Station;
    public PlayShortSound Sound;

    float delay;

    private void Update()
    {
        if (Station.doEvent)
            if(delay <= 0)
            {
                Sound.PlaySound();
                delay = 2;
            }
            else
            {
                delay -= Time.deltaTime;
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSoundPlayer : MonoBehaviour
{
    public bool isOpen;

    public ConsoleStation Station;
    public PlayShortSound SoundOpen;
    public PlayShortSound SoundClose;

    float delay;

    private void Update()
    {
        if(Station.doEvent && !Station.animationPlaying && delay <= 0)
        {
            if (Station.doEvent && isOpen)
            {
                SoundClose.PlaySound();
                isOpen = false;
            }
            if (Station.doEvent && !isOpen)
            {
                SoundOpen.PlaySound();
                isOpen = true;
            }
            delay = 2;
        }
        else
        {
            delay -= Time.deltaTime;
        }
    }
}

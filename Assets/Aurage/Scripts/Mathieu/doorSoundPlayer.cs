using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSoundPlayer : MonoBehaviour
{
    public bool isOpen;

    public ConsoleStation Station;
    public PlayShortSound SoundOpen;
    public PlayShortSound SoundClose;

    private void Update()
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
    }
}

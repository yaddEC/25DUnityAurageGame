using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSoundPlayer : MonoBehaviour
{
    public RadioStation Station;
    public PlayLoopSound Sound;

    private void Update()
    {
        if (Station.doEvent)
            Sound.PlaySoundWhioutContext();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaSoundPlayer : MonoBehaviour
{
    public RoombaStation Station;
    public PlayLoopSoundWithBegin Sound;

    private void Update()
    {
        Sound.PlaySoundNoEvent(Station.doEvent);
    }
}

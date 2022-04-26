using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaSoundPlayer : MonoBehaviour
{
    public TeslaZone TeslaState;
    public PlaySoundWithBeginAndEnd Sound;

    private void Update()
    {
        Sound.PlaySound(TeslaState.isIn);
    }
}

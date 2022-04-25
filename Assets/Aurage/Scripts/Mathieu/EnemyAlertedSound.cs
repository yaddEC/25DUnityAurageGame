using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlertedSound : MonoBehaviour
{
    public BasicEnemy EnemyState;
    public PlayLoopSound alertSound;
    public void Update()
    {
        if (EnemyState.playerDetected)
            alertSound.PlaySoundWhioutContext();
        else
            alertSound.StopSound();
    }
}

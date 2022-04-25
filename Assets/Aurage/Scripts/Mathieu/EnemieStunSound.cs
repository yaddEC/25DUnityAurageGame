using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieStunSound : MonoBehaviour
{
    public Enemy EnemieState;
    public PlayShortSound Sound;

    bool isActive = false;

    void Update()
    {
        if (EnemieState.isStunned && !isActive)
        {
            Sound.PlaySound();
            isActive = true;
        }
        if (!EnemieState.isStunned)
        {
            isActive = false;
        }
    }
}

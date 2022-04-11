using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public static bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isDead = true;
    }
}


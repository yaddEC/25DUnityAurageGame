using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    public bool inObject = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inObject = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inObject = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPath : MonoBehaviour
{
    public int index;

    private void Start()
    {
        index++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit");
        }
    }
}

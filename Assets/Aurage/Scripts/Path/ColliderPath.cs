using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPath : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {

        }
    }
}

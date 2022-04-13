using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    public static bool inObject = false;
    public static Collider coll;

    public bool isInputSocket;

    private void OnTriggerEnter(Collider other)
    {
        if (isInputSocket)
            SocketStation.index = 1;
        else
            SocketStation.index = 0;

        if (other.tag == "Player")
        {
            coll = other;
            inObject = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            inObject = false;
    }
}

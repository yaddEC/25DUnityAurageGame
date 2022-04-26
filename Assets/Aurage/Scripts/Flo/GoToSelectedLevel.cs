using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSelectedLevel : MonoBehaviour
{
    public bool canAccess = false;
    public bool isInLevel = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            isInLevel = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            isInLevel = false;
    }
}

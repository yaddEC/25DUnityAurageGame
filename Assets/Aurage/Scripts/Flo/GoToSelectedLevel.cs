using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSelectedLevel : MonoBehaviour
{
    public int levelIndex = 0;
    public bool goToNextLevel = false;
    public bool canAccess = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerState.isInMachine = true;

            if (InputManager.performB && canAccess)
                goToNextLevel = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            PlayerState.isInMachine = false;
    }
}

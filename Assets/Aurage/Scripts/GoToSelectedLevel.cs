using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSelectedLevel : MonoBehaviour
{
    public int levelIndex = 0;
    public bool goToNextLevel = false;
    public bool canAccess = false;
    public bool isInLevel = false;

    private void Update()
    {
        var unlockedIndex = PlayerPrefs.GetInt("UnlockedLevel");
        if (unlockedIndex >= levelIndex)
            canAccess = true;
        else
            canAccess = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isInLevel = true;

            if (InputManager.performB && canAccess)
            {
                goToNextLevel = true;
                LevelSelector.actualLevel = levelIndex;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInLevel = false;
        }
    }
}

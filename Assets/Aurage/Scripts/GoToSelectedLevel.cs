using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSelectedLevel : MonoBehaviour
{
    public int levelIndex = 0;
    public bool goToNextLevel = false;
    public bool canAccess = false;

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
        if (other.tag == "Player" && InputManager.performB && canAccess) goToNextLevel = true;
    }
}

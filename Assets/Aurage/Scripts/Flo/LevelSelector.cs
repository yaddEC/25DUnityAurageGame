using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GoToSelectedLevel[] goToSelectedLevels;
    public static int unlockedLevel = 1;
    public int debugUnlockedLevel = 1;

    private void Update()
    {
        debugUnlockedLevel = unlockedLevel;
        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);

        for (int i = 0; i != 6; i++)
            goToSelectedLevels[i].canAccess = true;

        foreach (var level in goToSelectedLevels)
        {
            if(level.isInLevel && InputManager.performB)
                SceneSwitcher.GoToSelectedScene(level.name);
        }

        Debug.Log(PlayerPrefs.GetInt("UnlockedLevel"));
    }
}

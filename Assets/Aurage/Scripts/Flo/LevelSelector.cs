using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GoToSelectedLevel[] goToSelectedLevels;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("UnlockedLevel") == 0)
            PlayerPrefs.SetInt("UnlockedLevel", 1);
    }

    private void Update()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("UnlockedLevel"); i++)
            goToSelectedLevels[i].canAccess = true;

        foreach (var level in goToSelectedLevels)
        {
            if (level.goToNextLevel)
                SceneSwitcher.GoToSelectedScene(level.name);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public static int actualLevel = 1;
    private GoToSelectedLevel[] goToSelectedLevels;

    private void Awake()
    {
        goToSelectedLevels = GameObject.FindObjectsOfType<GoToSelectedLevel>();
    }

    private void Update()
    {
        foreach (GoToSelectedLevel level in goToSelectedLevels)
        {
            if (level.goToNextLevel)
            {
                actualLevel = level.levelIndex;
                EnterLevel(actualLevel);
            }
        }    
    }

    private void EnterLevel(int index)
    {
        SceneSwitcher.GoToSelectedScene(index.ToString());
    }
}

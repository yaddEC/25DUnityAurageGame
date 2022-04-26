using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public OptionMenu refOptionMenu;

    public EventSystem m_EventSystem;

    public GameObject pauseMenu;
    public bool isOpen = false;
    public static bool pauseOpen = false;

    public PowerBar refPowerBar;

    private void Awake()
    {
        refPowerBar = GameObject.FindObjectOfType<PowerBar>();
        refOptionMenu = GameObject.FindObjectOfType<OptionMenu>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame)
            isOpen = !isOpen;

        if(isOpen)
            OpenPause();
        else
            ClosePause();
    }
    public void OpenPause()
    {
        refPowerBar.gameObject.SetActive(false);

        Time.timeScale = 0;
        refOptionMenu.CloseOption();

        if (!pauseOpen)
        {
            pauseMenu.SetActive(true);
            pauseOpen = true;
        }
    }

    public void ClosePause()
    {
        refPowerBar.gameObject.SetActive(true);

        if (!OptionMenu.optionOpen) 
            Time.timeScale = 1;

        isOpen = false;
        pauseOpen = false;
        pauseMenu.SetActive(false);
    }

    public void OpenOptionMenu()
    {
        ClosePause();
        refPowerBar.gameObject.SetActive(false);
        refOptionMenu.OpenOption();
    }
}

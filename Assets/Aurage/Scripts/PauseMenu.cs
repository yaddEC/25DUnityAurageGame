using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public EventSystem m_EventSystem;
    public GameObject menu;

    public void onBackPresed()
    {
        var i = Convert.ToInt32(!menu.activeSelf);
        menu.SetActive(!menu.activeSelf);
        Time.timeScale = i;
    }

    private void Update()
    {
        if (InputManager.performMid)
        {
            if (GameObject.FindGameObjectWithTag("OptionMenu") != null) GameObject.FindGameObjectWithTag("OptionMenu").GetComponent<OptionMenu>().onDisable();
            GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().onActivate();
            InputManager.performMid = false;
        }
    }
    private void onActivate()
    {
        Debug.Log("Pause");
        onBackPresed();
        m_EventSystem.firstSelectedGameObject.GetComponent<Button>().Select();
        m_EventSystem.firstSelectedGameObject.GetComponent<Button>().interactable = false;
        m_EventSystem.firstSelectedGameObject.GetComponent<Button>().interactable = true;
    }

    public void onRestartPresed()
    {
        Scene newScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(newScene.name);
        SceneManager.LoadScene(newScene.name);
        onBackPresed();
    }

    public void goToMainMenu()
    {
        Scene newScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(newScene.name);
        SceneManager.LoadScene("Menu");
        onBackPresed();
    }

    public void oppenOption()
    {
        menu.SetActive(!menu.activeSelf);
        GameObject.FindGameObjectWithTag("OptionMenu").GetComponent<OptionMenu>().onActivate();
        //Time.timeScale = 1;
        //onBackPresed();
    }
}

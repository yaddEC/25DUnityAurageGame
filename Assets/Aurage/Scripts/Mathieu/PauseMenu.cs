using System.Collections;
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

    int boolToInt(bool value)
    {
        if (value)
            return 1;
        return 0;
    }
    public void onBackPresed()
    {
        menu.SetActive(!menu.activeSelf);
        Time.timeScale = boolToInt(!menu.activeSelf);
    }
    public void onActivate()
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

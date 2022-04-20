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

    int boolToInt(bool value)
    {
        if (value)
            return 1;
        return 0;
    }
    public void onBackPresed()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        Time.timeScale = boolToInt(!this.gameObject.activeSelf);
    }
    public void onActivate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pause");
            onBackPresed();
            m_EventSystem.firstSelectedGameObject.GetComponent<Button>().Select();
            m_EventSystem.firstSelectedGameObject.GetComponent<Button>().interactable = false;
            m_EventSystem.firstSelectedGameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void onRestartPresed()
    {
        KillZone.isDead = false;
        Scene newScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(newScene.name);
        SceneManager.LoadScene(newScene.name);
        onBackPresed();
    }
}

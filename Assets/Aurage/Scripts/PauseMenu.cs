using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    int boolToInt(bool value)
    {
        if (value)
            return 1;
        return 0;
    }
    public void onActivate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pause");
            this.gameObject.SetActive(!this.gameObject.activeSelf);
            Time.timeScale = boolToInt(!this.gameObject.activeSelf);
        }
    }
    public void onBackPresed()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        Time.timeScale = boolToInt(!this.gameObject.activeSelf);
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

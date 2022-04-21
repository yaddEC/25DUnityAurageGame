using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public EventSystem m_EventSystem;
    public GameObject menu;

    public void onDisable() {
        //this.gameObject.SetActive(!this.gameObject.activeSelf);
        m_EventSystem.enabled = false;
        menu.SetActive(false);
        Time.timeScale = 1;
    }
    public void onActivate()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
        m_EventSystem.enabled = true;
        m_EventSystem.firstSelectedGameObject.GetComponent<Slider>().Select();
        m_EventSystem.firstSelectedGameObject.GetComponent<Slider>().interactable = false;
        m_EventSystem.firstSelectedGameObject.GetComponent<Slider>().interactable = true;
    }
}

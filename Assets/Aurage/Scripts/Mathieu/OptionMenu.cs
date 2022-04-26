using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public EventSystem m_EventSystem;

    public GameObject optionMenu;
    public bool isOpen = false;
    public static bool optionOpen = false;

    public AudioMixer GenMixer;
    public AudioMixer SoundMixer;
    public AudioMixer MusicMixer;

    private void Awake()
    {
        optionMenu = GameObject.FindGameObjectWithTag("OptionMenu");
        optionMenu.SetActive(false);
    }

    public void OpenOption()
    {
        Time.timeScale = 0;

        if (!optionOpen)
        {
            optionMenu.SetActive(true);
            optionOpen = true;
        } 

        m_EventSystem.firstSelectedGameObject.GetComponent<Slider>().Select();
        m_EventSystem.firstSelectedGameObject.GetComponent<Slider>().interactable = false;
        m_EventSystem.firstSelectedGameObject.GetComponent<Slider>().interactable = true;
    }

    public void CloseOption()
    {
        optionMenu.SetActive(false);
        optionOpen = false;
        isOpen = false;
    }

    public void OnAllVolumeChanged(System.Single vol)
    {
        SoundMixer.SetFloat("MasterVolume", Mathf.Round(vol * 15)-15);
    }
    public void OnSoundVolumeChanged(System.Single vol)
    {
        SoundMixer.SetFloat("SoundVolume", Mathf.Round(vol * 15) - 15);
    }
    public void OnMusicVolumeChanged(System.Single vol)
    {
        SoundMixer.SetFloat("MusicVolume", Mathf.Round(vol * 15) - 15);
    }
}

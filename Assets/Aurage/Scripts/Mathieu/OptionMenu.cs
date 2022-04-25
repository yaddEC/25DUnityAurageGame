using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public EventSystem m_EventSystem;
    public GameObject menu;

    public AudioMixer GenMixer;
    public AudioMixer SoundMixer;
    public AudioMixer MusicMixer;

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

    public void onAllVolumeChanged(System.Single vol)
    {
        Debug.Log(Mathf.Round(vol*10).ToString());
        SoundMixer.SetFloat("MasterVolume", Mathf.Round(vol * 15)-15);
    }
    public void onSoundVolumeChanged(System.Single vol)
    {
        Debug.Log(Mathf.Round(vol * 10).ToString());
        SoundMixer.SetFloat("SoundVolume", Mathf.Round(vol * 15) - 15);
    }
    public void onMusicVolumeChanged(System.Single vol)
    {
        Debug.Log(Mathf.Round(vol * 10).ToString());
        SoundMixer.SetFloat("MusicVolume", Mathf.Round(vol * 15) - 15);
    }
}

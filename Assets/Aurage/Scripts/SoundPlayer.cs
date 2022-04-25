using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource Source;
    public AudioSource EventSource;
    public bool playRandom;
    public bool StopWhenPause;
    public AudioClip[] Sound;

    bool alredyPerfomed;

    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (StopWhenPause && Time.timeScale == 0)
            Source.Stop();
    }

    public void PlaySound(int id)
    {
        if (playRandom)
            id = Random.Range(0, Sound.Length);
        Source.PlayOneShot(Sound[id]);
    }

    public void PlayLoopSoundEventWhenNotPaused(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            EventSource.loop = false;
            EventSource.Stop();
            alredyPerfomed = false;
        }
        if (context.performed && !alredyPerfomed)
        {
            EventSource.loop = true;
            EventSource.Play();
            alredyPerfomed = true;
        }
    }
    public void PlayLoopSoundEventWNPWithBegin(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            EventSource.loop = false;
            EventSource.Stop();
            alredyPerfomed = false;
        }
        if (context.performed && !alredyPerfomed)
        {
            EventSource.Play();
            alredyPerfomed = true;
        }
        if (alredyPerfomed)
        {
            EventSource.loop = true;
            EventSource.Play();
        }
    }
    public void switchSound(int id)
    {
        AudioClip temp = EventSource.clip;
        EventSource.clip = Sound[id];
        Sound[id] = temp;
    }
}

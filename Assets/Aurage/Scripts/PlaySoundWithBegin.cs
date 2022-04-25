using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaySoundWithBegin : MonoBehaviour
{
    public AudioSource Source;

    public AudioClip Begin;
    public AudioClip Loop;

    public bool StopWhenPause;

    void Update()
    {
        if (StopWhenPause && Time.timeScale == 0)
            Source.Stop();
    }

    bool alredyPerfomed = false;
    public void Play(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Source.loop = false;
            Source.Stop();
            alredyPerfomed = false;
        }
        if (context.performed && !alredyPerfomed)
        {
            Source.PlayOneShot(Begin);
            alredyPerfomed = true;
            
        }

        if(Source.clip != Loop)
            Source.clip = Loop;

        if (!Source.isPlaying)
        {
            Source.loop = true;
            Source.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayLoopSound : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Loop;

    public bool StopWhenPause;

    void Update()
    {
        if (StopWhenPause && Time.timeScale == 0)
            Source.Stop();
    }

    public void StopSound()
    {
        Source.Stop();
    }

    bool alredyPerfomed = false;
    public void PlaySound(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Source.loop = false;
            Source.Stop();
            alredyPerfomed = false;
        }
        if (Source.clip != Loop)
            Source.clip = Loop;
        if (context.performed && !alredyPerfomed)
        {
            Source.loop = true;
            Source.Play();
            alredyPerfomed = true;
        }
    }
}

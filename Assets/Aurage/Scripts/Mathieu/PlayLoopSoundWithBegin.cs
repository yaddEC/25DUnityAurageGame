using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayLoopSoundWithBegin : MonoBehaviour
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

    public void StopSound()
    {
        Source.Stop();
        Source.clip = null;
        Source.Stop();
    }

    bool alredyPerfomed = false;
    public void PlaySound(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Source.loop = false;
            StopCoroutine(PlayTheSound());
            Source.Stop();
            alredyPerfomed = false;
        }
        if(Source.clip != Loop)
            Source.clip = Loop;
        if (context.performed && !alredyPerfomed)
        {
            Source.loop = true;
            StartCoroutine(PlayTheSound());
            alredyPerfomed = true;
            
        }
    }
    public IEnumerator PlayTheSound()
    {
        Source.PlayOneShot(Begin);
        yield return new WaitForSeconds(Begin.length-0.5f);
        Source.Play();
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWithBeginAndEnd : MonoBehaviour
{
    public AudioSource Source;

    public AudioClip Begin;
    public AudioClip Loop;
    public AudioClip End;

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
    public void PlaySound(bool context)
    {
        if (!context && alredyPerfomed)
        {
            Source.loop = false;
            StopCoroutine(PlayTheSound());
            Source.Stop();
            Source.PlayOneShot(End);
            alredyPerfomed = false;
        }
        if (Source.clip != Loop)
            Source.clip = Loop;
        if (context && !alredyPerfomed)
        {
            Source.loop = true;
            StartCoroutine(PlayTheSound());
            alredyPerfomed = true;

        }
    }
    public IEnumerator PlayTheSound()
    {
        Source.PlayOneShot(Begin);
        yield return new WaitForSeconds(Begin.length - 0.5f);
        Source.Play();
        yield return null;
    }
}

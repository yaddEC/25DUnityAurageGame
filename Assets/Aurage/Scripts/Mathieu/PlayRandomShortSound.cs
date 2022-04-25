using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomShortSound : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip[] Sound;

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

    public void PlaySound()
    {
        Source.PlayOneShot(Sound[Random.Range(0, Sound.Length)]);
    }
}

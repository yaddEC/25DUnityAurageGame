using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource Source;
    public bool playRandom;
    public AudioClip[] Sound;
    
    void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(int id)
    {
        if (playRandom)
            id = Random.Range(0,Sound.Length);
        Source.PlayOneShot(Sound[id]);
    }
}

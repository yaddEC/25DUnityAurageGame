using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    AudioSource Source;
    public bool playRandom;
    public bool isLooping;
    public AudioClip[] Sound;

    void Start()
    {
        Source = GetComponent<AudioSource>();
        if(isLooping)
            Source.loop = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision infoCollision)
    {
        if (infoCollision.gameObject.name == "Player")
        {
            int id = Random.Range(0, Sound.Length);
            Source.PlayOneShot(Sound[id]);
        }
    }
            
}

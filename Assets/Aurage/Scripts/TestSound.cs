using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    AudioSource Source;
    public AudioClip Sound;
    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision infoCollision)
    {
        if (infoCollision.gameObject.name == "Player")
            Source.PlayOneShot(Sound);
    }
}

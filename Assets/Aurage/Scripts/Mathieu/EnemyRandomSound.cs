using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSound : MonoBehaviour
{
    public PlayRandomShortSound Sound;

    private void Awake()
    {
        StartCoroutine(PlayTheSound());
    }

    public IEnumerator PlayTheSound()
    {
        while (true)
        {
            Sound.PlaySound();
            yield return new WaitForSeconds(Random.Range(10, 20));
        }
    }
}

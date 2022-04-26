using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector3 spawnPoint;

    private void Awake()
    {
        spawnPoint = UnityFinder.FindTransformInChildWithTag(gameObject, "LockPosition").position;
    }

    public void SpawnPlayer(Transform player)
    {
        player.position = spawnPoint;
    }
}

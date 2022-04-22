using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    public GameObject player;
    private LayerMask obstacle;
    float sightDistance;
    float sightAngle;
    // Start is called before the first frame update
    void Start()
    {
        obstacle = LayerMask.GetMask("Obstacle");
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool SeeThePlayer()//return true if the player is in enemy line of sight+no obstacle separating them
    {
        Vector3 playerVector = player.transform.position - transform.position;
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (!PowerManager.isInMachine)
        {
            if (playerDistance < sightDistance)
            {
                if (Vector3.Angle(transform.forward, playerVector) < sightAngle)
                {
                    if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, playerVector, playerDistance, obstacle))
                    {
                        return true;
                    }

                }
            }
        }
        return false;
    }

}

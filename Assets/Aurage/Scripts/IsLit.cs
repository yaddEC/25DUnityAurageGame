using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLit : MonoBehaviour
{
    public GameObject player;
    public float sightAngle;
    public float sightDistance;
    private LayerMask obstacle;
    public bool isLit;
    //public bool PlayerStateCheckIsVisible;// pour test, a enlever
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        obstacle = LayerMask.GetMask("Obstacle");
    }

    void Update()
    {
       /* if (PlayerState.isVisible)
            PlayerStateCheckIsVisible = true;
        else
            PlayerStateCheckIsVisible = false;*/

        if (SeeThePlayer())
        {
            if (!isLit)
            {
                isLit = true;
                PlayerState.isVisible = false;
            }

        }
        else
        {
            if (isLit)
            {
                isLit = false;
                PlayerState.isVisible = true;
            }

        }
        
    }

    public bool SeeThePlayer()//return true if the player is in enemy line of sight+no obstacle separating them
    {
        Vector3 playerVector = player.transform.position - transform.position;
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (!PlayerState.isInMachine)
        {
            if (playerDistance < sightDistance)
            {
                if (Vector3.Angle(-transform.up, playerVector) < sightAngle)
                {
                    if (!Physics.Raycast(transform.position, playerVector, playerDistance, obstacle))
                    {

                        return true;
                    }

                }
            }
        }
        return false;
    }
}

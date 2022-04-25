using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCamera : MonoBehaviour
{
    public GameObject player;
    public float sightAngle;
    public float sightDistance;
    private LayerMask obstacle;
    public bool seePlayer;
    public bool isSeeingPlayer;

    public Color cache;
    public float deathDelay;
    public Color newColor;
    Vector3 playerVector ;

    // Start is called before the first frame update
    void Start()
    {
        seePlayer = false;
        player = GameObject.FindGameObjectWithTag("Player");
        obstacle = LayerMask.GetMask("Obstacle");
        cache = transform.GetChild(0).GetComponent<Renderer>().material.color;
        newColor = cache;
      
    }

    // Update is called once per frame
    void Update()
    {

        playerVector = player.transform.position - transform.localPosition;
        if (SeeThePlayer() && PlayerState.isVisible)
        {
            Debug.DrawLine(player.transform.position, transform.position);
            isSeeingPlayer = true;
            if (!seePlayer)
            {
                StartCoroutine(playerDetection());
            }
        }
        else 
        {
            
            isSeeingPlayer = false;
            if (seePlayer)
            {
                seePlayer = false;
                StartCoroutine(unAlert());
            }
            

        }
            
       

    }
    private IEnumerator playerDetection()
    {
        seePlayer = true;
        while(isSeeingPlayer)
        {
            if(newColor.g<0.1)
                PowerManager.outOfPower = true;
            newColor.g-=0.01f;
            transform.GetChild(0).GetComponent<Renderer>().material.color = newColor;
            yield return new WaitForSeconds(deathDelay);
        }
        

    }

    private IEnumerator unAlert()
    {
        
        while (!isSeeingPlayer && newColor.g < 1)
        {
               newColor.g += 0.01f;
                transform.GetChild(0).GetComponent<Renderer>().material.color = newColor;
                yield return new WaitForSeconds(deathDelay);
            
        }


    }

    public bool SeeThePlayer()//return true if the player is in enemy line of sight+no obstacle separating them
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (!PlayerState.isInMachine)
        {
            if (playerDistance < sightDistance)
            {
                if (Vector3.Angle(transform.GetChild(1).forward, playerVector) < sightAngle)
                {
                    if (!Physics.Raycast(transform.position , playerVector, playerDistance, obstacle))
                    {
                        
                        return true;
                    }

                }
            }
        }
        return false;
    }

}

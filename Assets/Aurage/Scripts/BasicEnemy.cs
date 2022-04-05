using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool alerted;
    public bool playerDetected;
    public bool isMoving;
    public bool isTurning;
    public float sightDistance;
    public float rotation = 180;
    public float rotationSpeed = 100;
    public float rotationDuration = 3;
    public float speed;
    public float sightAngle;
    public float safeTimeAlert = 2;
    public float alertDuration = 30;
    public LayerMask obstacle;
    public LayerMask edge;
    public GameObject player;
    public Vector3 dir;
    Rigidbody rigidbody;
  
    void Start()
    {
        
        alerted = false;
        
        isMoving = true;
        dir = Vector3.forward;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    public bool SeeThePlayer()//return true if the player is in enemy line of sight+no obstacle separating them
    {
        Vector3 playerVector = player.transform.position - transform.position;
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (playerDistance < sightDistance)
        {
            if (Vector3.Angle(transform.forward, playerVector) < sightAngle)
            {
                if(!Physics.Raycast(transform.position+Vector3.up*0.5f,playerVector, playerDistance,obstacle))
                {
                    return true;
                }
                
            }
        }
        return false;
    }

    void Update()
    {
        //timed rotation when edge is encounter
        if (isEdge())
            StartCoroutine(ChangeDir());
        
        if (isTurning)
            Turning();

        //start the "alerted"/[did i see something?] state of the enemy if he saw the player and wasnt already alerted
        if (SeeThePlayer() && !alerted)
            StartCoroutine(Alerted());
        
        //State debug
        if (playerDetected)
            gameObject.GetComponent<Renderer>().material.color = Color.red;//death animation/coroutine + game over screen
        else if (alerted)
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.blue;

    }

    void FixedUpdate()
    {
        if (isMoving)
            Move();
       
        Debug.DrawRay(transform.position , dir * 10);
    }

    private bool isEdge()//bool function check if on Edge
    {
        return Physics.Raycast(transform.position , dir, 0.8f, edge);
    }

    private void Turning()//function that makes gradual rotation
    {
        float turning;
       
        if (dir.z <= 0)
            turning = rotation;
        else
            turning = 0;
        
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y,  turning, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, angle, 0);
    }

    private void Move()//moving/roaming function
    {
        float move = dir.z * Time.fixedDeltaTime;
        rigidbody.velocity = new Vector3(0, 0, 10*speed*move);
    }

    private IEnumerator ChangeDir()//Coroutine that change the direction/ stop the moving for the gradual rotation
    {
        isMoving = false;
        isTurning = true;
        dir *= -1;

        yield return new WaitForSeconds(rotationDuration);

        isMoving = true;
        isTurning = false;
    }

    private IEnumerator Alerted()
    {
        isMoving = false;
        alerted = true;

        yield return new WaitForSeconds(safeTimeAlert);//safetime where the player can hide/get out of enemy sight
  
        for(int i=0; i<alertDuration;i++)//check if player is still in enemy sight
        {
            if (SeeThePlayer())
            {
                playerDetected = true;
                break;
            }
            else if(i== alertDuration-1)
            {
                isMoving = true;
                alerted = false;
            }
                
            yield return new WaitForSeconds(0.1f);
        }
    }
}
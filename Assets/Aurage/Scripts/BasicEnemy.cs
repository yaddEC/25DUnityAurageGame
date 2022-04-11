using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Rigidbody rigidbody;
    public bool alerted;
    public bool playerDetected;
    public bool isMoving;
    public bool isTurning;
    public bool isStunned;
    public float turning;
    public bool isDistracted;
    public float sightDistance;
    public float rotation = 180;
    public float rotationSpeed = 100;
    public float speed;
    public float sightAngle;
    public float safeTimeAlert = 2;
    public float alertDuration = 30;
    private LayerMask obstacle;
    private LayerMask edge;
    private GameObject player;
    private GameObject machine;
    public Vector3 dir;
    [HideInInspector] public Vector3 moveDirection;
    private Coroutine lastRoutine;

    void Start()
    {
        edge = LayerMask.NameToLayer("Edge");
        obstacle = LayerMask.NameToLayer("Obstacle");
        player = GameObject.FindGameObjectWithTag("Player");
        alerted = false;
        isMoving = true;
        dir = Vector3.right;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        lastRoutine = null;
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
        if (isEdge() && !isDistracted)
            ChangeDir();
        
        //start the "alerted"/[did i see something?] state of the enemy if he saw the player and wasnt turning or already alerted
        if (SeeThePlayer() && !alerted && !isTurning && !isStunned && !isDistracted)
            StartCoroutine(Alerted());
        
        //State debug
        if (playerDetected)
            gameObject.GetComponent<Renderer>().material.color = Color.red;//ennemy attack/death animation/coroutine + game over screen
        else if(isStunned)
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        else if (isDistracted)
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        else if (alerted)
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.blue;

    }

    void FixedUpdate()
    {
      if (isMoving)
            Move();
    }


    private bool isEdge()//bool function check if on Edge
    {
        return Physics.Raycast(transform.position , dir, 1.5f, edge)|| Physics.Raycast(transform.position, dir, 0.8f, obstacle);
    }


    private IEnumerator Turning()//Coroutine that makes gradual rotation
    {
        
        isTurning = true;
        if (dir.x <= 0)
            turning = rotation;
        
        else
            turning = 270;
        
        
        while(transform.eulerAngles.y!=turning)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, turning, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, angle, 0);
            
            yield return new WaitForSeconds(0.001f);
        }

        if(!isStunned)
        {

            isTurning = false;
            while(alerted)
            {
                yield return new WaitForSeconds(0.1f);
            }
            isMoving = true;
        }
    }

    private void Move()//moving/roaming function
    {
        float move = dir.x * Time.fixedDeltaTime;
        rigidbody.velocity = new Vector3(10 * speed * move, 0, 0);
    }

    private void ChangeDir()//Coroutine that change the direction/ stop the moving for the gradual rotation
    {
        isMoving = false;
        dir *= -1;
        lastRoutine = StartCoroutine(Turning());
    }

    public void Stun(float stunDuration)
    {
        StopCoroutine(lastRoutine);
        StartCoroutine(Stunned(stunDuration));
    }

    public IEnumerator Stunned(float stunDuration)//Coroutine that stun the enemy, then unstun him
    {
        isMoving = false;
        isStunned = true;

        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
        if (isTurning)
            lastRoutine = StartCoroutine(Turning());
        else
            isMoving = true;
    }

    public void Distracted(GameObject machine)
    {
        isDistracted = true;
        this.machine = machine;
        moveDirection = (machine.transform.position - transform.position).normalized;
        if (Mathf.RoundToInt(moveDirection.x) != dir.x)
            ChangeDir();
    }

    public void Focused()
    {
        isDistracted = false;
    }

    private IEnumerator Alerted()
    {
        isMoving = false;
        alerted = true;

        yield return new WaitForSeconds(safeTimeAlert);//safetime where the player can hide/get out of enemy sight
  
        for(int i=0; i<alertDuration;i++)//check if player is still in enemy sight
        {
            if (SeeThePlayer() && !isStunned)
            {
                playerDetected = true;
                SceneManager.LoadScene("GameOverScreen");
                //future coroutine that loads enemy shooting animation
                break;
            }
            else if(isStunned || isDistracted)
            {
                alerted = false;
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOverScreen");
        }



    }
}
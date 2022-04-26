using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    // Start is called before the first frame update

    private Rigidbody rb;
    private LayerMask obstacle;
    private LayerMask ground;
    private GameObject machine;
    private Coroutine lastRoutine;
    private Material enemyHead;
    private Animator animator;
    private float gravity;
    public bool alerted;
    public bool playerDetected;
    public bool isMoving;
    public bool isTurning;
    public bool isDistracted;
    public bool isGrounded;
    public float sightDistance;
    public float rotationSpeed = 100;
    public float speed;
    public float sightAngle;
    public float safeTimeAlert = 2;
    public float alertDuration = 30;
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject nextWayPoint;
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public List<GameObject> wayPoints;

    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        GetWayPoints();
        nextWayPoint = wayPoints[0];
        obstacle = LayerMask.GetMask("Obstacle");
        ground = LayerMask.GetMask("Floor");
        player = GameObject.FindGameObjectWithTag("Player");
        dir = (nextWayPoint.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
        alerted = false;
        isMoving = true;
        rb = gameObject.GetComponent<Rigidbody>();
        lastRoutine = null;
        enemyHead = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>().material;
    }

    private void GetWayPoints()
    {
        GameObject[] gameWayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        for (int i = 0; i < gameWayPoints.Length; i++)
        {
            if (gameWayPoints[i].transform.parent.parent == transform.parent)
            {
                wayPoints.Add(gameWayPoints[i]);
            }
        }


    }

    public bool SeeThePlayer()//return true if the player is in enemy line of sight+no obstacle separating them
    {
        Vector3 playerVector = player.transform.position - transform.position;
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (!PlayerState.isInMachine && PlayerState.isVisible && !PlayerState.isInNodePath)
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

    void Update()
    {
        DirAndGravity();
        StateColor();

        if (SeeThePlayer() && !alerted && !isTurning && !isStunned && !isDistracted)
            StartCoroutine(Alerted());
    }

    private void StateColor()
    {
        if (playerDetected)
            enemyHead.SetColor("_EmissionColor", Color.red);
        else if (isStunned)
            enemyHead.SetColor("_EmissionColor", Color.black);
        else if (isDistracted)
            enemyHead.SetColor("_EmissionColor", Color.blue);
        else if (alerted)
            enemyHead.SetColor("_EmissionColor", Color.yellow);
        else
            enemyHead.SetColor("_EmissionColor", Color.cyan);
    }

    private void DirAndGravity()
    {
        dir = (nextWayPoint.transform.position - transform.position).normalized;
        isGrounded = Physics.CheckSphere(transform.position, 0.5f, ground);

        if (isGrounded && gravity < 0)
            gravity = 0f;

        gravity += Physics.gravity.y * Time.deltaTime;

        if(transform.position.y<-1000)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    void FixedUpdate()
    {
        if (isMoving)
            Move();
    }

    private IEnumerator Turning()//Coroutine that makes gradual rotation
    {

        isTurning = true;
        animator.SetBool("isTurning", true);
        yield return new WaitForSeconds(0.1f);
        Quaternion look = Quaternion.LookRotation(dir);
        float time = 0f;
        while (time < 1)
        {
            Quaternion temp = Quaternion.Lerp(transform.rotation, look, time / 10);
            transform.rotation = new Quaternion(0, temp.y, 0, temp.w);
            time += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        if (!isStunned)
        {

            isTurning = false;
            animator.SetBool("isTurning", false);
            while (alerted)
            {
                yield return new WaitForSeconds(0.1f);
            }
            isMoving = true;
        }
    }

    private void Move()//moving/roaming function
    {
        float movex = dir.x * Time.fixedDeltaTime;
        float movez = dir.z * Time.fixedDeltaTime;
        rb.velocity = new Vector3(10 * speed * movex, gravity, 10 * speed * movez);
    }

    public void ChangeWayPoint(GameObject actualWayPoint)//Coroutine that change the direction/ stop the moving for the gradual rotation
    {
         
        isMoving = false;
        for (int i = 0; i < wayPoints.Count; i++)
        {

            if (wayPoints[i] == actualWayPoint)
            {
                if (i + 1 == wayPoints.Count)
                    nextWayPoint = wayPoints[0];
                else
                    nextWayPoint = wayPoints[i + 1];

            }
        }
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
        animator.SetBool("isStun", true);   
        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
        animator.SetBool("isStun", false);
        lastRoutine = StartCoroutine(Turning());
    }


    public void Distracted(GameObject machine)
    {
        isMoving = false;
        lastRoutine = StartCoroutine(Turning());
        Debug.Log("dist");
        this.machine = nextWayPoint;
        nextWayPoint = machine;
        isDistracted = true;
    }

    public void Halt()
    {
        isMoving = false;
        rb.velocity = Vector3.zero;
    }

    public void Focused()
    {
        Debug.Log("Focused");
        isMoving = false;
        nextWayPoint = machine;
        lastRoutine = StartCoroutine(Turning());
        isDistracted = false;
    }

    private IEnumerator Alerted()
    {
        isMoving = false;
        alerted = true;
        animator.SetBool("isAlerted", true);
        yield return new WaitForSeconds(safeTimeAlert);//safetime where the player can hide/get out of enemy sight

        for (int i = 0; i < alertDuration; i++)//check if player is still in enemy sight
        {
            if (SeeThePlayer() && !isStunned)
            {
                playerDetected = true;
                animator.SetBool("playerDetected", true);
                yield return new WaitForSeconds(0.8f);
                PowerManager.outOfPower = true;
                break;
            }
            else if (isStunned || isDistracted)
            {
                alerted = false;
                animator.SetBool("isAlerted", false);
                break;
            }
            else if (i == alertDuration - 1)
            {
                isMoving = true;
                alerted = false;
                animator.SetBool("isAlerted", false);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isStunned && !PlayerState.isInMachine && !PlayerState.isInNodePath)
            PowerManager.outOfPower = true;
    }
}
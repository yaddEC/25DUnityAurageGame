using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    public bool alerted;
    public bool playerDetected;
    public bool isMoving;
    public bool isTurning;
    public bool isStunned;
    public bool isDistracted;
    public float sightDistance;
    public float rotation = 90;
    public float rotationSpeed = 100;
    public float speed;
    public float sightAngle;
    public float safeTimeAlert = 2;
    public float alertDuration = 30;
    private LayerMask obstacle;
    private LayerMask edge;
    public GameObject player;
    public GameObject nextWayPoint;
    private GameObject machine;
    public Vector3 dir;
    [HideInInspector] public Vector3 moveDirection;
    private Coroutine lastRoutine;
    public Material enemyHead;
    public List<GameObject> wayPoints;
    public float turning = 0f;

    void Start()
    {
        getWayPoints();
        nextWayPoint = wayPoints[0];
        edge = LayerMask.GetMask("Edge");
        obstacle = LayerMask.GetMask("Obstacle");
        player = GameObject.FindGameObjectWithTag("Player");

        dir = (nextWayPoint.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
        turning = Vector3.Angle(transform.forward, dir);
        alerted = false;
        isMoving = true;

        rb = gameObject.GetComponent<Rigidbody>();
        lastRoutine = null;
        // enemyHead = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>().material;
    }

    private void getWayPoints()
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

    void Update()
    {
        //timed rotation when edge is encounter
       
            dir = (nextWayPoint.transform.position - transform.position).normalized;

        //start the "alerted"/[did i see something?] state of the enemy if he saw the player and wasnt turning or already alerted
        if (SeeThePlayer() && !alerted && !isTurning && !isStunned && !isDistracted)
            StartCoroutine(Alerted());

        //State debug
        /* if (playerDetected)
             enemyHead.SetColor("_EmissionColor", Color.red);
         else if (isStunned)
             enemyHead.SetColor("_EmissionColor", Color.black);
         else if (isDistracted)
             enemyHead.SetColor("_EmissionColor", Color.blue);
         else if (alerted)
             enemyHead.SetColor("_EmissionColor", Color.yellow);
         else
             enemyHead.SetColor("_EmissionColor", Color.cyan);*/

    }

    void FixedUpdate()
    {
        if (isMoving)
            Move();
    }


    private bool isEdge()//bool function check if on Edge
    {
        return Physics.Raycast(transform.position, dir, 1.5f, edge) || Physics.Raycast(transform.position, dir, 0.8f, obstacle);
    }


    private IEnumerator Turning()//Coroutine that makes gradual rotation
    {

        isTurning = true;
        yield return new WaitForSeconds(0.1f);
        Quaternion look = Quaternion.LookRotation(dir);
        float time = 0f;
        while (time < 1)
        {
            Quaternion temp = Quaternion.Lerp(transform.rotation, look, time / 20);
            transform.rotation = new Quaternion(0, temp.y, 0, temp.w);
            time += Time.deltaTime * rotationSpeed;
            turning = time;
            yield return null;
        }

        if (!isStunned)
        {

            isTurning = false;
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
        rb.velocity = new Vector3(10 * speed * movex, 0, 10 * speed * movez);
    }

    public void ChangeWayPoint(GameObject actualWayPoint)//Coroutine that change the direction/ stop the moving for the gradual rotation
    {
        if (!isDistracted) { 
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
        nextWayPoint = machine;
        isDistracted = false;
        lastRoutine = StartCoroutine(Turning());
    }

    private IEnumerator Alerted()
    {
        isMoving = false;
        alerted = true;

        yield return new WaitForSeconds(safeTimeAlert);//safetime where the player can hide/get out of enemy sight

        for (int i = 0; i < alertDuration; i++)//check if player is still in enemy sight
        {
            if (SeeThePlayer() && !isStunned)
            {
                playerDetected = true;
                //SceneManager.LoadScene("GameOverScreen");
                //future coroutine that loads enemy shooting animation
                break;
            }
            else if (isStunned || isDistracted)
            {
                alerted = false;
                break;
            }
            else if (i == alertDuration - 1)
            {
                isMoving = true;
                alerted = false;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isStunned && !PowerManager.isInMachine)
            SceneManager.LoadScene("GameOverScreen");
    }
}
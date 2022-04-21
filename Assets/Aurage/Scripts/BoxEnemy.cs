using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    
    public bool playerDetected;
    public bool isMoving;
    public bool isTurning;
    public bool isStunned;
    public float sightDistance;
    public float rotation = 90;
    public float rotationSpeed = 100;
    public float speed;
    public float sightAngle;
    public float alertDuration = 30;
    private LayerMask obstacle;
    public GameObject player;
    public GameObject nextWayPoint;
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
        obstacle = LayerMask.GetMask("Obstacle");
        player = GameObject.FindGameObjectWithTag("Player");
        dir = (nextWayPoint.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
        turning = Vector3.Angle(transform.forward, dir);
        isMoving = true;
        rb = gameObject.GetComponent<Rigidbody>();
        lastRoutine = null;
    }

    private void getWayPoints()
    {
        GameObject[] gameWayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        for (int i = 0; i < gameWayPoints.Length; i++)
        {
            if (gameWayPoints[i].transform.parent.parent == transform.parent)
                wayPoints.Add(gameWayPoints[i]);
        }
    }
    void Update()
    {
        dir = (nextWayPoint.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (isMoving)
            Move();
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
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        lastRoutine = StartCoroutine(Turning());
    }
}

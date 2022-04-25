using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;


    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isTurning;
    [HideInInspector] public bool isStunned;
    [HideInInspector] public bool isSleep;
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject nextWayPoint;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public List<GameObject> wayPoints;
    public float rotation;
    public float rotationSpeed = 1;
    public float speed;
    public Vector3 dir;
    private Coroutine lastRoutine;

    void Start()
    {
        getWayPoints();
        nextWayPoint = wayPoints[0];
        player = GameObject.FindGameObjectWithTag("Player");
        dir = (nextWayPoint.transform.position - transform.position).normalized;
        Quaternion temp = Quaternion.LookRotation(dir); 
        transform.rotation = new Quaternion(0, temp.y, 0, temp.w); 
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
            yield return null;
        }

        if (!isStunned|| !isSleep)
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
        if (!isStunned)
        {
            StopCoroutine(lastRoutine);
            StartCoroutine(Stunned(stunDuration));
        }

    }

    public void Guard(float guardDuration, float direction)
    {
        rb.velocity = Vector3.zero;
        StopCoroutine(lastRoutine);
        StartCoroutine(Guarded(guardDuration, direction));


    }

    public IEnumerator Stunned(float stunDuration)//Coroutine that stun the enemy, then unstun him
    {
        isMoving = false;
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        lastRoutine = StartCoroutine(Turning());
    }

    public IEnumerator Guarded(float stunDuration, float direction)//Coroutine that stun the enemy, then unstun him
    {
        
        isMoving = false;
        isSleep = true;
        Quaternion look = Quaternion.Euler(0,direction,0);
        float time = 0f;
        while (time < 1)
        {
            Quaternion temp = Quaternion.Lerp(transform.rotation, look, time / 20);
            transform.rotation = new Quaternion(0, temp.y, 0, temp.w);
            time += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(stunDuration);
        isSleep = false;
        lastRoutine = StartCoroutine(Turning());
    }
}

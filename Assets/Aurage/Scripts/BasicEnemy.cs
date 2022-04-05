using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool alerted;
    public bool edgeI;
    public bool isMoving;
    public bool isTurning;
    public float sightDistance;
    public float rotation = 180;
    public float rotationSpeed = 100;
    public float rotationDuration = 3;
    public float speed;
    public float sightAngle;
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

    public bool IsAlerted()
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
        if (isEdge() )
            StartCoroutine(ChangeDir());
        if (isTurning)
            Turning();
        
           

        if (IsAlerted())
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.blue;

    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position , dir * 10);
        if (isMoving)
            Move();
    }

    private bool isEdge()
    {
        return edgeI= Physics.Raycast(transform.position , dir, 0.8f, edge);
    }

    private void Turning()
    {
        float turning;
        if (dir.z <= 0)
            turning = rotation;
        else
            turning = 0;

        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y,  turning, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, angle, 0);
    }

    private void Move()
    {
        float move = dir.z * Time.fixedDeltaTime;
        rigidbody.velocity = new Vector3(0, 0, 10*speed*move);
    }
    private IEnumerator ChangeDir()
    {
        isMoving = false;
        isTurning = true;
        dir *= -1;
        yield return new WaitForSeconds(rotationDuration);
        isMoving = true;
        isTurning = false;

    }
}
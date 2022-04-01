using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    bool isEdge = false;
    bool isTurning = false;
    Vector3 dir = Vector3.forward;
    Rigidbody rigidbody;
    LayerMask edge;
    void Start()
    {
        edge = 6;
    }

    // Update is called once per frame

    void Update()
    {
        DirectionGetter();

    }

    void FixedUpdate()
    {
        Move();

    }

    private void DirectionGetter()
    {
        isEdge = Physics.Raycast(transform.position + dir * 0.8f, dir, 0.2f, edge);

    }

private void Move()
    {
        rigidbody.velocity = new Vector3(0,0,0);
    }
}

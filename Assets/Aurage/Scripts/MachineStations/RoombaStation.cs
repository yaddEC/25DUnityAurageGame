using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaStation : Station
{
    [Header("Property Info")]
    public float moveSpeed;
    private Rigidbody rb;
    //-------------------------------------------------------------
    private void Awake() { 
        var t = gameObject.name; tagToSearch = t;
        rb = GetComponent<Rigidbody>();
    }
    private void Start() { RegisterReferences(); }
    private void Update()
    {
        CooldownHandler(true);
        ClampInMachine();
        FreezeRoomba();
    }
    private void FixedUpdate()
    {
        if(doEvent) Move(InputManager.inputAxis);
    }
    //-------------------------------------------------------------
    private void Move(Vector2 input)
    {
        rb.velocity = new Vector3(input.x * moveSpeed, rb.velocity.y, input.y * moveSpeed) * Time.fixedDeltaTime;
    }
    private void FreezeRoomba()
    {
        if (!isInMachine) rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        else rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    //-------------------------------------------------------------
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0)
        {
            EnterMachine();
            StayMachine(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0) ExitMachine();
    }
}

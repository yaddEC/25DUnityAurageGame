using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaStation : Station
{
    [Header("Property Info")]
    public float moveSpeed;
    private Rigidbody rb;
    private Transform roomba;
    //-------------------------------------------------------------
    private void Awake() { 
        var t = gameObject.name; tagToSearch = t;
        rb = GetComponentInChildren<Rigidbody>();
    }
    private void Start() { RegisterReferences(); }
    private void Update()
    {
        CooldownHandler(true);
        FreezeRoomba();
    }
    private void FixedUpdate()
    {
        if(doEvent) MoveRoomba(InputManager.inputAxis);
    }
    //-------------------------------------------------------------
    private void MoveRoomba(Vector2 input)
    {
        rb.velocity = new Vector3(input.x * moveSpeed, rb.velocity.y, input.y * moveSpeed) * Time.fixedDeltaTime;
    }
    private void FreezeRoomba()
    {
        if (!isInMachine) rb.constraints = RigidbodyConstraints.FreezeAll;
        else rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
    //-------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0) EnterMachine(this.GetComponent<RoombaStation>());
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0) { StayMachine(true); }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0 && isInMachine) ExitMachine();
    }
}

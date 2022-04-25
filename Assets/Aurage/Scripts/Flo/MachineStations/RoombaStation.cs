using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaStation : Station
{
    [Header("Property Info")]
    public float moveSpeed;
    private Rigidbody roombaRb;
    private Transform roomba;
    //-------------------------------------------------------------
    private void Awake() { 
        var t = gameObject.name; tagToSearch = t;
        roombaRb = GetComponentInChildren<Rigidbody>();
    }
    private void Start() { RegisterReferences(); }
    private void Update()
    {
        CooldownHandler(special = true);
        FreezeRoomba();
    }
    private void FixedUpdate()
    {
        MoveRoomba(InputManager.inputAxis);
    }
    //-------------------------------------------------------------
    private void MoveRoomba(Vector2 input)
    {
        if(doEvent) roombaRb.velocity = new Vector3(input.x * moveSpeed, roombaRb.velocity.y, input.y * moveSpeed) * Time.fixedDeltaTime;
    }
    private void FreezeRoomba()
    {
        if (!PlayerState.isInMachine) roombaRb.constraints = RigidbodyConstraints.FreezeAll;
        else roombaRb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    //-------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0) EnterMachine(this.GetComponent<RoombaStation>());
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0) { StayMachine(autoExec = true); }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0) ExitMachine();
    }
}

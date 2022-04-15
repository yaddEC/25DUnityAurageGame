using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaStation : MonoBehaviour
{
    private PlayerMotion refPlayerMotion;
    public bool isFreezed = false;
    public bool isInMachine = false;
    public float moveSpeed;

    public float cooldown;
    private float cachedcooldown;

    private bool canMove = false;

    private Rigidbody rb;
    private MeshRenderer mr;

    private void Awake()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();

        rb = GetComponent<Rigidbody>();
        mr = refPlayerMotion.GetComponent<MeshRenderer>();

        cachedcooldown = cooldown;
    }

    private void Update()
    {
        ClampInMachine();

        if (cooldown > 0) cooldown -= Time.deltaTime;
        if(!isInMachine) rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    private void FixedUpdate()
    {
        if(canMove) Move(InputManager.inputAxis);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            PowerManager.isInMachine = true;
            mr.enabled = false;
            canMove = true;
            isInMachine = true;
            isFreezed = true;

            if (InputManager.performA) isFreezed = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0 && isInMachine)
        {
            PowerManager.isInMachine = false;
            mr.enabled = true;
            canMove = false;
            isInMachine = false;

            cooldown = cachedcooldown;
            Debug.Log("exit");
        }
    }

    private void Move(Vector2 input)
    {
        rb.velocity = new Vector3(input.x * moveSpeed, rb.velocity.y, input.y * moveSpeed) * Time.fixedDeltaTime;
    }

    private void ClampInMachine()
    {
        if (isFreezed)
        {
            refPlayerMotion.transform.position = transform.position;
            refPlayerMotion.rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
            refPlayerMotion.rb.constraints = RigidbodyConstraints.None;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaStation : MonoBehaviour
{
    private PlayerMotion refPlayerMotion;
    public bool isFreezed = false;
    public bool isInMachine = false;
    public float moveSpeed;

    private bool canMove = false;

    private Rigidbody rb;

    private void Awake()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ClampInMachine();
    }

    private void FixedUpdate()
    {
        if(canMove)
            Move(InputManager.inputAxis);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PowerManager.canLoosePower = false;
            canMove = true;
            isInMachine = true;
            isFreezed = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canMove = true;
            if (InputManager.performA) isFreezed = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PowerManager.canLoosePower = true;
            canMove = false;
            isInMachine = false;
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

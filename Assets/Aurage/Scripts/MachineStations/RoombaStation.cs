using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaStation : MonoBehaviour
{
    private PlayerMotion refPlayerMotion;
    public GameObject Roomba;
    public bool isFreezed = false;
    public bool isInMachine = false;
    public float moveSpeed;

    private Rigidbody rb;

    private void Awake()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();

        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PowerManager.canLoosePower = false;
            isInMachine = true;
            ClampInMachine();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Move(InputManager.inputAxis);

            if (InputManager.performA) isFreezed = false;
        }
    }

    private void Move(Vector2 input)
    {
        rb.velocity = new Vector3(input.x * moveSpeed, rb.velocity.y, input.y * moveSpeed) * Time.fixedDeltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PowerManager.canLoosePower = true;
            isInMachine = false;
        }
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

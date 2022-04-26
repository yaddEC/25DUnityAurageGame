using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script and Gameplay discontinued
public class ElectricityBullet : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 moveDirection;
    public float bulletSpeed = 5;
    public float stunDuration = 5;
    public float radioDuration = 10;
    public float maxDistance = 2;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveDirection = moveDirection.normalized;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z) * bulletSpeed;
        Destroy(gameObject, maxDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BasicEnemy")
        {
            other.gameObject.GetComponent<BasicEnemy>().Stun(stunDuration);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Turret"))
        {
            other.gameObject.transform.GetChild(0).GetComponent<Laser>().Stun(stunDuration);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Radio"))
        {
            other.gameObject.transform.GetComponent<RadioStation>().TurnRadioOn();
            Destroy(gameObject);
        }

       
    }
}

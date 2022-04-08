using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectricityBullet : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 moveDirection;
    public float bulletSpeed = 5;
    public float stunDuration = 5;
    public float radioDuration = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDirection = moveDirection.normalized;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z) * bulletSpeed;
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BasicEnemy"))
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
            other.gameObject.transform.GetComponent<RadioBehavior>().StartRadio();
            Destroy(gameObject);
        }

       
    }
}

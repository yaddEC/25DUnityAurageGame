using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public Vector3 moveDirection;
    public float bulletSpeed=5;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = moveDirection.normalized;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z)*bulletSpeed;
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}

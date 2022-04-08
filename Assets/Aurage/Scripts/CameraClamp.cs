using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    private GameObject obj;
    public Vector3 positionOffset;
    public Vector3 orientationOffset;
    public float smoothTime;
    private Vector3 velocity;

    private void Awake()
    {
        obj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, obj.transform.position + positionOffset, ref velocity, smoothTime);
        transform.rotation = Quaternion.Euler(orientationOffset.x, orientationOffset.y, orientationOffset.z);
    }
}


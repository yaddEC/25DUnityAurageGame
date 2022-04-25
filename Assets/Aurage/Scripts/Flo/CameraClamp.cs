using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    private static GameObject obj;

    public Vector3 posOffset;
    public Vector3 orientationOffset;
    public float smoothTime;

    private float zPos;
    public float zOffset;

    private void Awake()    
    {
        obj = GameObject.FindGameObjectWithTag("Player");
        ChangeZPos(obj.transform.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(obj.transform.position.x + posOffset.x,
                                                                          obj.transform.position.y + posOffset.y,
                                                                                                          zPos ), 
                                                                                               smoothTime / 100);

        transform.rotation = Quaternion.Euler(orientationOffset.x, orientationOffset.y, orientationOffset.z);
    }

    public void ChangeZPos(float z)
    {
        zPos = z + zOffset;
    }
}


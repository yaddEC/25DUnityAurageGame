using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer laser;
    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit))
        {
            if(hit.collider)
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                    Destroy(hit.transform.gameObject);
                else
                    laser.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
            else 
            {
                laser.SetPosition(1, new Vector3(0, 0, 3000));
            }
        }
    }
}

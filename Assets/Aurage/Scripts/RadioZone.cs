using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioZone : MonoBehaviour
{
   [HideInInspector] public bool wasDeactivate = false;
   [HideInInspector] public bool isDeactivate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BasicEnemy"))
            other.gameObject.GetComponent<BasicEnemy>().Distracted(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BasicEnemy") && isDeactivate && wasDeactivate)
            other.gameObject.GetComponent<BasicEnemy>().Focused();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioZone : MonoBehaviour
{
    private BasicEnemy refBasicEnemy;
    [HideInInspector] public bool isActive = false;

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BasicEnemy")
        {
            refBasicEnemy = other.gameObject.GetComponent<BasicEnemy>();
            refBasicEnemy.Distracted(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BasicEnemy" && isActive)
            refBasicEnemy.Focused();
    }
}
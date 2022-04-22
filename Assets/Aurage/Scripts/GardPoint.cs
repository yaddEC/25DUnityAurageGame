using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardPoint : MonoBehaviour
{
    private BoxEnemy refBoxEnemy;
    public float timeToWait = 5;
    public float directionToSee;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BoxEnemy" && transform.parent.parent == other.transform.parent)
        {
            refBoxEnemy = other.gameObject.GetComponent<BoxEnemy>();
            refBoxEnemy.ChangeWayPoint(this.gameObject);
            refBoxEnemy.Guard(timeToWait,directionToSee);
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private BasicEnemy  refBasicEnemy;
    private BoxEnemy refBoxEnemy;
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
        if (other.gameObject.tag == "BasicEnemy" && other.GetComponent<BasicEnemy>().nextWayPoint==this.gameObject)
        {
            refBasicEnemy = other.gameObject.GetComponent<BasicEnemy>();
            refBasicEnemy.ChangeWayPoint(this.gameObject);
        }

        if (other.gameObject.tag == "BoxEnemy" && other.gameObject.GetComponent<BoxEnemy>().nextWayPoint == this.gameObject)
        {
            refBoxEnemy = other.gameObject.GetComponent<BoxEnemy>();
            refBoxEnemy.ChangeWayPoint(this.gameObject);
        }
    }

}

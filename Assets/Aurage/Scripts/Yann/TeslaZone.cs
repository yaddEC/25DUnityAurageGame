using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaZone : Enemy
{
    private float cache;
    private float beginLine;
    private float endLine;
    private float beginChange;
    private float endChange;
    private LineRenderer laser;
    public float drainingPower;
    public Vector3 playerPos;
    public Vector3 linePos;
    // Start is called before the first frame update

    public bool isIn;

    private void Start()
    {
        beginLine = 0;
        endLine = 0.1f;
        beginChange = 0.01f;
        endChange = 0.01f;
        laser = transform.GetChild(0).GetComponent<LineRenderer>();
    }

    public void Stun(float stunDuration)
    {
        StartCoroutine(Stunned(stunDuration));
    }

    public IEnumerator Stunned(float stunDuration)//Coroutine that stun the enemy, then unstun him
    {
        isStunned = true;
        PowerManager.unchargeCache = cache;

        yield return new WaitForSeconds(stunDuration);

        PowerManager.unchargeCache = drainingPower;
        isStunned = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cache = PowerManager.unchargeCache;
            PowerManager.unchargeCache = drainingPower;

            isIn = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (isStunned || PlayerState.isInMachine || PlayerState.isInNodePath)
        {
            laser.SetPosition(1, Vector3.zero);
            laser.SetWidth(0, 0);
            isIn = false;

        }
        else if (other.gameObject.tag == "Player" && !PlayerState.isInMachine && !PlayerState.isInNodePath)
        {
            linePos = other.transform.position - this.transform.GetChild(0).position;

            if (beginLine < 0 || beginLine>0.1f)
            {
                beginChange *= -1;
            }

            if (endLine < 0 || endLine > 0.1f)
            {
                endChange *= -1;
            }

            beginLine += beginChange;
            endLine += endChange;
            
            laser.SetPosition(1, linePos);
            laser.SetWidth(beginLine, endLine);


        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PowerManager.unchargeCache = cache;
            laser.SetPosition(1, Vector3.zero);
            laser.SetWidth(0, 0);

            isIn = false;
        }
    }

}

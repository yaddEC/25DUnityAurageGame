using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketStation : MonoBehaviour
{
    [Header("Reference")]
    private PlayerMotion refPlayerMotion;

    public GameObject socketTarget;
    public static float coolDown;
    private static float cachedCoolDown;

    private void Awake()
    {
        coolDown = 2;
        cachedCoolDown = coolDown;

        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
    }
    private void Update()
    {
        coolDown -= Time.deltaTime;
    }

    private void TeleportToTarget()
    {
        coolDown = cachedCoolDown;
        refPlayerMotion.rb.velocity = Vector3.zero;

        if (socketTarget.name == "In")
        {
            refPlayerMotion.isInPath = false;
            refPlayerMotion.transform.position = socketTarget.transform.position;
        }    
        else
            refPlayerMotion.transform.position = socketTarget.transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && coolDown <= 0)
            TeleportToTarget();
    }
}

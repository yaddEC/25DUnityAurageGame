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

    private CameraClamp refCamerClamp;

    private void Awake()
    {
        coolDown = 2;
        cachedCoolDown = coolDown;

        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
        refCamerClamp = GameObject.FindObjectOfType<CameraClamp>();
    }
    private void Update()
    {
        coolDown -= Time.deltaTime;
    }

    private void TeleportToTarget()
    {
        coolDown = cachedCoolDown;
        refPlayerMotion.rb.velocity = Vector3.zero;
        refPlayerMotion.transform.position = socketTarget.transform.position;
        refCamerClamp.changeZPos(refPlayerMotion.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && coolDown <= 0)
            TeleportToTarget();
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            refPlayerMotion.isInPath = false;
            refPlayerMotion.isGrounded = false;
            Debug.Log("Here");
        }

    }
}

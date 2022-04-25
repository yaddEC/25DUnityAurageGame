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
        refPlayerMotion.transform.position = socketTarget.transform.position;
        refCamerClamp.ChangeZPos(refPlayerMotion.transform.position.z);

        coolDown = cachedCoolDown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && coolDown <= 0) TeleportToTarget();
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerState.isInNodePath = false;
            PlayerState.isGrounded = false;
        }

    }
}

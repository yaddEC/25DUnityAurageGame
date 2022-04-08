using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    public ColliderDetection[] refColliderDetection;
    public Socket refSocket;
    private int index;
    private bool inCable = false;

    [Header("Lamp UI/UX")]
    private MeshRenderer meshRenderer;

    [Header("Lamp Stats")]
    public bool isInSocket = false;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refColliderDetection = GetComponentsInChildren<ColliderDetection>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = refSocket.machineMaterials[1];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !inCable)
            ClampInCable(other, index = 1);
        else if(other.CompareTag("Player") && !inCable)
            ClampInCable(other, index = 0);
    }

    private void ClampInCable(Collider other, int index)
    {
        other.transform.position = refColliderDetection[index].transform.position;
        delayTeleport();
    }

    private IEnumerator delayTeleport()
    {
        inCable = true;
        yield return new WaitForSeconds(2f);
        inCable = false;
    }
}

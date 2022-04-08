using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    public ColliderDetection[] refColliderDetection;
    private NodeSettings refNodeSettings;
    public Socket refSocket;
    private int index;
    public bool inSocket = false;



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
        if (other.CompareTag("Player"))
        {
            if(refColliderDetection[0].inObject)
                StartCoroutine(ClampInCable(other, index = 1));
            else
                StartCoroutine(ClampInCable(other, index = 0));
        }
    }

    private IEnumerator ClampInCable(Collider other, int index)
    {
        inSocket = true;
        other.transform.position = refColliderDetection[index].transform.position;

        yield return new WaitForSeconds(0.2f);
        inSocket = false;
    }
}

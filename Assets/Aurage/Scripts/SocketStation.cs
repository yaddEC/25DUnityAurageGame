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
    public static int index;
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

    private void Update()
    {
        if (ColliderDetection.inObject)
            ClampInCable(ColliderDetection.coll, index);
    }

    private IEnumerator ClampInCable(Collider other, int index)
    {
        inSocket = true;
        other.transform.position = refColliderDetection[index].transform.position;

        yield return new WaitForSeconds(0.3f);
        inSocket = false;
    }
}

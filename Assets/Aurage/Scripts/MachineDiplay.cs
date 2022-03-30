using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDiplay : MonoBehaviour
{
    public InteractManager refInteractManager;

    private Transform refTransform;

    void OnDrawGizmos()
    {
        refTransform = refInteractManager.machineObject.transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawMesh(refTransform.GetComponent<MeshFilter>().sharedMesh, new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }

    private void Awake()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        var meshRenderer = GetComponent<MeshFilter>();
        meshRenderer.sharedMesh = refTransform.GetComponent<MeshFilter>().sharedMesh;
    }
}
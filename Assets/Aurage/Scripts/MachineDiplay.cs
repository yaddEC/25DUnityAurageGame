using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDiplay : MonoBehaviour
{
    public InteractManager refInteractManager;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawMesh(refInteractManager.machineObject.transform.GetComponent<MeshFilter>().sharedMesh, new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }

    private void Awake()
    {
        BoxCollider box =  gameObject.AddComponent<BoxCollider>();
        box.isTrigger = true;

        MeshFilter mesh = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        for (int i = 0; i < refInteractManager.machineMaterials.Length; i++)
        {
            meshRenderer.materials[i] = refInteractManager.machineMaterials[i];
        }

        mesh.sharedMesh = refInteractManager.machineObject.transform.GetComponent<MeshFilter>().sharedMesh;
    }
}
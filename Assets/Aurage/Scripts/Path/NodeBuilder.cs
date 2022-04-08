using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuilder : MonoBehaviour
{
    [SerializeField] private GameObject nodeHolder;
    [SerializeField] private GameObject containerHolder;
    [SerializeField] private Transform container;

    [SerializeField] private bool containerHolderCreated;

    [SerializeField] private List<GameObject> nodeHolders = new List<GameObject>();

    private void CreateNodeHolder()
    {
        nodeHolder = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity, container);
        nodeHolder.AddComponent<NodeReference>();

        nodeHolder.name = "NodeHolder";
        nodeHolder.tag = "NodeHolder";
        nodeHolders.Add(nodeHolder);
    }

    private void CreateNodeContainer()
    {
        containerHolder = new GameObject();
        containerHolder.name = "NodeContainer";
        containerHolder.tag = "NodeContainer";

        Instantiate(containerHolder, Vector3.zero, Quaternion.identity, transform);

        container = containerHolder.transform;
        containerHolderCreated = true;
    }

    public void BuildNodeHolder()
    {
        if (nodeHolders.Count > 1 && !containerHolderCreated)
            CreateNodeContainer();
        else
            CreateNodeHolder();
    }

    public void RemoveNodeHolder()
    {
        foreach (GameObject nodeHolder in nodeHolders)
        {
            if (nodeHolder == nodeHolder.gameObject)
            {
                nodeHolders.Remove(nodeHolder);
                DestroyImmediate(nodeHolder, true);
            }
        }
    }

    public void ResetAllNodeHolders()
    {
        DestroyImmediate(containerHolder, true);

        //Invoke("RemoveNodeHolder", 0.1f);

        containerHolderCreated = false;
        nodeHolders.Clear();
    }
}

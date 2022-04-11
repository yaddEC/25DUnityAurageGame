using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuilder : MonoBehaviour
{
    [SerializeField] private GameObject nodeHolder;
    [SerializeField] private GameObject containerHolder;
    [SerializeField] private Transform container;
    private Transform parentTransform;
    [SerializeField] private bool containerHolderCreated;
    [SerializeField] private List<GameObject> nodeHolders = new List<GameObject>();

    private void Update()
    {
        foreach (GameObject nodeHolder in nodeHolders)
        {
            if (nodeHolder.transform.parent != container)
                nodeHolder.transform.parent = container;
        }
    }

    private void OnDrawGizmos()
    {
        parentTransform = container;

        foreach (GameObject nodeHolder in nodeHolders)
        {
            if (nodeHolder.transform.parent != container)
                nodeHolder.transform.parent = container;
        }
    }

    private void CreateNodeHolder()
    {
        nodeHolder = new GameObject();
        nodeHolder.transform.SetParent(parentTransform);
        var noderef = nodeHolder.AddComponent<NodeReference>();
        noderef.mask = NodeSettings.mask;

        nodeHolder.name = "NodeHolder";
        nodeHolder.tag = "NodeHolder";
        nodeHolders.Add(nodeHolder);
    }

    private void CreateNodeContainer()
    {
        containerHolder = new GameObject();
        containerHolder.transform.SetParent(parentTransform);

        containerHolder.name = "NodeContainer";
        containerHolder.tag = "NodeContainer";

        container = containerHolder.transform;
        containerHolderCreated = true;
    }

    public void BuildNodeHolder()
    {
        if (nodeHolders.Count >= 1 && !containerHolderCreated)
        {
            CreateNodeContainer();
            CreateNodeHolder();
        }
        else
            CreateNodeHolder();
    }

    public void DestroyLastNodeHolder()
    {

        var index = nodeHolders.Count -1;
        var t = nodeHolders[index];

        nodeHolders.RemoveAt(index);
        DestroyImmediate(t);

        if(nodeHolders.Count <= 1)
        {
            containerHolderCreated = false;
            DestroyImmediate(containerHolder);
        }
    }

    public void DestroyAllNodeHolders()
    {
        foreach (GameObject nodeHolder in nodeHolders)
        {
            DestroyImmediate(nodeHolder);
        }
        nodeHolders.Clear();

        DestroyImmediate(containerHolder, true);
        containerHolderCreated = false;
    }
}

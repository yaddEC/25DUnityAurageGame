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
        nodeHolder = new GameObject();
        nodeHolder.transform.SetParent(container);
        nodeHolder.AddComponent<NodeReference>();

        nodeHolder.name = "NodeHolder";
        nodeHolder.tag = "NodeHolder";
        nodeHolders.Add(nodeHolder);
    }

    private void CreateNodeContainer()
    {
        containerHolder = new GameObject();
        containerHolder.transform.SetParent(transform);

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

    public void DestroyNodeHolder()
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

    public void DestroyAll()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuilder : MonoBehaviour
{
    [SerializeField] private Transform transformParent;
    [SerializeField] private GameObject nodeHolder;
    [SerializeField] private GameObject containerHolder;
    [SerializeField] private Transform container;
    private Transform parentTransform;
    private bool containerHolderCreated;
    [SerializeField] private List<GameObject> nodeHolders = new List<GameObject>();
    private void OnDrawGizmos()
    {
        transformParent = GameObject.Find("App").GetComponent<Transform>();
        containerHolder.transform.SetParent(transformParent);

        parentTransform = container;

        foreach (GameObject nodeHolder in nodeHolders)
        {
            if (nodeHolder.transform.parent != container)
                nodeHolder.transform.parent = container;

            Gizmos.DrawIcon(nodeHolder.transform.position, "HolderIcon.tiff", true);
        }
    }

    private void Update()
    {
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
        var noderef = nodeHolder.AddComponent<NodeHolder>();
        noderef.mask = NodeSettings.mask;

        nodeHolder.name = "NodeHolder";
        nodeHolder.tag = "NodeHolder";
        nodeHolders.Add(nodeHolder);
    }

    private void CreateNodeContainer()
    {
        containerHolder = new GameObject();
        containerHolder.transform.SetParent(transformParent);

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

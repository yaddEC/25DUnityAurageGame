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

        containerHolder = GameObject.FindGameObjectWithTag("NodeContainer").gameObject;
        if (containerHolder != null)
        {
            container = containerHolder.transform;
            containerHolder.transform.SetParent(transformParent);
        }

        if (nodeHolder == null && nodeHolders.Count >= 1) nodeHolder = GameObject.FindGameObjectWithTag("NodeHolder").gameObject;

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
        if (nodeHolders.Count <= 0) return;
        else
        {
            var index = nodeHolders.Count-1;

            if (nodeHolders.Count <= 1)
            {
                nodeHolders[0] = SafeDestroyGameObject(nodeHolders[0]);
                nodeHolders.RemoveAt(0);

                containerHolderCreated = false;
                DestroyImmediate(containerHolder);
                Debug.Log(index + " 2");
            }
            else
            {
                for (int i = 0; i < nodeHolders.Count; i++)
                {
                    if(i == index) nodeHolders[i] = SafeDestroyGameObject(nodeHolders[i]);
                }
                nodeHolders.RemoveAt(index);
                Debug.Log(index + " 1");
            }
        }
    }

    public void DestroyAllNodeHolders()
    {
        for (int i = 0; i < nodeHolders.Count; i++)
            nodeHolders[i] = SafeDestroyGameObject(nodeHolders[i]);

        nodeHolders.Clear();

        DestroyImmediate(containerHolder, true);
        containerHolderCreated = false;
    }

    public static T SafeDestroy<T>(T obj) where T : Object
    {
        if (Application.isEditor) Object.DestroyImmediate(obj);
        else Object.Destroy(obj);
        return null;
    }
    public static T SafeDestroyGameObject<T>(T gameObject) where T : Object
    {
        if (gameObject != null) SafeDestroy(gameObject);
        return null;
    }
}

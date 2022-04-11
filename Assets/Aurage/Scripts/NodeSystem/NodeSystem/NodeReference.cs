using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Node
{
    public int nodeIndex;
    public GameObject nodeObject;
    private Transform nodeTransform;

    private Node(int _nodeIndex, GameObject _nodeObject, Transform _nodeTransform)
    {
        nodeIndex = _nodeIndex;
        nodeObject = _nodeObject;
        nodeTransform = _nodeTransform;
    }
    public static Node CreateNode(int _nodeIndex, GameObject _nodeObject, Transform _nodeTransform)
    {
        Node node = new Node(_nodeIndex, _nodeObject, _nodeTransform);
        return node;
    }
}

public class NodeReference : MonoBehaviour
{
    private GameObject node;
    private static int index;

    public LayerMask mask;

    public List<Node> nodes = new List<Node>();
    private List<Transform> nodeList = new List<Transform>();

    private void OnDrawGizmos()
    {
        nodeList.Clear(); nodes.Clear();
        nodeList.AddRange(GetComponentsInChildren<Transform>());

        foreach (Transform node in nodeList.ToArray())
        {
            if (!(node != this.transform && node.gameObject.tag != "NodeHolder"))
                nodeList.Remove(node);
        }

        foreach (Transform node in nodeList)
        {
            nodes.Add(Node.CreateNode(int.Parse(node.gameObject.name), node.gameObject, node.gameObject.transform));
            //Gizmos.DrawIcon(node.position,  , true);
        }

    }

    public void CreateNode()
    {
        node = new GameObject();
        node.transform.SetParent(this.transform);
        var noderef = node.AddComponent<NodeReference>();
        noderef.mask = NodeSettings.mask;

        node.name = index.ToString();
        node.tag = "Node";
        index++;
    }

    public void DestroyLastNode()
    {
        var indexList = nodeList.Count - 1;
        var t = nodeList[indexList];

        nodeList.RemoveAt(indexList);
        DestroyImmediate(t.gameObject);
        index--;
        CheckIndexReference();
    }

    public void DestroyAllNodes()
    {
        foreach (Transform node in nodeList)
        {
            Destroy(node);
        }
        nodeList.Clear();
    }

    private void CheckIndexReference()
    {
        if (index < 0)
            index = 0;
    }
}
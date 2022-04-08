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
        }
    }
}

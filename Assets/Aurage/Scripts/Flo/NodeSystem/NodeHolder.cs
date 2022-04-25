using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

public class NodeHolder : MonoBehaviour
{
    private GameObject node;
    private static int index;

    public LayerMask mask;

    public List<Node> nodes = new List<Node>();
    public List<Transform> nodeList = new List<Transform>();

    private void OnDrawGizmos()
    {
        mask = NodeSettings.mask;

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
            Gizmos.DrawIcon(node.position, "NodeIcon.tiff", true);
        }

    }

    public void CreateNode()
    {
        if (nodeList.Count < index || nodeList.Count > index)
            index = nodeList.Count;

        node = new GameObject();
        node.transform.SetParent(this.transform);

        if (nodeList.Count > 1) node.transform.position = nodeList[nodeList.Count - 1].transform.position;
        else node.transform.position = this.transform.position;

        var noderef = node.AddComponent<NodePath>();

        if (nodeList.Count > 0)
        {
            var refpath = nodeList[nodeList.Count - 1].GetComponent<NodePath>();
            refpath.nodePoints.Add(noderef); // adds reference of new node to prev node
        }

        node.name = index.ToString();
        node.tag = "Node";
        index++;
    }

    public void DestroyLastNode()
    {
        if (nodes.Count <= 0)
            return;
        else
        { 
            var indexList = nodeList.Count - 1;
            var t = nodeList[indexList];

            var refpath = nodeList[indexList-1].GetComponent<NodePath>();
            refpath.nodePoints.Remove(nodeList[indexList].GetComponent<NodePath>());

            nodeList.RemoveAt(indexList);
            DestroyImmediate(t.gameObject);
            index--;
        }
    }

    public void DestroyAllNodes()
    {
        if (nodes.Count <= 0) 
            return;
        else
        { 
            foreach (Node node in nodes) 
                DestroyImmediate(node.nodeObject);
        }
    }
}
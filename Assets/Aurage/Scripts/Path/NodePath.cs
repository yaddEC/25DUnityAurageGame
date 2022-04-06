using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePath : MonoBehaviour
{
    private NodeReference refNodeReference;

    public Color lineColor = Color.white;
    public List<NodePath> nodePoints = new List<NodePath>();
    public NodeWalker refNodeWalker;
    public GameObject colliderPrefab;

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        foreach (NodePath node in nodePoints)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    private void Start()
    {
        refNodeReference = GameObject.FindObjectOfType<NodeReference>();
        refNodeWalker = GameObject.FindObjectOfType<NodeWalker>();
        foreach (NodePath node in nodePoints)
        {
            node.SetNodeReferences(this);
        }
    }

    private void Update()
    {
        CheckRaycast();
    }

    public void OnPlayerNode()
    {
        refNodeWalker.refPrevNodePath = this;
        refNodeWalker.refNextNodePath = NodePathTarget(refNodeWalker.refPlayerMotion.RLValue, nodePoints.ToArray(), refNodeWalker.transform);
    }

    private void SetNodeReferences(NodePath nodePath)
    {
        nodePoints.Add(nodePath);
    }

    public static NodePath NodePathTarget(Vector2 input, NodePath[] nodePaths, Transform self)
    {
        float minDistance = 9999999;
        NodePath selectedPath = null;

        foreach (NodePath point in nodePaths)
        {
            if (minDistance > Vector3.Distance(input, Vector3.Normalize(point.transform.position - self.position)))
            {
                minDistance = Vector3.Distance(input, Vector3.Normalize(point.transform.position - self.position));
                selectedPath = point;
            }
        }
        return selectedPath;
    }

    private void CheckRaycast()
    {
        RaycastHit hit;

        foreach (NodePath node in nodePoints)
        {
            bool touched = Physics.Raycast(transform.position, node.transform.position - transform.position, out hit, Vector3.Distance(node.transform.position, transform.position), refNodeReference.mask);

            if (touched)
                Debug.Log(touched);
        }
    }
}

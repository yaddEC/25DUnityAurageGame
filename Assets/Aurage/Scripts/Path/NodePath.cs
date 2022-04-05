using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePath : MonoBehaviour
{
    public Color lineColor = Color.white;
    public List<NodePath> nodePoints = new List<NodePath>();
    public NodeWalker refNodeWalker;
    public GameObject colliderPrefab;

    public int pointIndex;

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
        int.TryParse(name, out pointIndex);

        refNodeWalker = GameObject.FindObjectOfType<NodeWalker>();
        foreach (NodePath node in nodePoints)
        {
            node.SetNodeReferences(this);
        }

        for (int i = 0; i < nodePoints.Count; i++)
        {
            Vector3 currPoint = nodePoints[i].transform.position;

            if (i > 0)
            {
                Vector3 targetPoint = nodePoints[i + 1].transform.position; //prevPoint
                //ColliderSetup(currPoint, targetPoint, nodePoints[i].transform);
            }
        }
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

    private void ColliderSetup(Vector3 currPoint, Vector3 targetPoint, Transform parent)
    {
        Vector3 middlePoint = new Vector3((currPoint.x + targetPoint.x) / 2, (currPoint.y + targetPoint.y) / 2, (currPoint.z + targetPoint.z) / 2);
        Vector3 relativePos = targetPoint - currPoint;
        float distance = Vector3.Distance(targetPoint, currPoint);
        colliderPrefab.GetComponent<CapsuleCollider>().height = distance;
        Instantiate(colliderPrefab, middlePoint, Quaternion.LookRotation(relativePos, Vector3.up), parent);
    }
}

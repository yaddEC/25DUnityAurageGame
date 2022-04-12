using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePath : MonoBehaviour
{
    private NodeWalker refNodeWalker;
    public NodeHolder refNodeHolder;

    public Color lineColor = Color.white;
    public List<NodePath> nodePoints = new List<NodePath>();

    public bool isSocketEntry = false;

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
        refNodeWalker = GameObject.FindObjectOfType<NodeWalker>();
        refNodeHolder = GetComponentInParent<NodeHolder>();

        foreach (NodePath node in nodePoints)
        {
            node.SetNodeReferences(this);
        }
    }
    private void Update()
    {
        if (!refNodeWalker.refPlayerMotion.isInPath && refNodeWalker.refPlayerMotion.canBeDetectedByRaycast && NodeSettings.canClampOnCable)
            CheckRaycast();
    }

    public void OnPlayerNodePath()
    {
        refNodeWalker.refPrevNodePath = this;
        refNodeWalker.refNextNodePath = NodePathTarget(InputManager.inputAxis, nodePoints.ToArray(), refNodeWalker.transform);
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
            if (Physics.Linecast(transform.position, node.transform.position, out hit, refNodeHolder.mask))
            {
                refNodeWalker.refPlayerMotion.isInPath = true;
                refNodeWalker.isFreezed = false;
                refNodeWalker.refPrevNodePath = node;
                refNodeWalker.refCurrNodePath = node;
                refNodeWalker.refNextNodePath = this;
            }
        }
    }
}
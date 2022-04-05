using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Node
{
    public int nodeBranchFrom;
    public int nodeBranchTo;
    public SplineEditor nextBranchPath;

    public Node(int _nodeBranchFrom, int _nodeBranchTo, SplineEditor _nextBranchPath)
    {
        nodeBranchFrom = _nodeBranchFrom;
        nodeBranchTo = _nodeBranchTo;

        nextBranchPath = _nextBranchPath;
    }
}

public class SplineEditor : MonoBehaviour
{
    public Color raycolor = Color.white;
    public List<Transform> pathPoints = new List<Transform>();
    public List<Node> nodes = new List<Node>();

    public GameObject colliderPrefab;
    public bool canChangeBranch = true;

    private void OnDrawGizmos()
    {
        pathPoints.Clear();
        pathPoints.AddRange(GetComponentsInChildren<Transform>());
        Gizmos.color = raycolor;

        foreach (Transform point in pathPoints.ToArray())
        {
            if (!(point != this.transform && point.gameObject.tag != "HolderPath"))
                pathPoints.Remove(point);
        }

        for (int i = 0; i < pathPoints.Count; i++)
        {
            Vector3 pointCurrent = pathPoints[i].position;

            if (i > 0)
            {
                Vector3 pointPrevious = pathPoints[i - 1].position;
                Gizmos.DrawLine(pointPrevious, pointCurrent);
            }
        }

        foreach (Node node in nodes)
        {
            Gizmos.DrawLine(pathPoints[node.nodeBranchFrom].position, node.nextBranchPath.pathPoints[node.nodeBranchTo].position);
        }
    }

    private void Awake()
    {
        for (int i = 0; i < pathPoints.Count; i++)
        {
            Vector3 currPoint = pathPoints[i].position;

            if (i > 0)
            {
                Vector3 targetPoint = pathPoints[i - 1].position; //prevPoint
                ColliderSetup(currPoint, targetPoint, pathPoints[i]);
            }
        }


        /*int k  = 0;
        foreach (Transform point in pathPoints)
        {
            SplineWaypoint waypoint = point.gameObject.AddComponent<SplineWaypoint>();
            waypoint.index = k;
            waypoint.refSplineEditor = this;
            k++;
        }*/


        foreach (Node node in nodes)
        {
            GameObject junction = Instantiate(new GameObject(), pathPoints[node.nodeBranchFrom].position, Quaternion.identity, this.transform);
            junction.name = "Junction";
            junction.tag = "HolderPath";

            Vector3 currPoint = pathPoints[node.nodeBranchFrom].position;
            Vector3 targetPoint = node.nextBranchPath.pathPoints[node.nodeBranchTo].position; // nextPoint
            ColliderSetup(currPoint, targetPoint, junction.transform);

            /*SplineWaypoint waypoint = junction.AddComponent<SplineWaypoint>();
            waypoint.index = node.nodeBranchTo;
            waypoint.refSplineEditor = node.nextBranchPath;*/
        }
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
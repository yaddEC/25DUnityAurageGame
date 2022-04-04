using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineEditor : MonoBehaviour
{
    public Color raycolor = Color.white;
    public List<Transform> pathPoints = new List<Transform>();
    private Transform[] pointList;
    public int indexBranch = 0;
    public int branchPointIndexToGo = 0;

    public GameObject colliderPrefab;
    public Transform colliderHolder;

    public SplineEditor branchPath;
    public bool canChangeBranch = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = raycolor;
        pointList = GetComponentsInChildren<Transform>();
        pathPoints.Clear();

        foreach (Transform point in pointList)
        {
            if (point != this.transform && point.gameObject.tag != "HolderPath")
                pathPoints.Add(point);
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
    }

    private void Start()
    {
        for (int i = 0; i < pathPoints.Count; i++)
        {
            Vector3 pointCurrent = pathPoints[i].position;

            if (i > 0)
            {
                Vector3 pointPrevious = pathPoints[i - 1].position;

                Vector3 middle = new Vector3((pointCurrent.x + pointPrevious.x) / 2, (pointCurrent.y + pointPrevious.y) / 2, (pointCurrent.z + pointPrevious.z) / 2);
                Vector3 relativePos = pointPrevious - pointCurrent;
                float distance = Vector3.Distance(pointPrevious, pointCurrent);

                colliderPrefab.GetComponent<CapsuleCollider>().height = distance;

                Instantiate(colliderPrefab, middle, Quaternion.LookRotation(relativePos, Vector3.up), colliderHolder);
            }
        }
    }
}
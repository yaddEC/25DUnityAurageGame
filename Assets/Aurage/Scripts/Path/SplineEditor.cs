using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineEditor : MonoBehaviour
{
    public Color raycolor = Color.white;
    public List<Transform> pathPoints = new List<Transform>();
    private Transform[] pointList;
    public int indexBranch = 0;

    public SplineEditor branchPath;
    public bool canChangeBranch = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = raycolor;
        pointList = GetComponentsInChildren<Transform>();
        pathPoints.Clear();

        foreach (Transform point in pointList)
        {
            if (point != this.transform)
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
}
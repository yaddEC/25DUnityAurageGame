using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public SplineEditor splineEditor;
    public int currentPointIndex = 0;

    public float moveSpeed;
    private float reachDistance = 0.1f;
    public float rotationSpeed = 5.0f;

    private void Update()
    {
        // keep
        if (currentPointIndex >= splineEditor.pathPoints.Count)
            return;

        // keep
        float distance = Vector3.Distance(splineEditor.pathPoints[currentPointIndex].position, transform.position);
        
        // remove
        transform.position = Vector3.MoveTowards(transform.position, splineEditor.pathPoints[currentPointIndex].position, Time.deltaTime * moveSpeed);

        // keep
        var rotation = Quaternion.LookRotation(splineEditor.pathPoints[currentPointIndex].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * moveSpeed);

        // keep
        if (distance <= reachDistance)
        {
            if (splineEditor.canChangeBranch && currentPointIndex == splineEditor.indexBranch)
            {
                currentPointIndex = splineEditor.branchPointIndexToGo;
                splineEditor = splineEditor.branchPath;
            }
            else
                currentPointIndex++;
        }
    }
}
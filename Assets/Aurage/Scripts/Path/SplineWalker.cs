using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public SplineEditor splineEditor;
    public int currentPointIndex = 0;

    public PlayerMotion refPlayerMotion;

    public bool right = true;
    public bool moveEnable = true;

    public float moveSpeed;
    private float reachDistance = 0.1f;
    public float rotationSpeed = 5.0f;

    private void Awake()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
    }
    private void Update()
    {
        if(refPlayerMotion.isInPath)
            PathWalker();
    }

    private void ClampOnPath()
    {

    }

    private void PathWalker()
    {
        // keep
        if (currentPointIndex >= splineEditor.pathPoints.Count)
            return;

        // keep
        float distance = Vector3.Distance(splineEditor.pathPoints[currentPointIndex].position, transform.position);

        // remove
        if(moveEnable)
            if(!right)
                transform.position = Vector3.MoveTowards(transform.position, splineEditor.pathPoints[currentPointIndex].position, Time.deltaTime * moveSpeed);
            else
                transform.position = Vector3.MoveTowards(transform.position, splineEditor.pathPoints[currentPointIndex].position, Time.deltaTime * moveSpeed);

        // keep
        var rotation = Quaternion.LookRotation(splineEditor.pathPoints[currentPointIndex].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * moveSpeed);

        // keep
        if (distance <= reachDistance)
        {
            if (splineEditor.canChangeBranch && currentPointIndex == splineEditor.indexBranch)
            {
                splineEditor = splineEditor.branchPath;
                currentPointIndex = 0;
            }
            else
                currentPointIndex++;
        }
    }
}
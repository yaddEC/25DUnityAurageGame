using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class NodeWalker : MonoBehaviour
{
    public PlayerMotion refPlayerMotion;

    public NodePath refNextNodePath;
    public NodePath refPrevNodePath;

    public NodePath refCurrNodePath;

    public float moveSpeed;
    public float rotationSpeed = 5.0f;

    public bool right;
    public bool moveEnable;

    public bool isOnNode = false;

    private void Start()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
    }

    private void Update()
    {
        isOnNode = Vector3.Distance(transform.position, refCurrNodePath.transform.position) < 0.1f;

        if(isOnNode)
            refCurrNodePath.OnPlayerNode();
        
        if (refPlayerMotion.isInPath)
            WalkOnPath();
    }

    private void WalkOnPath()
    {
        refCurrNodePath = NodePath.NodePathTarget(refPlayerMotion.RLValue, new NodePath[] { refNextNodePath, refPrevNodePath }, transform);

        if (moveEnable)
        {
            transform.position = Vector3.MoveTowards(transform.position, refCurrNodePath.transform.position, moveSpeed * Time.deltaTime);
        }

        var rotation = Quaternion.LookRotation(refNextNodePath.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, moveSpeed * Time.deltaTime);
    }
}


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
    public bool isFreezed = false;
    public bool xPressed = false;

    private void Start()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
    }

    private void Update()
    {
        isOnNode = Vector3.Distance(transform.position, refCurrNodePath.transform.position) < 0.1f;

        if (isOnNode)
        {
            refCurrNodePath.OnPlayerNode();

            if (!xPressed)
                isFreezed = true;
            else
                isFreezed = false;

            if (isFreezed)
            {
                FreezePlayer();
                SelectNextPath();
            }
        }
        else
        {
            xPressed = false;
            isFreezed = false;
        }


        if (refPlayerMotion.isInPath && !isFreezed)
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

    private void FreezePlayer()
    {
        refPlayerMotion.transform.position = refCurrNodePath.transform.position;
    }

    private void SelectNextPath()
    {
        if(refPlayerMotion.xPressed)
        {
            xPressed = true;
        }
    }
}


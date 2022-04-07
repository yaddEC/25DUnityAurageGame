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

    public bool isOnNode = false;
    public bool isFreezed = false;

    private void Start()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();
    }

    private void Update()
    {
        isOnNode = Vector3.Distance(transform.position, refCurrNodePath.transform.position) < 0.1f;

        if (refCurrNodePath.hitPath)
        {
            refPlayerMotion.transform.position = refCurrNodePath.t.position;
            refPlayerMotion.playerBody.velocity = Vector3.zero;
            refPlayerMotion.isGrounded = true;
            refCurrNodePath.hitPath = false;
        }

        if (isOnNode)
        {
            refCurrNodePath.OnPlayerNodePath();

            if (!InputManager.performX)
                isFreezed = true;
            else
                isFreezed = false;

            if (isFreezed)
            {
                FreezeOnNode();
            }
        }

        if (refPlayerMotion.isInPath && !isFreezed && InputManager.inputAxis.x != 0)
            WalkOnPath();
    }

    public void WalkOnPath()
    {
        var rotation = Quaternion.LookRotation(refNextNodePath.transform.position - transform.position);

        refCurrNodePath = NodePath.NodePathTarget(InputManager.inputAxis, new NodePath[] { refNextNodePath, refPrevNodePath }, transform);

        transform.position = Vector3.MoveTowards(transform.position, refCurrNodePath.transform.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, moveSpeed * Time.deltaTime);
    }

    private void FreezeOnNode()
    {
        refPlayerMotion.transform.position = refCurrNodePath.transform.position;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class NodeWalker : MonoBehaviour
{
    [HideInInspector] public PlayerMotion refPlayerMotion;

    [HideInInspector] public NodePath refNextNodePath;
    [HideInInspector] public NodePath refPrevNodePath;

    [HideInInspector] public NodePath refCurrNodePath;

    public float moveSpeed;
    public float rotationSpeed = 5.0f;

    private bool isOnNode = false;
    /*[HideInInspector]*/ public bool isFreezed = false;

    private void Awake()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();

        refNextNodePath = GameObject.FindObjectOfType<NodePath>();
        refPrevNodePath = GameObject.FindObjectOfType<NodePath>();

        refCurrNodePath = GameObject.FindObjectOfType<NodePath>();
    }

    private void Update()
    {
        isOnNode = Vector3.Distance(transform.position, refCurrNodePath.transform.position) < 0.1f;

        if (isOnNode)
            OnNodeHandler();

        if (refPlayerMotion.isInPath && !isFreezed && InputManager.inputAxis.x != 0)
            WalkOnPath();
    }

    private void OnNodeHandler()
    {
        refCurrNodePath.OnPlayerNodePath();

        if (NodeSettings.canStopOnNode)
        {
            if (!InputManager.performX)
                isFreezed = true;
            else
                isFreezed = false;
        }
        else
            isFreezed = false;

        if (isFreezed)
            FreezeOnNode();
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private static PlayerMotion refPlayerMotion;
    
    public static bool isVisible = true;
    public static bool canBeTargeted = true;

    public static bool isFreezed = false;
    public static bool isInMachine = false;
    public static bool isInNodePath = false;

    public static bool isGrounded = false;
    public static bool canDash = true;

    public bool InspectorisVisible = true;
    public bool InspectorcanBeTargeted = true;

    public bool InspectorisFreezed = false;
    public bool InspectorisInMachine = false;
    public bool InspectorisInNodePath = false;

    public bool InspectorisGrounded = false;
    public bool InspectorcanDash = true;

    private void Awake()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();

        isInMachine = isInNodePath = isFreezed = isGrounded = false;
        canBeTargeted = isVisible = canDash = true;
    }

    private void Update()
    {
        InspectorDisplay();

        if (isInMachine)
        {
            refPlayerMotion.playerRb.useGravity = false;
            canBeTargeted = false;
        }
        else
        {
            refPlayerMotion.playerRb.useGravity = true;
            canBeTargeted = true;
        }
    }

    public static void FreezePlayer()
    {
        refPlayerMotion.playerRb.constraints = RigidbodyConstraints.FreezeAll;
        refPlayerMotion.playerRb.velocity = Vector3.zero;
    }

    public static void UnFreezePlayer()
    {
        refPlayerMotion.playerRb.constraints = RigidbodyConstraints.None;
    }

    private void InspectorDisplay()
    {
        InspectorisVisible = isVisible;
        InspectorcanBeTargeted = canBeTargeted;
        InspectorisFreezed = isFreezed;
        InspectorisInMachine = isInMachine;
        InspectorisInNodePath = isInNodePath;
        InspectorisGrounded = isGrounded;
        InspectorcanDash = canDash;
        InspectorisVisible = isVisible;
    }
}
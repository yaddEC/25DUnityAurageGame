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

    public bool InspectorIsVisible = true;
    public bool InspectorIsCanBeTargeted = true;

    public bool InspectorIsFreezed = false;
    public bool InspectorIsInMachine = false;
    public bool InspectorIsInNodePath = false;

    public bool InspectorIsGrounded = false;
    public bool InspectorIsCanDash = true;

    private void Awake()
    {
        refPlayerMotion = GameObject.FindObjectOfType<PlayerMotion>();

        isInMachine = isInNodePath = isFreezed = isGrounded = false;
        canBeTargeted = isVisible = canDash = true;
    }

    private void Update()
    {
        InspectorIisplay();

        if (isInMachine)
        {
            isFreezed = true;
            refPlayerMotion.playerRb.useGravity = false;
            canBeTargeted = false;
        }
        else
        {
            isFreezed = false;
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

    private void InspectorIisplay()
    {
        InspectorIsVisible = isVisible;
        InspectorIsCanBeTargeted = canBeTargeted;
        InspectorIsFreezed = isFreezed;
        InspectorIsInMachine = isInMachine;
        InspectorIsInNodePath = isInNodePath;
        InspectorIsGrounded = isGrounded;
        InspectorIsCanDash = canDash;
        InspectorIsVisible = isVisible;
    }
}
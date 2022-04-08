using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSettings : MonoBehaviour
{
    public bool DashOnNode = false;
    public bool ClampOnCable = false;
    public bool StopOnNode = false;

    public LayerMask maskForRaycast;

    [Header("NodeSystem Config")]
    public static bool canDashOnNode = false;
    public static bool canClampOnCable = true;
    public static bool canStopOnNode = true;

    public static LayerMask mask;

    private void Update()
    {
        UpdateSettings();
    }

    private void OnDrawGizmos()
    {
        UpdateSettings();
    }

    private void UpdateSettings()
    {
        canDashOnNode = DashOnNode;
        canClampOnCable = ClampOnCable;
        canStopOnNode = StopOnNode;

        mask = maskForRaycast;
    }
}

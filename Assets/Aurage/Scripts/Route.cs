using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] anchorPoints;
    private Vector3 gizmoPos;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmoPos =              Mathf.Pow(1 - t, 3)     * anchorPoints[0].position + 
                      3 *           Mathf.Pow(1 - t, 2) * t * anchorPoints[1].position + 
                      3 * (1 - t) * Mathf.Pow(t, 2)         * anchorPoints[2].position + 
                                    Mathf.Pow(t, 3)         * anchorPoints[3].position;

            Gizmos.DrawSphere(gizmoPos, 0.25f);
        }

        Gizmos.DrawLine(new Vector3(anchorPoints[0].position.x, anchorPoints[0].position.y, anchorPoints[0].position.z), 
                        new Vector3(anchorPoints[1].position.x, anchorPoints[1].position.y, anchorPoints[1].position.z));

        Gizmos.DrawLine(new Vector3(anchorPoints[2].position.x, anchorPoints[2].position.y, anchorPoints[2].position.z), 
                        new Vector3(anchorPoints[3].position.x, anchorPoints[3].position.y, anchorPoints[3].position.z));

    }
}
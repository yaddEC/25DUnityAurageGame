using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform pf;
    public Transform ps;
    public Transform pt;
    public Transform pq;

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 pfirst  = Vector3.Lerp(p0, p1, t);
        Vector3 psecond = Vector3.Lerp(p1, p2, t);
        Vector3 pthird  = Vector3.Lerp(p2, p3, t);

        Vector3 pfirstsecond = Vector3.Lerp(pfirst, psecond, t);
        Vector3 psecondthird = Vector3.Lerp(psecond, pthird, t);

        Vector3 pfinal = Vector3.Lerp(pfirstsecond, psecondthird, t);
        return pfinal;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float delta = 1f - t;

        return 3f * delta * delta * (p1 - p0)       +
               6f * delta         * t * (p2 - p1)   +
               3f * t             * t * (p3 - p2);
    }

    [Range(0, 1)]
    public float t;

    private void Update()
    {
        transform.position = GetPoint(pf.position, ps.position, pt.position, pq.position, t);
        transform.rotation = Quaternion.LookRotation(GetFirstDerivative(pf.position, ps.position, pt.position, pq.position, t));
    }

    private void OnDrawGizmos()
    {
        int sigmentsNumber = 20;
        Vector3 preveousePoint = pf.position;

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float parement = (float)i / sigmentsNumber;
            Vector3 point = GetPoint(pf.position, ps.position, pt.position, pq.position, parement);
            Gizmos.DrawLine(preveousePoint, point);
            preveousePoint = point;
        }
    }
}

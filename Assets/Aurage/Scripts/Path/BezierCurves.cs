using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurves : MonoBehaviour
{
    // own lerp
    public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
        return a + (b - a) * t;
    }

    // handle curves with 3 points
    public static Vector2 QuadriaticCurve(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p0 = Lerp(a, b, t);
        Vector2 p1 = Lerp(b, c, t);
        return Lerp(p0, p1, t);
    }

    // handle curves with 4 points
    public static Vector2 CubicCurve(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
    {
        Vector2 p0 = QuadriaticCurve(a, b, c, t);
        Vector2 p1 = QuadriaticCurve(b, c, d, t);
        return Lerp(p0, p1, t);
    }

     // no need for curves > 4, easier to generate more and more points to have a better curve other than creating other anchor points
}

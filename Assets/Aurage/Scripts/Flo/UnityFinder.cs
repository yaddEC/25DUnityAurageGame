using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class UnityFinder
{
    public static Transform FindTransformInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
                return t.GetChild(i).transform;

        }
        return null;
    }

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
                return t.GetChild(i).gameObject;

        }
        return null;
    }

    public static Vector3 Projection(Vector3 p, Vector3 a, Vector3 b)
    {
        Vector3 axis = b - a;
        Vector3 point = p - a;
        float dp = Vector3.Dot(point, axis);
        return new Vector3((dp / (axis.x * axis.x + axis.y * axis.y)) * axis.x,
            (dp / (axis.x * axis.x + axis.y * axis.y)) * axis.y) + a;
    }
}
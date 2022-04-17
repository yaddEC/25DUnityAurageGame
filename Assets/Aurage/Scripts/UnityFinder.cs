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
}
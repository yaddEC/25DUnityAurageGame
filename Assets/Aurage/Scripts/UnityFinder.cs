using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class UnityFinder
{
    public static Transform FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
                return t.GetChild(i).transform;

        }
        return null;
    }
}
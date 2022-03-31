using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector] public PathSystem pathSystem;

    // on cree notre path
    public void CreatePath()
    {
        pathSystem = new PathSystem(transform.position);
    }
}

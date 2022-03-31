using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
    PathCreator pathCreator;
    PathSystem pathSystem;

    // fonction unity OnSceneGUI -> draw si on est sur la scène
    private void OnSceneGUI()
    {
        DrawPathOnScreen();
        GetMousePosition();
    }

    private void GetMousePosition()
    {
        // get la position de la souris
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        // check si on appuie bien sur la souris, et shift
        if(guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            // system d'undo
            Undo.RecordObject(pathCreator, "Add segment");
            pathSystem.AddSegment(mousePos); // ajoute d'un point lorsque l'on clique
        }
    }

    // fonction unity OnEnable -> permet de setup notre path à l'écran
    private void OnEnable()
    {
        pathCreator = (PathCreator)target;

        if (pathCreator.pathSystem == null)
            pathCreator.CreatePath();

        pathSystem = pathCreator.pathSystem;
    }

    private void DrawPathOnScreen()
    {
        // trace les segments entre nos points
        for (int i = 0; i < pathSystem.NumSegments; i++)
        {
            Vector2[] points = pathSystem.GetPointsInSegment(i); // get les points pour ensuite draw
            Handles.color = Color.black;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.red, null, 2);
        }

        // apparition des points d'encrage, que l'on peut bouger à notre convenance
        Handles.color = Color.yellow;
        for (int i = 0; i < pathSystem.NumPoints; i++)
        {
            // freeMoveHandle pour bouger le point
            Vector2 newPos = Handles.FreeMoveHandle(pathSystem[i], Quaternion.identity, .1f, Vector2.zero, Handles.CylinderHandleCap);

            // systeme d'undo et de refresh du point si on l'a bougé
            if (pathSystem[i] != newPos)
            {
                Undo.RecordObject(pathCreator, "Move point");
                pathSystem.MovePoint(i, newPos);
            }
        }
    }
}

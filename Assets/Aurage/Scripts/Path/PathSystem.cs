using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// utilisation des courbes de bezier pour créer un petit système de path creator pour le jeu Aurage
// https://fr.wikipedia.org/wiki/Courbe_de_B%C3%A9zier

[System.Serializable]
public class PathSystem
{
    [SerializeField] private List<Vector2> points;

    public PathSystem(Vector2 centre)
    {
        points = new List<Vector2>
        {
            centre  +Vector2.left,
            centre + (Vector2.left+Vector2.up)*.5f,
            centre + (Vector2.right+Vector2.down)*.5f,
            centre + Vector2.right
        };
    }

    // get easy access to values of List Vector2
    public Vector2 this[int i] {  get { return points[i]; } }

    // get the number of points in the points list
    public int NumPoints { get { return points.Count; } }

    // get number of segments in the path
    public int NumSegments { get { return (points.Count - 4) / 3 + 1; } }

    // add a segment ..
    public void AddSegment(Vector2 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) * .5f);
        points.Add(anchorPos);
    }

    // get points of a said segment
    public Vector2[] GetPointsInSegment(int i)
    {
        return new Vector2[]
        {
            points[i*3],
            points[i*3+1],
            points[i*3+2],
            points[i*3+3]
        };
    }

    // set position of said point to target position
    public void MovePoint(int i, Vector2 pos)
    {
        Vector2 deltaMove = pos - points[i]; // get changing position
        points[i] = pos;

        // check if we are moving an anchor point(yellow points)
        // since anchor point have 2 points, anchor point are multiple of 3
        if (i % 3 == 0)
        {
            if (i + 1 < points.Count)
                points[i + 1] += deltaMove;
            if (i - 1 >= 0)
                points[i - 1] += deltaMove;
        }
        else // then if it's not anchor point
        {
            // some upfront checkups
            bool nextPointIsAnchor = (i + 1) % 3 == 0;
            int anchorControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
            int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;

            // need to check if anchorControlIndex exists before doing anything
            if (anchorControlIndex >= 0 && anchorControlIndex < points.Count)
            {
                float distance = (points[anchorIndex] - points[anchorControlIndex]).magnitude;
                Vector2 direction = (points[anchorIndex] - pos).normalized;
                points[anchorControlIndex] = points[anchorIndex] + direction * distance;
            }
        }
    }
}

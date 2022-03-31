using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private Vector3 objectPosition;
    private int routeIndex = 0;
    private float tParam = 0;
    private float speedModifier = 0.5f;
    private bool toRefresh = true;

    private void Start()
    {
        routeIndex = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        toRefresh = true;
    }

    private void Update()
    {
        if (toRefresh)
            StartCoroutine(FollowAnchorPoints(routeIndex));
    }

    private IEnumerator FollowAnchorPoints(int routeIndex)
    {
        toRefresh = false;

        Vector3 p0 = routes[routeIndex].GetChild(0).position;
        Vector3 p1 = routes[routeIndex].GetChild(1).position;
        Vector3 p2 = routes[routeIndex].GetChild(2).position;
        Vector3 p3 = routes[routeIndex].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier * Time.timeScale;

            objectPosition = 
                                                Mathf.Pow(1 - tParam, 3)    * p0            + 
                            3 *                 Mathf.Pow(1 - tParam, 2)    * tParam * p1   + 
                            3 * (1 - tParam) *  Mathf.Pow(tParam, 2)        * p2            + 
                                                Mathf.Pow(tParam, 3)        * p3;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0;
        speedModifier = speedModifier * 0.90f;
        routeIndex += 1;

        if (routeIndex > routes.Length - 1)
            routeIndex = 0;

        toRefresh = true;
    }
}

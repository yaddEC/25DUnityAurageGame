using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB : MonoBehaviour
{
    public Material cableMaterial;

    public bool rgb = true;
    private bool toRefresh = true;

    public Color color;
    public float[] colors;


    private void Start()
    {
        colors = new float[3];
    }

    private void FixedUpdate()
    {
        if (rgb && toRefresh) StartCoroutine(RGBFeature());
        else if(!rgb) cableMaterial.SetColor("colorEmission", cableMaterial.GetColor("colorEmission"));
    }

    private IEnumerator RGBFeature()
    {
        toRefresh = false;

        //                 r                                   g                                   b
        colors[0] = Random.Range(0f, 1f);   colors[1] = Random.Range(0f, 1f);   colors[2] = Random.Range(0f, 1f);
        color = new Color(colors[0], colors[1], colors[2], 1);
        cableMaterial.SetColor("colorEmission", color);

        yield return new WaitForSeconds(0.5f);
        toRefresh = true;
    }
}

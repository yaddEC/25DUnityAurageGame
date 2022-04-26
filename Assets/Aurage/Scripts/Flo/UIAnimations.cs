using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimations : MonoBehaviour
{
    public Image logoImage;
    private bool refresh = true;

    private void Awake()
    {
        logoImage = GameObject.FindGameObjectWithTag("UIAnimation").GetComponent<Image>();
    }

    private void Update()
    {
        if (refresh)
            StartCoroutine(AlphaColor());
    }

    private IEnumerator AlphaColor()
    {
        var colorAlpha = logoImage.color.a + 0.01f;

        refresh = false;
        logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, colorAlpha);
        yield return new WaitForSeconds(0.1f);
        refresh = true;
    }
}

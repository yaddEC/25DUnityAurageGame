using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    private PowerManager refPowerManager;
    private Slider refLifeSlider;
    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refLifeSlider = GameObject.FindObjectOfType<Slider>();
        SetMaxLife(refPowerManager.maxPower);
    }
    private void Update()
    {
        SetLife(refPowerManager.currentPower);
    }

    public void SetMaxLife(float life)
    {
        refLifeSlider.maxValue = life;
        refLifeSlider.value = life;
    }

    public void SetLife(float life)
    {
        refLifeSlider.value = life;
    }
}

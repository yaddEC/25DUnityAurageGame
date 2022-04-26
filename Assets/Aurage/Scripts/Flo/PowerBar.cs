using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public PowerManager refPowerManager;
    public Slider refLifeSlider;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        refLifeSlider = GameObject.FindGameObjectWithTag("LifeBar").GetComponent<Slider>();
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

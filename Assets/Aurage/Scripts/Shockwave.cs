using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private PowerManager refPowerManager;
    public int shockwavePower = 4;
    public LayerMask mask;
    public bool refresh = true;
    public float cooldown = 2;
    private float cachedCooldown;
    private GameObject shockWavePrefab;
    private GameObject shockWaveClone;
    public int scale;
    public float fadeSpeed;
    public float duration;

    private void Awake()
    {
        shockWavePrefab = Resources.Load<GameObject>("Prefabs/Setup/ElectricShock");
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        cachedCooldown = cooldown;
    }
    private void Update()
    {
        if (refPowerManager.currentPower >= 75)      { scale = 10; fadeSpeed = 2; duration = 1f; } 
        else if (refPowerManager.currentPower >= 45) { scale = 8; fadeSpeed = 3; duration = 0.65f; }
        else if (refPowerManager.currentPower >= 20) { scale = 6; fadeSpeed = 4; duration = 0.5f; }  
        else                                         { scale = 4; fadeSpeed = 5; duration = 0.4f; }              

        if (cooldown > 0) cooldown -= Time.deltaTime;
        if (cooldown <= 0) refresh = true;

        if (InputManager.performY && cooldown <= 0 && refresh && !PowerManager.isInMachine) Attack();
    }


    private void Attack()
    {
        shockWaveClone = Instantiate(shockWavePrefab, transform.position, Quaternion.identity);
        shockWaveClone.GetComponent<ElectricShock>().oneScale *= (scale - 1);
        shockWaveClone.GetComponent<ElectricShock>().fadeSpeed =fadeSpeed ;
        Destroy(shockWaveClone, duration);
        refPowerManager.currentPower -= refPowerManager.currentPower / shockwavePower;
        refresh = false;
        cooldown = cachedCooldown;
    }
}

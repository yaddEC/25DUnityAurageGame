using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private PowerManager refPowerManager;
    public int shockwavePower = 4;
    private float intensity;
    public LayerMask mask;
    public bool refresh = true;
    public float cooldown = 2;
    private float cachedCooldown;

    public int[] waveZone;
    public int zoneIndex;

    private int delta;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, delta);
    }

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        cachedCooldown = cooldown;
    }
    private void Update()
    {
        if      (refPowerManager.currentPower >= 75)   zoneIndex = 0;
        else if (refPowerManager.currentPower >= 45)   zoneIndex = 1;
        else if (refPowerManager.currentPower >= 20)   zoneIndex = 2;
        else                                           zoneIndex = 3;

        delta = waveZone[zoneIndex];

        if (cooldown > 0) cooldown -= Time.deltaTime;
        if (cooldown <= 0) refresh = true;

        if (InputManager.performY && cooldown <= 0 && refresh && !PowerManager.isInMachine)
        {
            GetIntensity();
            Attack();
        }
    }

    private void GetIntensity()
    {
        intensity = refPowerManager.currentPower - (refPowerManager.currentPower / shockwavePower);
        refPowerManager.currentPower /= shockwavePower;
        refresh = false;
    }

    private void Attack()
    {
        Collider[] colliders;

        colliders = Physics.OverlapSphere(transform.position, delta, mask);
        foreach (Collider col in colliders)
        {
            var c = col.GetComponent<Rigidbody>();
            if (c) c.AddForce((c.transform.position - transform.position).normalized * intensity * 100); 
        }

        cooldown = cachedCooldown;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        
      

    }

    public void Aim(InputAction.CallbackContext value)
    {
        if(value.started)
            Debug.Log("Aim");
        
    }

    public void Fire(InputAction.CallbackContext value)
    {
        if (value.performed)
            Debug.Log("Fire");

    }
}

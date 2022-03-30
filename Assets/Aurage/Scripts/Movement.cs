using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    
    public GameObject aiming;
    GameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
      

    }

    public void Aim(InputAction.CallbackContext value)
    {
        if (value.performed)
            clone = Instantiate(aiming, transform.position+Vector3.right, Quaternion.identity);

    }

    public void Fire(InputAction.CallbackContext value)
    {

        if (value.performed)
            Destroy(clone);

    }


}

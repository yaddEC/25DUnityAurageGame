using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    
    public GameObject aimingArrow;
    public GameObject bullet;
    GameObject aimingArrowClone;
    GameObject bulletClone;

    bool isAiming = false;
    public Vector2 aimDir ;
    // Start is called before the first frame update
    void Start()
    {
        aimingArrowClone = Instantiate(aimingArrow, transform.position + Vector3.right, Quaternion.identity);
        aimingArrowClone.transform.parent = gameObject.transform;
        aimingArrowClone.transform.localScale = new Vector3(0, 0, 0);
        aimDir = Vector2.right;
    }

  
    // Update is called once per frame
    void Update()
    {
        
      

    }

    public void Aim(InputAction.CallbackContext value)
    {

        isAiming =true;
        if (value.performed)
        {
            aimingArrowClone.transform.localScale = new Vector3(1, 1, 1);
        }
           
       

    }

    public void LeftStick(InputAction.CallbackContext value)
    {
        if(isAiming)
        {

        if (value.performed)
            {
                aimDir = value.ReadValue<Vector2>();
                
                
            }

         
            transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(aimDir.x, aimDir.y) * -180 / Mathf.PI + 90f);

        }
        else
        {
            //Movement
        }
    }

    public void Fire(InputAction.CallbackContext value)
    {

        if (value.performed)
        {
            aimingArrowClone.transform.localScale = new Vector3(0, 0, 0);
            isAiming = false;
            bulletClone = Instantiate(bullet, transform.position , Quaternion.identity);
            bulletClone.GetComponent<ElectricityBullet>().moveDirection = new Vector3(aimDir.x,aimDir.y,0);
            
        }


    }


}

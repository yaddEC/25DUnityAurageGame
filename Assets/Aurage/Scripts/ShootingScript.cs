using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    private GameObject aimingArrow;
    private GameObject rotationAxis;
    private GameObject bullet;
    private GameObject rotationAxisClone;
    private GameObject aimingArrowClone;
    private GameObject bulletClone;
    private PowerManager playerPower;
    public Vector2 input;

    bool isAiming = false;
  

    private void Start()
    {
        aimingArrow = Resources.Load<GameObject>("Prefabs/AimingArrow");
        rotationAxis = Resources.Load<GameObject>("Prefabs/RotationAxis");
        bullet = Resources.Load<GameObject>("Prefabs/ElectricityBullet");
        rotationAxisClone = Instantiate(rotationAxis, transform.position , Quaternion.identity);
        aimingArrowClone = Instantiate(aimingArrow, transform.position + Vector3.right, Quaternion.identity);
        playerPower = gameObject.GetComponent<PowerManager>();
        
        aimingArrowClone.transform.parent = rotationAxisClone.transform;
        aimingArrowClone.transform.localScale = new Vector3(0, 0, 0);
      
    }

    public void Aim(InputAction.CallbackContext value)
    {
        isAiming = true;

        if (value.performed)
            aimingArrowClone.transform.localScale = new Vector3(1, 1, 1);
    }

    void FixedUpdate()
    {

        input = InputManager.inputAxis;
        if (isAiming)
            rotationAxisClone.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(InputManager.inputAxis.x, InputManager.inputAxis.y) * -180 / Mathf.PI + 90f);
       
        rotationAxisClone.transform.position = transform.position;
    }



    public void Fire(InputAction.CallbackContext value)
    {
        if (value.performed )
        {
            aimingArrowClone.transform.localScale = new Vector3(0, 0, 0);
            isAiming = false;

            if (playerPower.currentPower >= 20 && InputManager.inputAxis != Vector2.zero)
            {
                playerPower.currentPower = playerPower.currentPower - 20;
                bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
                bulletClone.GetComponent<ElectricityBullet>().moveDirection = new Vector3(InputManager.inputAxis.x, InputManager.inputAxis.y);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotion : MonoBehaviour
{
    public float    moveSpeed;
    public int      planDistance;
    public float    planChangeSpeed;

    private Rigidbody playerBody;
    private Transform playerPos;
    private Vector3 velocity = Vector3.zero;
    private float RLValue;
    private int PlanValue;
    private bool changePlanDone = true;
    private bool isDashing = false;

    //---------function---------
    private void SwitPlan(int _ud)
    {
        if (!changePlanDone)
        {
            Vector3 newVelocity = new Vector3(playerBody.velocity.x, playerBody.velocity.y,(_ud * Time.deltaTime) * planChangeSpeed);
            playerBody.velocity = Vector3.SmoothDamp(playerBody.velocity, newVelocity, ref velocity, .05f);
            if(_ud == -1 && playerPos.position.z <= 0.01) 
            {
                playerBody.velocity = new Vector3(playerBody.velocity.x, playerBody.velocity.y, 0);
                playerPos.position = new Vector3(playerPos.position.x, playerPos.position.y, 0);
                changePlanDone = true;
            }else if (_ud == 1 && playerPos.position.z >= planDistance-0.01)
            {
                playerBody.velocity = new Vector3(playerBody.velocity.x, playerBody.velocity.y, 0);
                playerPos.position = new Vector3(playerPos.position.x, playerPos.position.y, planDistance);
                changePlanDone = true;
            }
            if (_ud == 0)
                changePlanDone = true;
            
        }
    }
    private void Move(float _rl)
    {

        Vector3 newVelocity = new Vector2((_rl*Time.deltaTime)* moveSpeed, playerBody.velocity.y);
        playerBody.velocity = Vector3.SmoothDamp(playerBody.velocity, newVelocity, ref velocity, .05f);
    }
    //-------------------------

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        playerPos = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if(!isDashing)
            SwitPlan(PlanValue);
        else
        {
            playerBody.velocity = new Vector3(playerBody.velocity.x * 2, playerBody.velocity.y+5, playerBody.velocity.z);
            isDashing = false;
        }
        Move(RLValue);
    }

    public void onPlanInput(InputAction.CallbackContext context)
    {
        if (changePlanDone)
        {
            PlanValue = Mathf.RoundToInt(context.ReadValue<Vector2>().y);
            changePlanDone = false;
        }
    }
    public void onMoveRightLeft(InputAction.CallbackContext context)
    {
        RLValue = context.ReadValue<Vector2>().x;
    }
    public void onDashPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDashing = true;
        }
    }
}

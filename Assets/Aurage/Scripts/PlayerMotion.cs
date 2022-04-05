using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotion : MonoBehaviour
{
    public float    moveSpeed;
    public int      planDistance;
    public float    planChangeSpeed;
    public float    jumpPower;
    public float    dashSpeed;

    public bool isInLamp;
    public bool isInPath;
    public Vector3 lockPosition;

    private Rigidbody playerBody;
    private Transform playerPos;
    private Vector3 velocity = Vector3.zero;
    public Vector2 RLValue;
    private int PlanValue;
    private bool changePlanDone = true;
    private bool isDashing = false;
    public bool xPressed = false;

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

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position + new Vector3(0.4f, 0, 0), Vector3.down, 0.55f) || Physics.Raycast(transform.position + new Vector3(-0.4f, 0, 0), Vector3.down, 0.55f) || Physics.Raycast(transform.position, Vector3.down, 0.55f))
            return true;
        return false;
    }
    private void FreezPos()
    {
        
        playerPos.localPosition = lockPosition;
        playerBody.velocity = new Vector3(0,0,0);
    }
    //-------------------------

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        playerPos = GetComponent<Transform>();
    }
    //use for Degub can be removed

    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0.4f, 0, 0), Vector3.down*0.55f, Color.red);
        Debug.DrawRay(transform.position + new Vector3(-0.4f, 0, 0), Vector3.down*0.55f, Color.red);
        Debug.DrawRay(transform.position, Vector3.down * 0.55f, Color.red);
    }
    private void FixedUpdate()
    {
        if (isInLamp)
        {
            if (isDashing)
            {
                playerBody.velocity = new Vector3(playerBody.velocity.x + RLValue.x *dashSpeed, playerBody.velocity.y, playerBody.velocity.z);
                isDashing = false;
                isInLamp = false;
            }
            FreezPos();
        }
        else if (isInPath)
        {
            if (RLValue.x != 0)
                GetComponent<NodeWalker>().moveEnable = true;
            else
                GetComponent<NodeWalker>().moveEnable = false;
            if (RLValue.x > 0.5)
                GetComponent<NodeWalker>().right = true;
            if (RLValue.x < -0.5)
                GetComponent<NodeWalker>().right = false;

        }
        else
        {
            if (IsGrounded())
            {

                if (!isDashing)
                    SwitPlan(PlanValue);
                else
                {
                    playerBody.velocity = new Vector3(playerBody.velocity.x * dashSpeed, playerBody.velocity.y + jumpPower, playerBody.velocity.z);
                    isDashing = false;
                }
            }
            Move(RLValue.x);
        }
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
        RLValue = context.ReadValue<Vector2>();
    }
    public void onDashPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
            isDashing = true;
    }
    public void onXPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
            xPressed = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotion : MonoBehaviour
{
    private PowerManager refPowerManager;
    public static Station refStation;
    public Station station;
    public float moveSpeed;

    public float dashSpeed;
    public float dashGravity;
    public bool canDash = true;
    public float dashCooldown = 10f;
    public float cachedDashCooldown;

    public float dashPower = 10;

    public bool canBeDetectedByRaycast = true;
    public bool isInPath;

    public Rigidbody playerRb;
    private Vector3 velocity = Vector3.zero;

    public LayerMask floorMask;

    public bool isGrounded;

    public static bool canBeTargeted;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.down / 5, transform.localScale.x / 2);
    }

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        cachedDashCooldown = dashCooldown;

        playerRb = GetComponent<Rigidbody>();
        playerRb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        Debug.Log(playerRb.useGravity + " gravity");

        if (PowerManager.isInMachine) playerRb.useGravity = false;
        else playerRb.useGravity = true;

        station = refStation;

        MovementUpdate();

        if (!canDash) dashCooldown -= Time.deltaTime;

        if (dashCooldown <= 0)
        {
            dashCooldown = cachedDashCooldown;
            canDash = true;
        }

        ClampInMachine();
    }

    private void FixedUpdate()
    {
        if (!isInPath) MovementFixedUpdate();
    }

    //--------------------------------------------------
    private void MovementUpdate()
    {
        if (!isInPath)
        {
            //if(!PowerManager.isInMachine) playerRb.useGravity = true;
            //else playerRb.useGravity = false;
            GroundCheck();
        }
        else
        {
            //playerRb.useGravity = false;
            PowerManager.isInMachine = true;

            if (NodeSettings.canDashOnNode) DashCheck(true);
            else canDash = false;
        }
    }

    private void MovementFixedUpdate()
    {
        if (isGrounded) FloorMovement();

        else if (!isGrounded)
            playerRb.velocity += Vector3.down * dashGravity * Time.fixedDeltaTime;
    }

    private void Move(Vector2 input)
    {
        playerRb.velocity = new Vector3(input.x * moveSpeed, playerRb.velocity.y, input.y * moveSpeed) * Time.fixedDeltaTime;
    }

    private void Dash()
    {
        if(isGrounded && !PowerManager.isInMachine)
        {
            playerRb.AddForce(dashSpeed * Vector3.up);

            refPowerManager.currentPower -= dashPower;
            canDash = false;
        }
        else if(PowerManager.isInMachine)
        {
            if (InputManager.inputAxis != Vector2.zero) playerRb.AddForce(dashSpeed * 1.5f * InputManager.inputAxis);
            else playerRb.AddForce(dashSpeed * 1.5f * Vector3.up);
        }

        PowerManager.isInMachine = false;
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down / 5, transform.localScale.x / 2, floorMask);
    }

    private void FloorMovement()
    {
        DashCheck(true);

        if (!InputManager.performTrigger && InputManager.inputAxis != Vector2.zero) Move(InputManager.inputAxis);
        else Move(Vector2.zero);
    }

    //--------------------------------------------------
    public void DashCheck(bool b)
    {
        if(b)
        {
            if (InputManager.performA && canDash)
            {
                if (isInPath) StartCoroutine(RaycastDetection());
                Dash();
            }
        }
        else
            if (InputManager.performA)
                Dash();
    }

    private IEnumerator RaycastDetection()
    {
        Debug.Log("StartedRaycastDetection");
        //playerRb.useGravity = true; 
        isInPath = false;
        canBeDetectedByRaycast = false;
        yield return new WaitForSeconds(0.1f);
        canBeDetectedByRaycast = true;
    }
    public void ClampInMachine()
    {
        if (PowerManager.isInMachine)
            transform.position = refStation.lockPosition.position;
    }
}
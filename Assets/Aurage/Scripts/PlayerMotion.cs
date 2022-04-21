using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotion : MonoBehaviour
{
    private PowerManager refPowerManager;

    public float moveSpeed;

    public float dashSpeed;
    public float dashGravity;
    public bool canDash = true;
    public float dashCooldown = 10f;
    public float cachedDashCooldown;

    public float dashPower = 10;

    public bool canBeDetectedByRaycast = true;
    public bool isInPath;

    public Rigidbody rb;
    private Vector3 velocity = Vector3.zero;

    public LayerMask wallMask;
    public LayerMask floorMask;

    public bool isGrounded;

    public static bool canBeTargeted;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();

        cachedDashCooldown = dashCooldown;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (PowerManager.isInMachine) rb.useGravity = false;
        MovementUpdate();

        if (!canDash) dashCooldown -= Time.deltaTime;

        if (dashCooldown <= 0)
        {
            dashCooldown = cachedDashCooldown;
            canDash = true;
        }
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
            rb.useGravity = true;
            GroundCheck();
        }
        else
        {
            rb.useGravity = false;
            PowerManager.isInMachine = true;

            if (NodeSettings.canDashOnNode) DashCheck();
            else canDash = false;
        }
    }

    private void MovementFixedUpdate()
    {
        if(isGrounded) FloorMovement();
        rb.velocity += Vector3.down * dashGravity * Time.fixedDeltaTime;
    }

    private void Move(Vector2 input)
    {
        rb.velocity = new Vector3(input.x * moveSpeed, rb.velocity.y, input.y * moveSpeed) * Time.fixedDeltaTime;
    }

    private void Dash()
    {
        if(isGrounded)
        {
            rb.AddForce(dashSpeed * Vector3.up);
        }
        else
        {
            if (InputManager.inputAxis != Vector2.zero) rb.AddForce(dashSpeed * 1.5f * InputManager.inputAxis);
            else rb.AddForce(dashSpeed * 1.5f * Vector3.up);
        }

        refPowerManager.currentPower -= dashPower;
        canDash = false;
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.position, transform.localScale.x/2, floorMask);
    }

    private void FloorMovement()
    {
        DashCheck();

        if (!InputManager.performTrigger && InputManager.inputAxis != Vector2.zero) Move(InputManager.inputAxis);
        else Move(Vector2.zero);
    }

    //--------------------------------------------------
    private void DashCheck()
    {
        if (InputManager.performA && canDash)
        {
            if(isInPath) StartCoroutine(RaycastDetection());
            Dash();
        }
    }

    private IEnumerator RaycastDetection()
    {
        rb.useGravity = true; isInPath = false;
        canBeDetectedByRaycast = false;
        yield return new WaitForSeconds(0.1f);
        canBeDetectedByRaycast = true;
    }
}
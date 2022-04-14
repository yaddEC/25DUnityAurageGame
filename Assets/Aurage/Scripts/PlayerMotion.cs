using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotion : MonoBehaviour
{
    private PowerManager refPowerManager;

    public float moveSpeed;
    public int[] planList;

    public float dashSpeed;
    public float dashGravity;
    public bool canDash = true;
    public float dashCooldown = 10f;
    public float cachedDashCooldown;

    public float dashPower = 10;

    public int currentplan = 1;
    public float changePlanTime;

    public bool canBeDetectedByRaycast = true;
    public bool isInPath;

    public Rigidbody rb;
    private Vector3 velocity = Vector3.zero;

    public LayerMask wallMask;
    public LayerMask floorMask;

    public bool isGrounded;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();

        cachedDashCooldown = dashCooldown;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovementUpdate();

        if (!canDash)
            dashCooldown -= Time.deltaTime;

        if (dashCooldown <= 0)
        {
            dashCooldown = cachedDashCooldown;
            canDash = true;
        }
    }

    private void FixedUpdate()
    {
        MovementFixedUpdate();
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
            rb.velocity = Vector3.zero;
            PowerManager.canLoosePower = false;

            if (NodeSettings.canDashOnNode)
                DashCheck();
            else
                canDash = false;
        }
    }

    private void MovementFixedUpdate()
    {
        if (!isInPath && isGrounded)
            FloorMovement();

        if (!isInPath)
            rb.velocity += Vector3.down * dashGravity * Time.fixedDeltaTime;
    }

    private void Move(Vector2 input)
    {
        rb.velocity = new Vector3(-input.x, rb.velocity.y, -input.y) * moveSpeed * Time.fixedDeltaTime;
    }

    private void Dash()
    {
        rb.AddForce(dashSpeed * Vector2.up);
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

        if (!InputManager.performTrigger && InputManager.inputAxis != Vector2.zero)
            Move(InputManager.inputAxis);
        else
            Move(Vector2.zero);
    }

    //--------------------------------------------------
    private void DashCheck()
    {
        if (InputManager.performA && canDash)
        {
            isInPath = false;
            Dash();
            StartCoroutine(RaycastDetection());
        }
    }

    private IEnumerator RaycastDetection()
    {
        canBeDetectedByRaycast = false;
        yield return new WaitUntil(() => isGrounded == true);
        canBeDetectedByRaycast = true;
    }
}
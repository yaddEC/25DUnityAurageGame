using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotion : MonoBehaviour
{
    private NodeWalker refNodeWalker;
    private PowerManager refPowerManager;

    public float moveSpeed;
    public int[] planList;

    public float dashSpeed;
    public float dashGravity;
    public bool canDash = true;
    public float dashCooldown = 10f;
    public float cachedDashCooldown;

    public float dashPower = 10;

    public int currentplan;
    public float changePlanTime;

    public bool canBeDetectedByRaycast = true;
    public bool isInPath;

    public Rigidbody rb;
    private Vector3 velocity = Vector3.zero;

    public LayerMask wallMask;
    public LayerMask floorMask;

    public bool isGrounded;
    

    public static bool isInMachine;

    //--------------------------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, transform.localScale.x/2);
    }
    private void Awake()
    {
        cachedDashCooldown = dashCooldown;

        rb = GetComponent<Rigidbody>();
        refNodeWalker = GameObject.FindObjectOfType<NodeWalker>();
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();
        currentplan = 1;
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
            if(!isInMachine)
                PowerManager.canLoosePower = true;
            else
                PowerManager.canLoosePower = false;

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
        Vector3 newVelocity = new Vector2((input.x * Time.deltaTime) * moveSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, newVelocity, ref velocity, .05f);
    }
    private void Dash(Vector2 input)
    {
        rb.velocity += new Vector3(input.x, input.y, 0).normalized * dashSpeed;
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
        PlanCheck();

        if (!InputManager.performTrigger)
            Move(InputManager.inputAxis);
        else
            Move(Vector2.zero);
    }

    private void ChangePlan(bool f, bool b)
    {
        if (InputManager.inputAxis.y > 0 && currentplan != planList.Length && !f)
            currentplan += 1;
        else if (InputManager.inputAxis.y < 0 && currentplan != 0 && !b)
            currentplan -= 1;

        transform.position = new Vector3(transform.position.x, transform.position.y, planList[currentplan]);
    }

    //--------------------------------------------------
    private void DashCheck()
    {
        if (InputManager.performA && canDash)
        {
            isInPath = false;
            Dash(InputManager.inputAxis);
            StartCoroutine(RaycastDetection());
        }
    }

    private void PlanCheck()
    {
        var hitForward = Physics.Raycast(transform.position, Vector3.forward, 2, wallMask);
        var hitBackward = Physics.Raycast(transform.position, Vector3.back, 2, wallMask);

        if (InputManager.performY && InputManager.inputAxis.y != 0)
            ChangePlan(hitForward, hitBackward);
    }

    private IEnumerator RaycastDetection()
    {
        canBeDetectedByRaycast = false;
        yield return new WaitUntil(() => isGrounded == true);
        canBeDetectedByRaycast = true;
    }
}
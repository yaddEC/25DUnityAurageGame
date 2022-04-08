using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotion : MonoBehaviour
{
    private NodeWalker refNodeWalker;

    public float moveSpeed;
    public int[] planList;

    public float dashSpeed;
    public float dashGravity;

    public int currentplan;
    public float changePlanTime;

    public bool canBeDetectedByRaycast = true;
    public bool isInPath;

    public Rigidbody rb;
    private Vector3 velocity = Vector3.zero;

    public LayerMask wallMask;

    public bool isGrounded;

    //--------------------------------------------------
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        refNodeWalker = GameObject.FindObjectOfType<NodeWalker>();
        currentplan = 1;
    }

    private void Update()
    {
        MovementUpdate();
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

            DashCheck();
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
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.55f);
    }

    private void FloorMovement()
    {
        DashCheck();
        PlanCheck();

        Move(InputManager.inputAxis);
    }

    private void ChangePlan(bool f, bool b)
    {
        if (InputManager.inputAxis.y > 0 && currentplan != planList.Length && !f)
            currentplan += 1;
        else if (InputManager.inputAxis.y < 0 && currentplan != 0 && !b)
            currentplan -= 1;

        transform.position = new Vector3(transform.position.x, transform.position.y, planList[currentplan]);
        InputManager.performChangePlan = false;
    }

    //--------------------------------------------------
    private void DashCheck()
    {
        if (InputManager.performDash)
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

        if (InputManager.performChangePlan && InputManager.inputAxis.y != 0)
            ChangePlan(hitForward, hitBackward);
    }

    private IEnumerator RaycastDetection()
    {
        canBeDetectedByRaycast = false;
        yield return new WaitUntil(() => isGrounded == true);
        canBeDetectedByRaycast = true;
    }
}
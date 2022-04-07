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

    public int currentplan;
    public float changePlanTime;
    public bool reversePlanIndex = false;

    public bool canBeDetectedByRaycast = true;

    public bool isInPath;

    public Rigidbody playerBody;
    private Vector3 velocity = Vector3.zero;

    public bool isGrounded;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        refNodeWalker = GameObject.FindObjectOfType<NodeWalker>();
        currentplan = 1;
    }

    private void Update()
    {
        if (!isInPath)
            GroundCheck();

        if(isInPath)
        {
            isGrounded = false;
            playerBody.useGravity = false;
        }
    }

    private void FixedUpdate()
    {
        if (!isInPath && isGrounded)
            FloorMovement();
    }

    private void Move(Vector2 input)
    {
        Vector3 newVelocity = new Vector2((input.x * Time.deltaTime) * moveSpeed, playerBody.velocity.y);
        playerBody.velocity = Vector3.SmoothDamp(playerBody.velocity, newVelocity, ref velocity, .05f);
    }

    private void GroundCheck()
    {
        playerBody.useGravity = true;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.55f);
    }

    private void Dash(Vector2 input)
    {
        Vector3 newVelocity = new Vector2(input.x, input.y) * dashSpeed * Time.deltaTime;
        playerBody.velocity = newVelocity;
        InputManager.performDash = false;
    }

    private void FloorMovement()
    {
        if (InputManager.performDash)
        {
            Dash(InputManager.inputAxis);
            StartCoroutine(RaycastDetection());
        }

        if (InputManager.performChangePlan && InputManager.inputAxis.y != 0)
        {
            CheckIndexPlan();
            ChangePlan();
        }

        Move(InputManager.inputAxis);
    }

    private IEnumerator RaycastDetection()
    {
        canBeDetectedByRaycast = false;
        yield return new WaitUntil(() => isGrounded == true);
        canBeDetectedByRaycast = true;
    }

    private void ChangePlan()
    {
        if (InputManager.inputAxis.y > 0)
            currentplan += 1;
        else if (InputManager.inputAxis.y < 0)
            currentplan -= 1;

        transform.position = new Vector3(transform.position.x, transform.position.y, planList[currentplan]);
    }

    private void CheckIndexPlan()
    {
        if (currentplan >= planList.Length)
            reversePlanIndex = true;
        else if (currentplan <= 0)
            reversePlanIndex = false;
    }
}

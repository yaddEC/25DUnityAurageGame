using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private PowerManager refPowerManager;
    public Station refStation;

    public float moveSpeed;

    public float dashSpeed;
    public float dashGravity;
    
    public float dashCooldown = 10f;
    public float cachedDashCooldown;
    public float dashPower = 10;

    //public bool canBeDetectedByRaycast = true;

    public Rigidbody playerRb;
    private Vector3 velocity = Vector3.zero;

    public LayerMask floorMask;

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
        MovementUpdate();

        if (!PlayerState.canDash) dashCooldown -= Time.deltaTime;

        if (dashCooldown <= 0)
        {
            dashCooldown = cachedDashCooldown;
            PlayerState.canDash = true;
        }

        ClampInMachine();
    }

    private void FixedUpdate()
    {
        if (!PlayerState.isInNodePath) MovementFixedUpdate();
    }

    //--------------------------------------------------
    private void MovementUpdate()
    {
        if (!PlayerState.isInNodePath) GroundCheck();

        else if (PlayerState.isInNodePath)
        {
            PlayerState.isInMachine = true;

            if (NodeSettings.canDashOnNode) DashCheck(true);
            else PlayerState.canDash = false;
        }
    }

    private void MovementFixedUpdate()
    {
        if (PlayerState.isGrounded) FloorMovement();
        else if (!PlayerState.isGrounded) playerRb.velocity += Vector3.down * dashGravity * Time.fixedDeltaTime;
    }

    private void Move(Vector2 input)
    {
        playerRb.velocity = new Vector3(input.x * moveSpeed, playerRb.velocity.y, input.y * moveSpeed) * Time.fixedDeltaTime;
    }

    private void Dash()
    {
        if(PlayerState.isGrounded && !PlayerState.isInMachine)
        {
            playerRb.AddForce(dashSpeed * Vector3.up);

            refPowerManager.currentPower -= dashPower;
            PlayerState.canDash = false;
        }
        else if(PlayerState.isInMachine)
        {
            if (InputManager.inputAxis != Vector2.zero) playerRb.AddForce(dashSpeed * 1.5f * InputManager.inputAxis);
        }

        PlayerState.isInMachine = false;
    }

    private void GroundCheck()
    {
        PlayerState.isGrounded = Physics.CheckSphere(transform.position + Vector3.down / 5, transform.localScale.x / 2, floorMask);
    }

    private void FloorMovement()
    {
        DashCheck(false);

        if (InputManager.inputAxis != Vector2.zero) Move(InputManager.inputAxis);
    }

    //--------------------------------------------------
    public void DashCheck(bool autoExec)
    {
        if(autoExec) Dash();
        else
        {
            if (InputManager.performA) 
                Dash();
        }
    }

    /*private IEnumerator RaycastDetection()
    {
        Debug.Log("StartedRaycastDetection");
        //playerRb.useGravity = true; 
        isInPath = false;
        canBeDetectedByRaycast = false;
        yield return new WaitForSeconds(0.1f);
        canBeDetectedByRaycast = true;
    }*/
    public void ClampInMachine()
    {
        if (PlayerState.isInMachine && refStation != null)
            transform.position = refStation.lockPosition.position;
    }
}
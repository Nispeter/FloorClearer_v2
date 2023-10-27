using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_3 : MonoBehaviour
{
    
    private float _lastJumpTime;

    public float moveSpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float floorDrag = 6f;
    public float playerHeight = 1.5f;
    public float gravity = 9.81f;
    public float maxSlopeAngle = 45f;

    public const int defaultAirJumps = 1;
    [SerializeField] private int _remainingAirJumps = defaultAirJumps;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    public KeyCode jumpKey = KeyCode.Space;

    public LayerMask ground;
    private bool grounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private float _currentMaxSpeed;

    private void Start()
    {
        _lastJumpTime = jumpCooldown;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        walkSpeed = moveSpeed;
        sprintSpeed = moveSpeed * 2f;
        _currentMaxSpeed = walkSpeed;
    }

    private void Update()
    {
        GroundCheck();
        MyInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GroundCheck()
    {
        RaycastHit groundHit;
        grounded = Physics.Raycast(transform.position, Vector3.down, out groundHit , playerHeight, ground);
        RaycastVisualization.VisualizeRaycast(transform.position, Vector3.down, playerHeight);

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && _remainingAirJumps > 0)
        {
            _remainingAirJumps--;
            Jump();
            _lastJumpTime = Time.time;
        }

        if (!grounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration); // Apply negative acceleration to simulate falling
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentMaxSpeed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentMaxSpeed = walkSpeed * 0.5f;
        }
        else
        {
            _currentMaxSpeed = walkSpeed;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            _remainingAirJumps = defaultAirJumps;
            rb.AddForce(moveDirection.normalized * moveSpeed * 15f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (grounded)
        {
            rb.drag = floorDrag; // Apply floor drag when grounded
        }
        else
        {
            rb.drag = 0f; // No drag when in the air
        }

        if (flatVel.magnitude > _currentMaxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _currentMaxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_2 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravity = -9.81f; // Gravity should be negative for the character to fall down

    public Transform orientation;
    public const int defaultAirJumps = 2;
    [SerializeField] private int _remainingAirJumps = defaultAirJumps;

    public float raycastHeight = 1.5f; // Height for the raycast
    public LayerMask groundLayer; // Layer mask for the ground

    private bool grounded;
    private CharacterController controller;
    private Vector3 playerVelocity; // To handle vertical velocity due to jumping and gravity

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundCheck();
        MyInput();
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, raycastHeight, groundLayer);
        
        RaycastVisualization.VisualizeRaycast(transform.position, Vector3.down, raycastHeight);

        if (grounded)
            _remainingAirJumps = defaultAirJumps;
    }

    private void MyInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && (_remainingAirJumps > 0 || grounded)) // Use GetButtonDown for Jump
        {
            Debug.Log("Jump");
            Jump();
        }

        if (!grounded)
        {
            // Apply gravity to the player's vertical velocity
            playerVelocity.y += gravity * Time.deltaTime;
        }
        else
        {
            _remainingAirJumps = defaultAirJumps;
            playerVelocity.y = 0f;
        }

        // Calculate movement only in the X-Z plane (horizontal plane)
        Vector3 moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Apply the player's overall velocity
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        grounded = false; // Player is no longer grounded after jumping
        playerVelocity.y = Mathf.Sqrt(2 * jumpForce * -gravity); // Calculate initial jump velocity
        _remainingAirJumps--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WARNING: discrepancia de nombre de archivo y clase, ya es muy tarde para cambiarlo y-y
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float groundRayLength = 0.2f;
    public int airJumps = 1;

    public Transform cameraTransform;
    private CharacterController controller;

    private Vector3 velocity;
    private float _defaultSpeed;
    private int _jumpsRemainig;
    public float maxYSpeed;
    public bool dashing;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
;
        _defaultSpeed = movementSpeed;
        _jumpsRemainig = airJumps;
    }

    public void HandleMovement(float horizontalInput, float verticalInput, bool isSprinting, bool isWalking)
    {
        Vector3 moveDirection = (cameraTransform.forward.normalized * verticalInput + cameraTransform.right.normalized * horizontalInput);
        moveDirection.y = 0f;

        if (isSprinting)
        {
            movementSpeed = 2f * _defaultSpeed;
        }
        else if (isWalking)
        {
            movementSpeed = 0.5f * _defaultSpeed;
        }
        else
        {
            movementSpeed = _defaultSpeed;
        }
        Vector3 moveVelocity = moveDirection.normalized * movementSpeed;

        ApplyGravity();

        controller.Move((moveVelocity + velocity) * Time.deltaTime);
    }

    public void HandleJump()
    {

        if (IsGrounded())
        {
            _jumpsRemainig = airJumps;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        else if (_jumpsRemainig > 0)
        {
            _jumpsRemainig--;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }


    }

    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            if (velocity.y < 0)
            {
                velocity.y = -1f;
            }
        }
    }



    private bool IsGrounded()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + controller.center;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundRayLength))
        {
            return true;
        }

        return false;
    }
}

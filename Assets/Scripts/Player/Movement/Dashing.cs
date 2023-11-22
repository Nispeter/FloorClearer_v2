using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private CharacterController characterController;
    private FirstPersonMovement fpm;

    [Header("Dashing")]
    public float dashSpeed;
    public float dashDuration;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    private Vector3 dashDirection;
    private bool isDashing = false;
    private float currentDashTime = 0;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        fpm = GetComponent<FirstPersonMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey) && dashCdTimer <= 0)
        {
            StartDash();
        }

        if (isDashing)
        {
            Dash();
        }

        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void StartDash()
    {
        dashCdTimer = dashCd;
        fpm.dashing = true;

        Transform forwardT = useCameraForward ? playerCam : orientation;
        dashDirection = GetDirection(forwardT) * dashSpeed;
        currentDashTime = dashDuration;
        isDashing = true;
    }

    private void Dash()
    {
        if (currentDashTime > 0)
        {
            characterController.Move(dashDirection * Time.deltaTime);
            currentDashTime -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
            fpm.dashing = false;
        }
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }
}

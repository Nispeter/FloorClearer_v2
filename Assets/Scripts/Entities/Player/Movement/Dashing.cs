using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;
    public float dashDistance = 5f; // Distance the dash should cover
    public float dashTime = .2f;      // Duration of the dash
    public Camera playerCamera;
    public float normalFOV = 60f;    // Normal field of view
    public float dashFOV = 70f;      // Increased field of view during dash

    private Vector3 dashDirection;
    public bool isDashing;
    private float dashStartTime;
    private float dashSpeed;         // Calculated based on distance and time

    void Start()
    {
        // Calculate dash speed based on distance and time
        dashSpeed = dashDistance / dashTime;
    }

    public void StartDash()
    {
        if (isDashing) return; // Prevent restarting dash if already dashing

        isDashing = true;
        dashDirection = transform.forward; // Dash in the direction the player is facing
        dashStartTime = Time.time;
        playerCamera.fieldOfView = dashFOV; // Increase FOV
    }

    void Update()
    {
        if (isDashing)
        {
            ContinueDash();
        }
    }

    public void ContinueDash()
    {
        if (Time.time < dashStartTime + dashTime)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
        }
        else
        {
            EndDash();
        }
    }

    private void EndDash()
    {
        isDashing = false;
        playerCamera.fieldOfView = normalFOV; // Reset FOV
    }
}

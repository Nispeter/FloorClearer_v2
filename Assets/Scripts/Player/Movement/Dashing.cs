using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("movement")]
    public CharacterController controller;
    public float dashDistance = 5f;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public Camera playerCamera;
    public float normalFOV = 60f;      // Normal field of view
    public float dashFOV = 80f;        // Increased field of view during dash

    private Vector3 dashDirection;
    public bool isDashing;
    private float dashStartTime;

    public void StartDash()
    {
        if (isDashing) return; // Prevent restarting dash if already dashing

        isDashing = true;
        dashDirection = transform.forward; // Dash in the direction the player is facing
        dashStartTime = Time.time;
        playerCamera.fieldOfView = dashFOV; // Increase FOV
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

    public void EndDash()
    {
        isDashing = false;
        playerCamera.fieldOfView = normalFOV; // Reset FOV
    }
}

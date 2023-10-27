using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAnimations : MonoBehaviour
{
    public Transform cameraTransform;
    public float idleHoverSpeed = 1f;
    public float idleHoverAmount = 0.1f;
    public float walkSpeedModifier = 1f;
    public float walkAmount = 0.1f;
    public float jumpAmount = 0.5f;
    public float shootRecoilAmount = 0.3f;
    public float shootingRecoilSpeed = 5f;

    private Vector3 initialPosition;
    private float time;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        FollowCameraRotation();
        HandleIdleAnimation();
        HandleWalkingAnimation();
        HandleJumpingAnimation();

        if (Input.GetMouseButtonDown(0)) 
        {
            HandleShootingAnimation();
        }
    }

    void FollowCameraRotation()
    {
        Vector3 currentAngles = transform.localEulerAngles;
        currentAngles.x = -cameraTransform.localEulerAngles.x; 
        transform.localEulerAngles = currentAngles;
    }

    void HandleIdleAnimation()
    {
        float hoverOffset = Mathf.Sin(time * idleHoverSpeed) * idleHoverAmount;
        Vector3 idlePosition = initialPosition + new Vector3(0, hoverOffset, 0);
        transform.localPosition = idlePosition;
        time += Time.deltaTime;
    }

    void HandleWalkingAnimation()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            float offset = Mathf.Sin(Time.time * walkSpeedModifier) * walkAmount;
            transform.localPosition += new Vector3(offset, 0, offset);
        }
    }

    void HandleJumpingAnimation()
    {
        if (Input.GetButtonDown("Jump"))
        {
            transform.localPosition += new Vector3(0, jumpAmount, 0);
        }
    }

    void HandleShootingAnimation()
    {
        Vector3 shootPosition = transform.localPosition - new Vector3(0, shootRecoilAmount, shootRecoilAmount);
        transform.localPosition = Vector3.Lerp(transform.localPosition, shootPosition, shootingRecoilSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBall : MonoBehaviour
{
    public Camera caster;
    public float ballNumber;
    public Element elementType;
    [SerializeField] private float _spacing = 0.15f;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;

    private float _zOffset = 0.45f;
    private float _xOffset = .5f;

    public float lerpSpeed = 5f;
    public float floatAmplitude = 0.05f;
    public float floatFrequency = 1f;

    private float phaseShift;

    private ParticleSystem particleSystem; // Reference to the Particle System

    private void Start()
    {
        _initialPosition = caster.transform.position;
        _targetPosition = _initialPosition + Vector3.right * (_spacing * ballNumber);

        // Calculate a unique phase shift based on the ball's position
        phaseShift = transform.position.x * 0.1f; // Adjust this factor to control the phase shift

        // Get the Particle System component attached to the ball, if available
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        MoveElementalBall();
        RotateElementalBall();
        UpdateParticleSystemPosition();
    }

    private void MoveElementalBall()
    {
        Vector3 targetPosition = CalculateTargetPosition();
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.fixedDeltaTime);
        transform.position = newPosition;
    }

    private Vector3 CalculateTargetPosition()
    {
        Vector3 cameraForward = caster.transform.forward;
        Vector3 cameraRight = caster.transform.right;
        Vector3 cameraUp = caster.transform.up;

        Vector3 initialPosition = caster.transform.position + cameraRight * (_spacing * (ballNumber - 1));
        Vector3 targetPosition = initialPosition + cameraForward + cameraUp * _zOffset;

        float oscillation = Mathf.Sin((Time.time + phaseShift) * floatFrequency) * floatAmplitude;
        return targetPosition + cameraUp * oscillation;
    }

    private void RotateElementalBall()
    {
        Quaternion newRotation = Quaternion.LookRotation(caster.transform.forward, Vector3.up);
        transform.rotation = newRotation;
    }

    private void UpdateParticleSystemPosition()
    {
        if (particleSystem != null)
        {
            particleSystem.transform.position = transform.position;
        }
    }
}

using System;
using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    // Actions
    public static Action<float, float> ChangeVector;
    public static Action OnRelease;
    public static Action OnRush;
    [Header("Arrow Settings")]
    public float AccelerationFactor;
    public float DriftFactor;
    public float TurnFactor;
    public float MaxSpeed;
    // Components
    private Rigidbody arrowRigidBody;
    // Local variables
    private float accelerationInput = 0;
    private float rotationInput = 0;
    
    private float rotationAngle = 0;
    
    private float velocityVsUp;
    private bool stop;

    private void Awake()
    {
        arrowRigidBody = GetComponent<Rigidbody>();
        
        ChangeVector += ChangeInputVector;
        OnRelease += OnReleaseRestart;
    }

    private void OnDestroy()
    {
        ChangeVector -= ChangeInputVector;
        OnRelease -= OnReleaseRestart;
    }

    private void ChangeInputVector(float vertical, float horizontal)
    {
        accelerationInput = vertical;
        rotationInput = horizontal;
        stop = false;
    }

    private void OnReleaseRestart()
    {
        stop = true;
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        KillVelocity();
        ApplySteering();
    }

    private void ApplyEngineForce()
    {
        velocityVsUp = Vector3.Dot(transform.up, arrowRigidBody.velocity);
        
        if (velocityVsUp > MaxSpeed && accelerationInput > 0)
            return;
        
        if (velocityVsUp < -MaxSpeed * 0.5f && accelerationInput < 0)
            return;
        
        if (arrowRigidBody.velocity.sqrMagnitude > MaxSpeed * MaxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0)
            arrowRigidBody.drag = Mathf.Lerp(arrowRigidBody.drag, 3.0f, Time.fixedDeltaTime);
        else
            arrowRigidBody.drag = 0;
        
        var force = new Vector3(rotationInput, 0, accelerationInput) * AccelerationFactor;
        
        arrowRigidBody.AddForce(force, ForceMode.Force);
    }

    private void ApplySteering()
    {
        if (stop)
            return;

        var h = rotationInput;
        var v = accelerationInput;
        rotationAngle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
        
        var moveRotation = Quaternion.Euler(90, 0, -rotationAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, Time.deltaTime * TurnFactor);
    }
    

    private void KillVelocity()
    {
        var forwardVelocity = transform.up * Vector3.Dot(arrowRigidBody.velocity, transform.up);
        var rightVelocity = transform.right * Vector3.Dot(arrowRigidBody.velocity, transform.right);
        
        arrowRigidBody.velocity = forwardVelocity + rightVelocity * DriftFactor;
    }
}
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetCamera;
    public float smoothSpeed = 0.125f;
    
    private Vector3 desiredPosition;

    private void FixedUpdate()
    {
        desiredPosition = target.position + offsetCamera;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition; 
    }
}

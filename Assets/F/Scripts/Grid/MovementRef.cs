using UnityEngine;

public class MovementRef : MonoBehaviour
{
    public Transform cameraTransform;  // Reference to the camera

    void Update()
    {
        // Get the camera's forward direction, but lock it to the XZ plane by zeroing out the Y component
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;  // Ensure we only rotate on the XZ plane

        // Normalize the vector to avoid dealing with different magnitudes
        cameraForward.Normalize();

        // Determine the direction by finding which axis the camera is closest to
        Vector3 snappedDirection = GetClosestAxisDirection(cameraForward);

        // Set the object's forward direction to the calculated snapped direction
        transform.forward = snappedDirection;
    }

    // This method returns the closest axis-aligned direction (x+, x-, z+, z-)
    Vector3 GetClosestAxisDirection(Vector3 direction)
    {
        // Determine the absolute values of the direction components
        float absX = Mathf.Abs(direction.x);
        float absZ = Mathf.Abs(direction.z);

        // Snap to the closest axis-aligned direction
        if (absX > absZ)
        {
            // If x component is larger, snap to x+ or x-
            return direction.x > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            // If z component is larger, snap to z+ or z-
            return direction.z > 0 ? Vector3.forward : Vector3.back;
        }
    }
}
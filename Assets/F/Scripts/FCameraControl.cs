using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCameraControl : MonoBehaviour
{
    public Transform target1;   // The object the camera follows, rotates around, and zooms on
    public Transform target2;
    public float zoomSpeed = 10f;  // Speed of zooming in/out
    public float followSpeed = 5f; // Speed of following the target
    public float rotationSpeed = 100f; // Speed of camera rotation
    public float minDistance = 5f; // Minimum zoom distance
    public float maxDistance = 50f; // Maximum zoom distance
    public float scrollSensitivity = 1f; // Sensitivity of the scroll wheel

    private float currentDistance; // Current distance from the target
    private float yaw; // Horizontal angle (left/right rotation)
    private float pitch; // Vertical angle (up/down rotation)

    public GridBehavior gb;

    void Start()
    {
        // Calculate the initial distance between the camera and the target
        currentDistance = Vector3.Distance(transform.position, target1.position);

        // Set initial yaw and pitch angles based on current camera rotation
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        // Handle camera zoom based on scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        currentDistance -= scrollInput * zoomSpeed;

        // Clamp the zoom distance between min and max
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Handle camera rotation using right mouse button
        if (Input.GetMouseButton(1)) // Right mouse button is held
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime; // Horizontal rotation
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime; // Vertical rotation

            // Clamp pitch angle to avoid flipping the camera (limit vertical rotation)
            pitch = Mathf.Clamp(pitch, -60f, 60f);
        }

        // Calculate the new position of the camera based on the yaw, pitch, and current zoom distance
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0); // Camera rotation
        Vector3 direction = rotation * Vector3.back; // Direction away from the target
        Vector3 desiredPosition;
        if(gb.isPlayer1Turn)
            desiredPosition = target1.position + direction * currentDistance;
        else
            desiredPosition = target2.position + direction * currentDistance;
        // Smoothly update the camera's position to follow and rotate around the target
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Make the camera look at the target
        if(gb.isPlayer1Turn)
            transform.LookAt(target1);
        else
            transform.LookAt(target2);
    }
}

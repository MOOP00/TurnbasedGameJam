using UnityEngine;
using System.Collections;

public class CameraControllerTurnBased : MonoBehaviour
{
    public Transform player1Transform;  // ตำแหน่งผู้เล่น 1
    public Transform player2Transform;  // ตำแหน่งผู้เล่น 2
    public Vector3 fixedPosition;       // ตำแหน่งกล้องที่นิ่ง
    public Vector3 fixedRotation;       // มุมกล้องที่นิ่ง
    public float rotationSpeed = 50f;   // ความเร็วในการหมุนรอบ
    public float orbitDuration = 3f;    // ระยะเวลาที่กล้องจะหมุนรอบผู้เล่น
    public float orbitDistance = 10f;   // ระยะห่างระหว่างกล้องและจุดกึ่งกลางระหว่างผู้เล่น
    public float transitionDuration = 2f;  // ระยะเวลาที่ใช้ในการเคลื่อนกล้องกลับไปยังจุดที่ตั้งไว้

    private bool isOrbiting = false;

    void Start()
    {
        if (player1Transform != null && player2Transform != null)
        {
            StartCameraOrbit();
        }
        else
        {
            Debug.LogError("ผู้เล่นยังไม่ได้ถูกกำหนดค่า!");
        }
    }

    public void StartCameraOrbit()
    {
        if (!isOrbiting)
        {
            StartCoroutine(OrbitAroundPlayers());
        }
    }

    private IEnumerator OrbitAroundPlayers()
    {
        isOrbiting = true;

        // คำนวณจุดกลางระหว่างผู้เล่น 1 และผู้เล่น 2
        Vector3 middlePoint = (player1Transform.position + player2Transform.position) / 2;
        Vector3 directionToMiddle = (transform.position - middlePoint).normalized;
        Vector3 orbitStartPosition = middlePoint + directionToMiddle * orbitDistance;

        float elapsedTime = 0f;

        while (elapsedTime < orbitDuration)
        {
            elapsedTime += Time.deltaTime;

            // หมุนกล้องรอบจุดกลาง
            transform.RotateAround(middlePoint, Vector3.up, rotationSpeed * Time.deltaTime);

            // รักษาระยะห่างคงที่จากจุดกลาง
            directionToMiddle = (transform.position - middlePoint).normalized;
            transform.position = middlePoint + directionToMiddle * orbitDistance;

            // หันกล้องไปทางจุดกลาง
            transform.LookAt(middlePoint);

            yield return null;
        }

        // หลังจากหมุนครบเวลาแล้ว กล้องจะค่อยๆ กลับไปยังตำแหน่งคงที่
        yield return StartCoroutine(MoveToFixedPosition());
        isOrbiting = false;
    }

    private IEnumerator MoveToFixedPosition()
    {
        // ตำแหน่งและมุมหมุนเริ่มต้นของกล้อง
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, fixedPosition, elapsedTime / transitionDuration);
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(fixedRotation), elapsedTime / transitionDuration);

            yield return null;
        }

        // กล้องจะอยู่ในตำแหน่งและมุมที่กำหนดไว้
        transform.position = fixedPosition;
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}
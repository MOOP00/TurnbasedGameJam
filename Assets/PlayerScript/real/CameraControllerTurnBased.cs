using UnityEngine;
using System.Collections;

public class CameraControllerTurnBased : MonoBehaviour
{
    private PlayerController playerController;    // ตัวควบคุมผู้เล่น
    public Transform enemyTransform;             // ตำแหน่งของศัตรู
    public Vector3 fixedPosition;                // ตำแหน่งกล้องที่ต้องการ (นิ่ง)
    public Vector3 fixedRotation;                // มุมกล้องที่ต้องการ (นิ่ง)
    public float rotationSpeed = 50f;            // ความเร็วการหมุนรอบ
    public float orbitDuration = 3f;             // ระยะเวลาที่กล้องจะหมุนรอบ
    public float orbitDistance = 10f;            // ระยะห่างระหว่างกล้องกับจุดกลางระหว่างผู้เล่นและศัตรู
    public float transitionDuration = 2f;        // ระยะเวลาที่ใช้ในการเคลื่อนกล้องกลับไปยังตำแหน่งคงที่
    private bool isOrbiting = false;             // ตัวเช็คสถานะว่ากล้องกำลังหมุนอยู่หรือไม่

    void Start()
    {
        // ค้นหาผู้เล่นที่ถูกส่งต่อมาจาก Scene ก่อนหน้า (ใช้ DontDestroyOnLoad)
        playerController = FindObjectOfType<PlayerController>();

        if (playerController != null && enemyTransform != null)
        {
            // เริ่มการหมุนกล้องรอบจุดกลางระหว่างผู้เล่นและศัตรู
            StartCameraOrbit();
        }
        else
        {
            Debug.LogError("Player or Enemy not found!");
        }
    }

    public void StartCameraOrbit()
    {
        if (!isOrbiting && playerController != null && enemyTransform != null) // หากยังไม่มีการหมุน ให้เริ่มหมุน
        {
            StartCoroutine(OrbitAroundTarget());
        }
    }

    private IEnumerator OrbitAroundTarget()
    {
        isOrbiting = true;

        // คำนวณจุดกลางระหว่างผู้เล่นและศัตรู
        Vector3 middlePoint = (playerController.transform.position + enemyTransform.position) / 2;

        // คำนวณทิศทางจากกล้องไปยังจุดกลางระหว่างผู้เล่นและศัตรู
        Vector3 directionToMiddle = (transform.position - middlePoint).normalized;
        Vector3 orbitStartPosition = middlePoint + directionToMiddle * orbitDistance;

        float elapsedTime = 0f;

        // หมุนรอบจุดกลางระหว่างผู้เล่นและศัตรูเป็นเวลาที่กำหนด
        while (elapsedTime < orbitDuration)
        {
            elapsedTime += Time.deltaTime;

            // หมุนกล้องรอบจุดกลาง
            transform.RotateAround(middlePoint, Vector3.up, rotationSpeed * Time.deltaTime);

            // รักษาระยะห่างคงที่จากจุดกลาง
            directionToMiddle = (transform.position - middlePoint).normalized;
            transform.position = middlePoint + directionToMiddle * orbitDistance;

            // หันกล้องให้มองไปที่จุดกลาง
            transform.LookAt(middlePoint);

            yield return null;
        }

        // หลังจากหมุนครบเวลาแล้ว กล้องจะค่อยๆ เคลื่อนกลับไปยังตำแหน่งคงที่
        yield return StartCoroutine(MoveToFixedPosition());
        isOrbiting = false;
    }

    private IEnumerator MoveToFixedPosition()
    {
        // เก็บตำแหน่งเริ่มต้นและมุมหมุนเริ่มต้นของกล้อง
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float elapsedTime = 0f;

        // เคลื่อนกล้องจากตำแหน่งปัจจุบันไปยังตำแหน่งคงที่
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            // ค่อยๆ เลื่อนตำแหน่งกล้องและหมุนกล้องให้กลับไปยังตำแหน่งและมุมคงที่
            transform.position = Vector3.Lerp(startPosition, fixedPosition, elapsedTime / transitionDuration);
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(fixedRotation), elapsedTime / transitionDuration);

            yield return null;
        }

        // ทำให้แน่ใจว่ากล้องจะอยู่ที่ตำแหน่งคงที่
        transform.position = fixedPosition;
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}
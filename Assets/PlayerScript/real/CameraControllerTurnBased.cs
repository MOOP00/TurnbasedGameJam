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

        // หลังจากหมุนครบเวลาแล้ว กลับไปที่ตำแหน่งคงที่
        SetFixedPosition();
        isOrbiting = false;
    }

    private void SetFixedPosition()
    {
        // ตั้งค่าตำแหน่งและมุมกล้องคงที่
        transform.position = fixedPosition;
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}
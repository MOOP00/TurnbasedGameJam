using UnityEngine;
using System.Collections;

public class CameraControllerTurnBased : MonoBehaviour
{
    private PlayerController playerController;    // ตัวควบคุมผู้เล่น
    public Transform enemyTransform;             // ตำแหน่งของศัตรู
    public Vector3 fixedPosition;                // ตำแหน่งกล้องที่ต้องการ (นิ่ง)
    public Vector3 fixedRotation;                // มุมกล้องที่ต้องการ (นิ่ง)
    public float fadeDuration = 2f;              // ระยะเวลาการ Fade
    private bool isFading = false;               // ตัวเช็คสถานะการ Fade

    void Start()
    {
        // ตั้งค่ากล้องไปยังตำแหน่งที่คงที่ทันที
        transform.position = fixedPosition;
        transform.rotation = Quaternion.Euler(fixedRotation);

        // เริ่มทำการ Fade ทันทีเมื่อ Scene เริ่ม
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        isFading = true;
        float elapsedTime = 0f;

        // ค่อยๆ ซีดจางจากดำไปสว่าง
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // ปรับโค้ด Fade ของคุณตามความต้องการได้ที่นี่
            yield return null;
        }

        isFading = false;
    }

    public void StartFadeOut()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0f;

        // ค่อยๆ ทำให้ภาพจางจากสว่างไปเป็นดำ
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // ปรับโค้ด Fade ของคุณตามความต้องการได้ที่นี่
            yield return null;
        }

        isFading = false;
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image blackScreen;  // ภาพสีดำที่ใช้สำหรับการซีดจาง
    public float fadeDuration = 1f;  // ระยะเวลาการซีดจาง
    public ParticleSystem warpEffect;  // Particle System สำหรับเอฟเฟกต์วาร์ป
    public float waitDuration = 2f;  // ระยะเวลารอในหน้าจอสีดำก่อนเปลี่ยน Scene
    public Camera mainCamera;  // กล้องหลักที่จะใช้ทำการซูม
    public float zoomDuration = 1f;  // ระยะเวลาการซูม
    public float zoomDistance = 10f;  // ระยะการซูมเข้า

    private bool enableZoom = true;  // เปิดใช้งานการซูม

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void ChangeSceneWithWarp(string sceneName)
    {
        StartCoroutine(WarpAndChangeScene(sceneName));
    }

    private IEnumerator WarpAndChangeScene(string sceneName)
    {
        // ซูมเข้าเพื่อสร้างเอฟเฟกต์การดูดเข้า หากเปิดการซูม
        if (enableZoom)
        {
            Debug.Log("Starting Zoom In");
            yield return StartCoroutine(ZoomIn());
        }

        // แสดงเอฟเฟกต์วาร์ป
        Debug.Log("Starting Warp Effect and Scene Transition...");
        StartWarpEffect();

        // ซีดจางหน้าจอเป็นสีดำ
        yield return StartCoroutine(FadeOut());

        // รอเป็นเวลา waitDuration ก่อนเปลี่ยน Scene
        yield return new WaitForSeconds(waitDuration);

        // เปลี่ยน Scene
        SceneManager.LoadScene(sceneName);

        // ซีดจางกลับมาเมื่อเข้าสู่ Scene ใหม่
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator ZoomIn()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned!");
            yield break;
        }

        Debug.Log("Zooming in...");

        // ตำแหน่งเริ่มต้นของกล้อง
        Vector3 startPosition = mainCamera.transform.position;

        // ตำแหน่งที่กล้องจะถูกซูมเข้าไป
        Vector3 targetPosition = startPosition + mainCamera.transform.forward * zoomDistance;

        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / zoomDuration);  // ซูมเข้าโดยการเลื่อนกล้อง
            yield return null;
        }

        Debug.Log("Zoom completed!");
    }

    private IEnumerator FadeOut()
    {
        blackScreen.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackScreen.color = new Color(0, 0, 0, Mathf.Clamp01(elapsedTime / fadeDuration));
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        blackScreen.color = new Color(0, 0, 0, 1);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackScreen.color = new Color(0, 0, 0, 1 - Mathf.Clamp01(elapsedTime / fadeDuration));
            yield return null;
        }

        blackScreen.gameObject.SetActive(false);  // ซ่อนภาพสีดำเมื่อซีดจางเสร็จ
    }

    private void StartWarpEffect()
    {
        // เล่น Particle System สำหรับเอฟเฟกต์วาร์ป
        if (warpEffect != null)
        {
            warpEffect.Play();
        }
        else
        {
            Debug.LogWarning("Warp Effect not assigned.");
        }
    }
}
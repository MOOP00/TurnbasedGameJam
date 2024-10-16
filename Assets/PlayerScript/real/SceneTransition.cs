using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image blackScreen;  // ภาพสีดำที่ใช้สำหรับการซีดจาง
    public float fadeDuration = 1f;  // ระยะเวลาการซีดจาง
    public ParticleSystem warpEffect;  // Particle System สำหรับเอฟเฟกต์วาร์ป

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
        // แสดงเอฟเฟกต์วาร์ป
        StartWarpEffect();

        // ซีดจางหน้าจอเป็นสีดำ
        yield return StartCoroutine(FadeOut());

        // เปลี่ยน Scene
        SceneManager.LoadScene(sceneName);

        // ซีดจางกลับมาเมื่อเข้าสู่ Scene ใหม่
        yield return StartCoroutine(FadeIn());
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

        blackScreen.gameObject.SetActive(false);
    }

    private void StartWarpEffect()
    {
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FCameraControl : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public float zoomSpeed = 10f;
    public float followSpeed = 5f;
    public float rotationSpeed = 100f;
    public float minDistance = 5f;
    public float maxDistance = 50f;
    public float scrollSensitivity = 1f;

    private float currentDistance;
    private float yaw;
    private float pitch;
    private Transform actualTarget;

    public TextMeshProUGUI mainText;

    public GridBehavior gb;

    public float fadeDuration = 1f;

    void Start()
    {
        currentDistance = Vector3.Distance(transform.position, target1.position);

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
        StartCoroutine(FadeTextInAndOut());

        if (gb == null)
        {
            Debug.LogError("GridBehavior is not assigned!");
        }
    }

    void Update()
    {
        if (target1 == null || target2 == null)
        {
            Debug.LogError("Targets are not assigned!");
            return;
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        currentDistance -= scrollInput * zoomSpeed;

        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -60f, 60f);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = rotation * Vector3.back;
        Vector3 desiredPosition = actualTarget.position + direction * currentDistance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.LookAt(actualTarget);

        if (gb.isPlayer1Turn)
        {
            actualTarget = target1;
        }
        else
        {
            actualTarget = target2;
        }
    }

    public IEnumerator FadeTextInAndOut()
    {
        yield return StartCoroutine(FadeInText());
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOutText());
    }

    public IEnumerator FadeInText()
    {
        float elapsedTime = 0f;
        Color textColor = mainText.color;
        textColor.a = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            mainText.color = textColor;
            yield return null;
        }
    }

    public IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        Color textColor = mainText.color;
        textColor.a = 1f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            mainText.color = textColor;
            yield return null;
        }
    }
}
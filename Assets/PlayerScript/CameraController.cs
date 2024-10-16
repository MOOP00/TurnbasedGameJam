using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offsetZ = -14f;  // ระยะห่างของกล้องจากผู้เล่นในแนว Z (ด้านหลัง)
    public float offsetY = 10f;   // ระยะความสูงของกล้องจากผู้เล่น
    public float smoothing = 2f;  // ความนุ่มนวลในการเคลื่อนกล้อง

    // Player transform component
    Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player game object in the scene and get its transform component
        playerPos = FindFirstObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    // Following the player
    void FollowPlayer()
    {
        // ตำแหน่งที่กล้องควรจะอยู่ (ด้านหลังและสูงกว่าผู้เล่น)
        Vector3 targetPosition = new Vector3(playerPos.position.x, playerPos.position.y + offsetY, playerPos.position.z + offsetZ);

        // เคลื่อนกล้องอย่างนุ่มนวลไปยังตำแหน่งที่ต้องการ
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);

        // หันกล้องไปทางผู้เล่น
        transform.LookAt(playerPos);
    }
}
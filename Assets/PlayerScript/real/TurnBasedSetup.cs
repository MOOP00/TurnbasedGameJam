using UnityEngine;

public class TurnBasedSetup : MonoBehaviour
{
    void Start()
    {
        // หาผู้เล่นใน Scene ปัจจุบันแล้วเรียกใช้ฟังก์ชัน EnterTurnBasedMode
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.EnterTurnBasedMode(); // ปิดการขยับของผู้เล่นในโหมด Turn-Based
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }

        // คุณสามารถเพิ่มระบบ Turn-Based อื่นๆ ที่นี่
    }
}
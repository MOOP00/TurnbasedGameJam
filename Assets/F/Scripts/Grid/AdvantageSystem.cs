using TMPro;
using UnityEngine;

public class AdvantageSystem : MonoBehaviour
{
    public static int Attack1 = 10;  // Reroll points for Player 1
    public static int Attack2 = 10;  // Reroll points for Player 2

    public static int health1 = 100; // Health for Player 1
    public static int health2 = 100; // Health for Player 2

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    void Awake()
    {
        // โหลดค่าความเสียหายจาก PlayerPrefs เมื่อเริ่ม
        //health1 = PlayerPrefs.GetInt("Player1Health", 100);
        //health2 = PlayerPrefs.GetInt("Player2Health", 100);
        SetText();
    }

    void Update()
    {
        SetText();
    }

    // ฟังก์ชันรีเซ็ตสุขภาพให้เป็น 100
    public void ResetHealth()
    {
        health1 = 100;  // Reset Player 1 health to 100
        health2 = 100;  // Reset Player 2 health to 100
        SetText();      // Update the UI
    }

    public void ModifyAttack(bool isPlayer1, int amount)
    {
        if (isPlayer1)
        {
            Attack1 += amount;
        }
        else
        {
            Attack2 += amount;
        }
    }

    public void ModifyHealth(bool isPlayer1, int amount)
    {
        if (isPlayer1)
        {
            health1 += amount;
        }
        else
        {
            health2 += amount;
        }

        // Update the UI text to reflect the change
        SetText();
    }

    public void SetText()
    {
        text1.text = $"Player 1\nHealth: {health1}\nAttack: {Attack1}";
        text2.text = $"Player 2\nHealth: {health2}\nAttack: {Attack2}";
    }

    // ใช้เพื่อรีเซ็ตสุขภาพเมื่อหยุดเล่น
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Player1Health", 100); // บันทึกสุขภาพ Player 1 เป็น 100
        PlayerPrefs.SetInt("Player2Health", 100); // บันทึกสุขภาพ Player 2 เป็น 100
    }
}
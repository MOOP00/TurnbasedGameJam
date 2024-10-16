using UnityEngine;
using TMPro; // TextMeshPro

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI player1HealthText;
    public TextMeshProUGUI player2HealthText;
    public AdvantageSystem advantageSystem;

    void Start()
    {
        // ตรวจสอบว่าเราอยู่ใน Scene 2 เท่านั้น
        if (advantageSystem != null)
        {
            UpdateHealthText();
        }
        else
        {
            // หากไม่อยู่ใน Scene 2 ให้ซ่อน Text ทั้งหมด
            player1HealthText.gameObject.SetActive(false);
            player2HealthText.gameObject.SetActive(false);
        }
    }

    void UpdateHealthText()
    {
        if (advantageSystem != null)
        {
            player1HealthText.text = "Player 1\nHealth: " + advantageSystem.health1;
            player2HealthText.text = "Player 2\nHealth: " + advantageSystem.health2;
        }
    }
}
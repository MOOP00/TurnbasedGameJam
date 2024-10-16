using TMPro;
using UnityEngine;

public class AdvantageSystem : MonoBehaviour
{
    public int player1Reroll = 0;  // Reroll points for Player 1
    public int player2Reroll = 0;  // Reroll points for Player 2
    public int player1Dice = 0;  // Dice points for Player 1
    public int player2Dice = 0;  // Dice points for Player 2

    public int health1 = 100; // Health for Player 1
    public int health2 = 100; // Health for Player 2

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    void Start()
    {
        // โหลดค่าความเสียหายจาก PlayerPrefs เมื่อเริ่ม
        health1 = PlayerPrefs.GetInt("Player1Health", 100);
        health2 = PlayerPrefs.GetInt("Player2Health", 100);
        SetText();
    }

    void Update()
    {
        SetText();
    }

    public void ModifyReroll(bool isPlayer1, int amount)
    {
        if (isPlayer1)
        {
            player1Reroll += amount;
        }
        else
        {
            player2Reroll += amount;
        }
    }

    public void ModifyDice(bool isPlayer1, int amount)
    {
        if (isPlayer1)
        {
            player1Dice += amount;
        }
        else
        {
            player2Dice += amount;
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
        text1.text = $"Player 1\nHealth: {health1}";
        text2.text = $"Player 2\nHealth: {health2}";
    }
}
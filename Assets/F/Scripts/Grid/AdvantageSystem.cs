using TMPro;
using UnityEngine;

public class AdvantageSystem : MonoBehaviour
{
    public static int Attack1 = 10;  // Attack points for Player 1
    public static int Attack2 = 10;  // Attack points for Player 2

    public static int health1 = 15; // Health for Player 1
    public static int health2 = 15; // Health for Player 2

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    void Awake()
    {
        SetText(); // Update the health text initially
    }

    void Update()
    {
        SetText(); // Update the health text continuously
    }

    public void ResetHealth()
    {
        health1 = 15;  // Reset Player 1 health to 15
        health2 = 15;  // Reset Player 2 health to 15
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

        SetText(); // Update the UI text to reflect the change
    }

    public void SetText()
    {
        text1.text = $"Player 1\nHealth: {health1}\nAttack: {Attack1}";
        text2.text = $"Player 2\nHealth: {health2}\nAttack: {Attack2}";
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Player1Health", 15); // Save Player 1 health to 15
        PlayerPrefs.SetInt("Player2Health", 15); // Save Player 2 health to 15
    }
}
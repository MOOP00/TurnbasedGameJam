using TMPro;
using UnityEngine;

public class AdvantageSystem : MonoBehaviour
{
    public int player1Reroll = 0;
    public int player2Reroll = 0;
    public int player1Dice = 0;
    public int player2Dice = 0;

    public int health1 = 100;
    public int health2 = 100;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

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
    }

    public void SetText()
    {
        text1.text = "Player1\nHealth: " + health1.ToString();
        text2.text = "Player2\nHealth: " + health2.ToString();
    }
}
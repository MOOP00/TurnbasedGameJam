using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvantageSystem : MonoBehaviour
{
    public int player1Advantage = 0;  // Advantage points for Player 1
    public int player2Advantage = 0;  // Advantage points for Player 2

    public void ModifyAdvantage(bool isPlayer1, int amount)
    {
        if (isPlayer1)
        {
            player1Advantage += amount;
            Debug.Log("Player 1 Advantage: " + player1Advantage);
        }
        else
        {
            player2Advantage += amount;
            Debug.Log("Player 2 Advantage: " + player2Advantage);
        }
    }

    // Call this to get advantage points for the current player
    public int GetCurrentPlayerAdvantage(bool isPlayer1)
    {
        return isPlayer1 ? player1Advantage : player2Advantage;
    }
}

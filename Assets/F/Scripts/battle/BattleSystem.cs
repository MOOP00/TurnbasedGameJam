using System.Collections;
using TMPro;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public FDiceFaceChecker player1Dice;
    public FDiceFaceChecker player2Dice;
    public AdvantageSystem advantageSystem;
    public TextMeshProUGUI player1RollText;
    public TextMeshProUGUI player2RollText;
    public TextMeshProUGUI battleResultText;
    public string returnSceneName;
    public float battleDuration = 3f; // Duration of the battle
    public float delayBeforeResult = 2f; // Delay before showing the result
    public float delayBeforeReturn = 2f; // Delay before returning to the previous scene

    private bool battleStarted = false; // Variable to check if the battle has started

    void Update()
    {
        // Check if the space bar has been pressed
        if (!battleStarted && Input.GetKeyDown(KeyCode.Space))
        {
            battleStarted = true;
            StartCoroutine(ExecuteBattle());
        }
    }

    private IEnumerator ExecuteBattle()
    {
        // Roll the dice for each player
        player1Dice.ThrowObject();
        player2Dice.ThrowObject();

        // Wait until the dice have stopped rolling
        yield return new WaitForSeconds(battleDuration);

        // Get the values rolled by Player 1 and Player 2
        int player1Roll = player1Dice.value;
        int player2Roll = player2Dice.value;

        // Display the results of the rolls
        player1RollText.text = "Player 1 Roll: " + player1Roll;
        player2RollText.text = "Player 2 Roll: " + player2Roll;

        // Wait a moment before calculating the battle result
        yield return new WaitForSeconds(delayBeforeResult);

        // Determine the outcome of the battle
        if (player1Roll > player2Roll)
        {
            advantageSystem.ModifyHealth(false, -AdvantageSystem.Attack1); // Deal damage to Player 2
            battleResultText.text = "Player 1 wins the round!";
        }
        else if (player2Roll > player1Roll)
        {
            advantageSystem.ModifyHealth(true, -AdvantageSystem.Attack2); // Deal damage to Player 1
            battleResultText.text = "Player 2 wins the round!";
        }
        else
        {
            battleResultText.text = "It's a draw!";
        }

        // Check if either player's health is 0 or less to transition to the ending scene
        if (AdvantageSystem.health1 <= 0 || AdvantageSystem.health2 <= 0)
        {
            // Start the ending scene transition
            yield return StartCoroutine(SceneTransitionToEnding());
        }
        else
        {
            // Wait before returning to the previous scene
            yield return new WaitForSeconds(delayBeforeReturn);
            PlayerPrefs.SetInt("Player1Health", AdvantageSystem.health1); // Save Player 1 health
            PlayerPrefs.SetInt("Player2Health", AdvantageSystem.health2); // Save Player 2 health
            UnityEngine.SceneManagement.SceneManager.LoadScene(returnSceneName); // Return to the previous scene
        }
    }

    private IEnumerator SceneTransitionToEnding()
    {
        // This is where you would implement the transition effect before going to the ending scene
        yield return new WaitForSeconds(1f); // Simulate waiting for an effect (optional)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ending"); // Load the ending scene
    }
}
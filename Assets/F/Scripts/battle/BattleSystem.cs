using System.Collections;
using UnityEngine;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    public FRandomThrow player1DiceThrower;
    public FRandomThrow player2DiceThrower;

    public FDiceFaceChecker player1Dice;
    public FDiceFaceChecker player2Dice;

    public AdvantageSystem advantageSystem;

    public TextMeshProUGUI player1RollText;
    public TextMeshProUGUI player2RollText;
    public TextMeshProUGUI battleResultText;

    public string returnSceneName;
    public float battleDuration = 3f;
    public float delayBeforeResult = 2f;
    public float delayBeforeReturn = 2f;

    void Start()
    {
        StartCoroutine(ExecuteBattle());
    }

    private IEnumerator ExecuteBattle()
    {
        // ทอยลูกเต๋าทั้งสอง
        player1DiceThrower.ThrowObject();
        player2DiceThrower.ThrowObject();

        yield return new WaitForSeconds(battleDuration);

        // รอจนกระทั่งลูกเต๋าหยุด
        yield return new WaitUntil(() => player1Dice.moving == false && player2Dice.moving == false);

        // แสดงผลการทอยลูกเต๋า
        player1RollText.text = "Player 1 rolled: " + player1Dice.value;
        player2RollText.text = "Player 2 rolled: " + player2Dice.value;

        // คำนวณผลลัพธ์
        int damage = player1Dice.value - player2Dice.value;

        if (damage > 0)
        {
            advantageSystem.ModifyHealth(false, -damage); // ลดพลังชีวิตของ Player 2
            battleResultText.text = "Player 1 wins! Player 2 takes " + damage + " damage.";
        }
        else if (damage < 0)
        {
            advantageSystem.ModifyHealth(true, damage); // ลดพลังชีวิตของ Player 1
            battleResultText.text = "Player 2 wins! Player 1 takes " + Mathf.Abs(damage) + " damage.";
        }
        else
        {
            battleResultText.text = "It's a draw!";
        }

        yield return new WaitForSeconds(delayBeforeResult);

        // กลับไปยัง Scene แรก
        yield return new WaitForSeconds(delayBeforeReturn);
        UnityEngine.SceneManagement.SceneManager.LoadScene(returnSceneName);
    }
}
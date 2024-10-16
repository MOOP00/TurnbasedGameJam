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
    public float battleDuration = 3f; // เวลาต่อสู้
    public float delayBeforeResult = 2f; // เวลาก่อนแสดงผลลัพธ์
    public float delayBeforeReturn = 2f; // เวลาก่อนวาปกลับ

    private bool battleStarted = false; // ตัวแปรบอกสถานะการเริ่มการต่อสู้

    void Update()
    {
        // ตรวจสอบว่ากด Space bar หรือยัง
        if (!battleStarted && Input.GetKeyDown(KeyCode.Space))
        {
            battleStarted = true;
            StartCoroutine(ExecuteBattle());
        }
    }

    private IEnumerator ExecuteBattle()
    {
        // ให้ลูกเต๋าแต่ละลูกถูกโยน
        player1Dice.ThrowObject();
        player2Dice.ThrowObject();

        // รอจนกว่าลูกเต๋าจะหยุดหมุน
        yield return new WaitForSeconds(battleDuration);

        // ดึงค่าที่ผู้เล่น 1 และ 2 ทอยได้
        int player1Roll = player1Dice.value;
        int player2Roll = player2Dice.value;

        // แสดงผลลัพธ์การทอยลูกเต๋า
        player1RollText.text = "Player 1 Roll: " + player1Roll;
        player2RollText.text = "Player 2 Roll: " + player2Roll;

        // รอเวลาเล็กน้อยก่อนคำนวณผลการต่อสู้
        yield return new WaitForSeconds(delayBeforeResult);

        // คำนวณผลการต่อสู้
        int damageToPlayer2 = Mathf.Max(0, player1Roll - player2Roll);
        int damageToPlayer1 = Mathf.Max(0, player2Roll - player1Roll);

        // ปรับลดเลือดของแต่ละผู้เล่น
        advantageSystem.ModifyHealth(true, -damageToPlayer1);  // ลดเลือด Player 1
        advantageSystem.ModifyHealth(false, -damageToPlayer2); // ลดเลือด Player 2

        // แสดงผลลัพธ์การต่อสู้
        if (damageToPlayer1 > damageToPlayer2)
        {
            battleResultText.text = "Player 1 wins the round!";
        }
        else if (damageToPlayer2 > damageToPlayer1)
        {
            battleResultText.text = "Player 2 wins the round!";
        }
        else
        {
            battleResultText.text = "It's a draw!";
        }

        // รอเวลาก่อนวาปกลับไปยัง Scene แรก
        yield return new WaitForSeconds(delayBeforeReturn);
        PlayerPrefs.SetInt("Player1Health", advantageSystem.health1); // บันทึกสุขภาพของ Player 1
        PlayerPrefs.SetInt("Player2Health", advantageSystem.health2); // บันทึกสุขภาพของ Player 2
        UnityEngine.SceneManagement.SceneManager.LoadScene(returnSceneName);
    }
}
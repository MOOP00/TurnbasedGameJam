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

    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;
    private ParticleSystem.MainModule mainModule;
    private bool battleStarted = false; // ตัวแปรบอกสถานะการเริ่มการต่อสู้
    private bool hasPlayed = false;
    AudioManager audioManager;

    void Start()
    {
        {
            // Initialization of components
            audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogWarning("AudioManager not found!");
            }

            if (player1Dice == null || player2Dice == null)
            {
                Debug.LogError("Dice references are not assigned in the inspector!");
            }
        }
    }

    void Update()
    {
        // ตรวจสอบว่ากด Space bar หรือยัง
        if (!battleStarted && Input.GetKeyDown(KeyCode.Space))
        {
            battleStarted = true;
            StartCoroutine(ExecuteBattle());
        }
    }

    private void PlayParticlesOnce(ParticleSystem ps)
    {
        if (ps != null && !hasPlayed)
        {
            ps.Play();
            hasPlayed = true;
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

        // แสดงผลลัพธ์การต่อสู้
        if (player1Roll > player2Roll)
        {
            advantageSystem.ModifyHealth(false, -AdvantageSystem.Attack1);
            battleResultText.text = "Player 1 wins the round!";
            AdvantageSystem.Attack1 = 0;
            AdvantageSystem.Attack2 = 0;
            PlayParticlesOnce(particleSystem1);
            audioManager.PlaySFX(audioManager.slash);

        }
        else if (player2Roll > player1Roll)
        {
            advantageSystem.ModifyHealth(true, -AdvantageSystem.Attack2);
            battleResultText.text = "Player 2 wins the round!";
            AdvantageSystem.Attack1 = 0;
            AdvantageSystem.Attack2 = 0;
            PlayParticlesOnce(particleSystem2);
            audioManager.PlaySFX(audioManager.magic);
        }
        else
        {
            battleResultText.text = "It's a draw!";
        }

        // รอเวลาก่อนวาปกลับไปยัง Scene แรก
        yield return new WaitForSeconds(delayBeforeReturn);
        PlayerPrefs.SetInt("Player1Health", AdvantageSystem.health1); // บันทึกสุขภาพของ Player 1
        PlayerPrefs.SetInt("Player2Health", AdvantageSystem.health2); // บันทึกสุขภาพของ Player 2
        UnityEngine.SceneManagement.SceneManager.LoadScene(returnSceneName);
    }
}
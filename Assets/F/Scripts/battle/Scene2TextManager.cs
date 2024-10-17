using UnityEngine;
using TMPro;

public class Scene2TextManager : MonoBehaviour
{
    public TextMeshProUGUI player1HealthText;
    public TextMeshProUGUI player2HealthText;
    public AdvantageSystem advantageSystem;

    void Start()
    {
        if (advantageSystem != null)
        {
            UpdateHealthText();
        }
    }

    void Update()
    {
        if (advantageSystem != null)
        {
            UpdateHealthText();
        }
    }

    void UpdateHealthText()
    {
        player1HealthText.text = "Player 1\nHealth: " + AdvantageSystem.health1;
        player2HealthText.text = "Player 2\nHealth: " + AdvantageSystem.health2;
    }
}
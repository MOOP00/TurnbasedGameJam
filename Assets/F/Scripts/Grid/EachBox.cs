using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EachBox : MonoBehaviour
{
    public GridBehavior gridBehavior;
    public AdvantageSystem advantageSystem;
    public int randomEffect;
    public TextMeshPro _text;

    void Start()
    {
        _text = GetComponentInChildren<TextMeshPro>();
        gridBehavior = GameObject.Find("GridManager").GetComponent<GridBehavior>();
        advantageSystem = GameObject.Find("GridManager").GetComponent<AdvantageSystem>();
        randomEffect = Random.Range(0, 9);

        switch (randomEffect)
        {
            case 0:
                _text.text = "+6 Step";
                break;
            case 1:
                _text.text = "+3 Step";
                break;
            case 2:
                _text.text = "+9 Step";
                break;
            case 3:
                _text.text = "+1 Reroll";
                break;
            case 4:
                _text.text = "+2 Reroll";
                break;
            case 5:
                _text.text = "-1 Reroll";
                break;
            case 6:
                _text.text = "+1 Dice";
                break;
            case 7:
                _text.text = "+2 Dice";
                break;
            case 8:
                _text.text = "-1 Dice";
                break;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == gridBehavior.player1 || other.gameObject == gridBehavior.player2)
        {
            bool isPlayer1 = other.gameObject == gridBehavior.player1;
            ApplyRandomEffect(isPlayer1);
            Destroy(gameObject);
        }
    }

    void ApplyRandomEffect(bool isPlayer1)
    {
        switch (randomEffect)
        {
            case 0:
                gridBehavior.stepLeft += 6;  // Gain 6 more steps this round
                Debug.Log("Gained 6 more steps!");
                break;
            case 1:
                gridBehavior.stepLeft += 3;  // Gain 6 more steps this round
                Debug.Log("Gained 3 more steps!");
                break;
            case 2:
                gridBehavior.stepLeft += 9;  // Gain 6 more steps this round
                Debug.Log("Gained 9 more steps!");
                break;
            case 3:
                advantageSystem.ModifyReroll(isPlayer1, 1);  // Gain +1 advantage
                Debug.Log("Gained +1 reroll!");
                break;
            case 4:
                advantageSystem.ModifyReroll(isPlayer1, 2);  // Gain +2 advantage
                Debug.Log("Gained +2 reroll!");
                break;
            case 5:
                advantageSystem.ModifyReroll(isPlayer1, -1);  // Gain -1 advantage
                Debug.Log("Gained -1 reroll!");
                break;
            case 6:
                advantageSystem.ModifyDice(isPlayer1, 1);  // Gain +1 advantage
                Debug.Log("Gained +1 dice!");
                break;
            case 7:
                advantageSystem.ModifyDice(isPlayer1, 2);  // Gain +2 advantage
                Debug.Log("Gained +2 dice!");
                break;
            case 8:
                advantageSystem.ModifyDice(isPlayer1, -1);  // Gain -1 advantage
                Debug.Log("Gained -1 dice!");
                break;
        }
    }
}

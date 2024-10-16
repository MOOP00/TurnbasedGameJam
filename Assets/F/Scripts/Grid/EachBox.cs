using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachBox : MonoBehaviour
{
    public GridBehavior gridBehavior;
    public AdvantageSystem advantageSystem;

    void Start()
    {
        gridBehavior = GameObject.Find("GridManager").GetComponent<GridBehavior>();
        advantageSystem = GameObject.Find("GridManager").GetComponent<AdvantageSystem>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == gridBehavior.player1 || other.gameObject == gridBehavior.player2)
        {
            bool isPlayer1 = other.gameObject == gridBehavior.player1;

            // Apply random effect when a player interacts with a mystery box
            ApplyRandomEffect(isPlayer1);

            // Destroy the mystery box after use
            Destroy(gameObject);
        }
    }

    void ApplyRandomEffect(bool isPlayer1)
    {
        int randomEffect = Random.Range(0, 4);  // Pick a random effect from 0 to 3

        switch (randomEffect)
        {
            case 0:
                gridBehavior.stepsToMove += 6;  // Gain 6 more steps this round
                Debug.Log("Gained 6 more steps!");
                break;
            case 1:
                advantageSystem.ModifyAdvantage(isPlayer1, 1);  // Gain +1 advantage
                Debug.Log("Gained +1 advantage!");
                break;
            case 2:
                advantageSystem.ModifyAdvantage(isPlayer1, 2);  // Gain +2 advantage
                Debug.Log("Gained +2 advantage!");
                break;
            case 3:
                advantageSystem.ModifyAdvantage(isPlayer1, -1);  // Gain -1 advantage
                Debug.Log("Gained -1 advantage!");
                break;
        }
    }
}

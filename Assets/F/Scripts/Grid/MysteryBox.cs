using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public GameObject mysteryBoxPrefab;  // The mystery box prefab
    public int mysteryBoxCount = 5;  // Number of mystery boxes in the grid
    public LayerMask obstacleLayer;

    public GridBehavior gridBehavior;
    public AdvantageSystem advantageSystem;

    void Start()
    {
        GenerateRandomMysteryBoxes();
    }

    void GenerateRandomMysteryBoxes()
    {
        for (int i = 0; i < mysteryBoxCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(1, gridBehavior.gridSize-1),
                0,
                Random.Range(1, gridBehavior.gridSize-1)
            );

            // Ensure mystery boxes do not spawn on player or obstacle positions
            while (randomPos == gridBehavior.player1Pos || randomPos == gridBehavior.player2Pos || Physics.CheckSphere(randomPos, 0.4f, obstacleLayer))
            {
                randomPos = new Vector3(
                    Random.Range(1, gridBehavior.gridSize-1),
                    0,
                    Random.Range(1, gridBehavior.gridSize-1)
                );
            }

            Instantiate(mysteryBoxPrefab, randomPos, Quaternion.identity);
        }
    }
}

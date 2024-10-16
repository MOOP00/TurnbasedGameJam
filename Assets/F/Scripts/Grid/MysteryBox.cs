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
                Random.Range(0, gridBehavior.gridSize),
                0,
                Random.Range(0, gridBehavior.gridSize)
            );

            // Ensure mystery boxes do not spawn on player or obstacle positions
            while (randomPos == gridBehavior.player1Pos || randomPos == gridBehavior.player2Pos || Physics.CheckSphere(randomPos, 0.4f, obstacleLayer))
            {
                randomPos = new Vector3(
                    Random.Range(0, gridBehavior.gridSize),
                    0,
                    Random.Range(0, gridBehavior.gridSize)
                );
            }

            Instantiate(mysteryBoxPrefab, randomPos, Quaternion.identity);
        }
    }
}
